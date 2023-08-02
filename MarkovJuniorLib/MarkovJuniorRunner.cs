﻿using MarkovJuniorLib.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

using Graphics = MarkovJuniorLib.Internal.Graphics;
using GUI = MarkovJuniorLib.Internal.GUI;

namespace MarkovJuniorLib
{
    /// <summary>
    /// Entry point to run MarkovJunior algorithm
    /// </summary>
    public static class MarkovJuniorRunner
    {
        /// <summary>
        /// Get default pallette
        /// </summary>
        /// <returns>Dictionary with symbols and matching colors</returns>
        public static Dictionary<char, Color32> GetDefaultPallette()
        {
            static byte GetColorComp(int val, int comp) => (byte)((val >> (comp * 8)) & 255);

            using var palletteTextReader = new System.IO.StringReader(Constants.Pallette_XML);
            Dictionary<char, Color32> pallette = XDocument.Load(palletteTextReader).Root.Elements("color").ToDictionary(
                x => x.Get<char>("symbol"),
                x =>
                {
                    var i = (255 << 24) + Convert.ToInt32(x.Get<string>("value"), 16);
                    return new Color32(GetColorComp(i, 2), GetColorComp(i, 1), GetColorComp(i, 0), GetColorComp(i, 3));
                });
            return pallette;
        }

        /// <summary>
        /// Run algorithm lazily, so results will be generated during enumeration
        /// </summary>
        /// <param name="modelConfig">Configuration</param>
        /// <returns>Results of execution</returns>
        /// <exception cref="Exception">Throws exception if something goes wrong</exception>
        public static IEnumerable<RunResult> Run(ModelConfig modelConfig)
        {
            //Resources.palette
            using var palletteTextReader = new System.IO.StringReader(Constants.Pallette_XML);
            Dictionary<char, int> palette = XDocument.Load(palletteTextReader).Root.Elements("color").ToDictionary(x => x.Get<char>("symbol"), x => (255 << 24) + Convert.ToInt32(x.Get<string>("value"), 16));

            using var modelTextReader = new System.IO.StringReader(modelConfig.ModelXML);
            XDocument modeldoc = XDocument.Load(modelTextReader, LoadOptions.SetLineInfo);

            Interpreter interpreter = Interpreter.Load(modelConfig, modeldoc.Root, modelConfig.Width_MX, modelConfig.Height_MY, modelConfig.Depth_MZ);
            if (interpreter == null)
            {
                throw new Exception("Can not create interpreter");
            }

            Dictionary<char, int> customPalette = new(palette);
            foreach (var x in modelConfig.Colors) customPalette[x.Symbol] = (x.Color.a << 24) + (x.Color.r << 16) + (x.Color.g << 8) + (x.Color.b);

            byte[] initialState = null;
            if(modelConfig.InitialTexture != null)
            {
                if (modelConfig.InitialTexture.width != modelConfig.Width_MX || modelConfig.InitialTexture.height != modelConfig.Height_MY)
                    throw new Exception("Initial texture width and height must match MX and MY");
                if (modelConfig.Depth_MZ != 1)
                    throw new Exception("Initial texture can be only provided when MZ is 1");

                var (initialColors, _, _, _) = Graphics.LoadBitmap(modelConfig.InitialTexture);
                var colorIndexes = customPalette.ToDictionary(x => x.Value, x => (byte)Array.IndexOf(interpreter.grid.characters, x.Key));
                initialState = new byte[initialColors.Length];
                for (var i = 0; i < initialColors.Length; i++)
                {
                    initialState[i] = colorIndexes[initialColors[i]];
                }
            }

            var results = new List<RunResult>();
            for (int k = 0; k < modelConfig.Amount; k++)
            {
                int seed = modelConfig.Seeds != null && k < modelConfig.Seeds.Length ? modelConfig.Seeds[k] : UnityEngine.Random.Range(0, int.MaxValue);
                foreach ((byte[] result, char[] legend, int FX, int FY, int FZ) in interpreter.Run(seed, modelConfig.Steps, modelConfig.Gif, initialState))
                {
                    int[] colors = legend.Select(ch => customPalette[ch]).ToArray();
                    //string outputname = modelConfig.Gif ? $"output/{interpreter.counter}" : $"output/{modelConfig.Name}_{seed}";
                    if (FZ == 1 || modelConfig.Iso)
                    {
                        var (bitmap, WIDTH, HEIGHT) = Graphics.Render(result, FX, FY, FZ, colors, modelConfig.PixelSize, modelConfig.Gui);
                        if (modelConfig.Gui > 0) GUI.Draw(modelConfig.Name, interpreter.root, interpreter.current, bitmap, WIDTH, HEIGHT, customPalette);
                        var tex = Graphics.SaveBitmap(bitmap, WIDTH, HEIGHT);
                        yield return new RunResult { Texture = tex };
                    }
                    else
                    {
                        var vox = VoxHelper.SaveVox(result, (byte)FX, (byte)FY, (byte)FZ, colors, null);
                        yield return new RunResult { Vox = vox };
                    }
                }
            }
        }
    }
}