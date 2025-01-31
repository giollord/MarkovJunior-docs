﻿// Copyright (C) 2022 Maxim Gumin, The MIT License (MIT)

using System;
using System.Text;
using System.Collections.Generic;
using MarkovJuniorLib.Models;

namespace MarkovJuniorLib.Internal
{
    /// <summary>
    /// Helper functions for loading and saving <c>.vox</c> files for 3D grids.
    /// </summary>
    static class VoxHelper
    {
        /// <summary>
        /// Loads a 3D grid state from a <c>.vox</c> file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>A tuple of (state, MX, MY, MZ), or <c>(null, -1, -1, -1)</c> if the loading fails.</returns>
        public static (int[], int, int, int) LoadVox(byte[] data)
        {
            try
            {
                using var ms = new System.IO.MemoryStream(data);
                using var stream = new System.IO.BinaryReader(ms);

                int[] result = null;
                int MX = -1, MY = -1, MZ = -1;

                string magic = new(stream.ReadChars(4));
                int version = stream.ReadInt32();

                while (stream.BaseStream.Position < stream.BaseStream.Length)
                {
                    byte[] bt = stream.ReadBytes(1);
                    char head = Encoding.ASCII.GetChars(bt)[0];

                    if (head == 'S')
                    {
                        string tail = Encoding.ASCII.GetString(stream.ReadBytes(3));
                        if (tail != "IZE") continue;

                        int chunkSize = stream.ReadInt32();
                        stream.ReadBytes(4);
                        //Console.WriteLine("found SIZE chunk");
                        MX = stream.ReadInt32();
                        MY = stream.ReadInt32();
                        MZ = stream.ReadInt32();
                        stream.ReadBytes(chunkSize - 4 * 3);
                        //Console.WriteLine($"size = ({MX}, {MY}, {MZ})");
                    }
                    else if (head == 'X')
                    {
                        string tail = Encoding.ASCII.GetString(stream.ReadBytes(3));
                        if (tail != "YZI") continue;

                        if (MX <= 0 || MY <= 0 || MZ <= 0) return (null, MX, MY, MZ);
                        result = new int[MX * MY * MZ];
                        for (int i = 0; i < result.Length; i++) result[i] = -1;

                        //Console.WriteLine("found XYZI chunk");
                        stream.ReadBytes(8);
                        int numVoxels = stream.ReadInt32();
                        //Console.WriteLine($"number of voxels = {numVoxels}");
                        for (int i = 0; i < numVoxels; i++)
                        {
                            byte x = stream.ReadByte();
                            byte y = stream.ReadByte();
                            byte z = stream.ReadByte();
                            byte color = stream.ReadByte();
                            result[x + y * MX + z * MX * MY] = color;
                            //Console.WriteLine($"adding voxel {x} {y} {z} of color {color}");
                        }
                    }
                }
                return (result, MX, MY, MZ);
            }
            catch (Exception) { return (null, -1, -1, -1); }
        }

        static void WriteString(this System.IO.BinaryWriter stream, string s) { foreach (char c in s) stream.Write(c); }

        /// <summary>
        /// Saves a 3D grid state as a <c>.vox</c> file.
        /// </summary>
        public static byte[] SaveVox(byte[] state, byte MX, byte MY, byte MZ, int[] palette, string filename)
        {
            List<(byte, byte, byte, byte)> voxels = new();
            for (byte z = 0; z < MZ; z++) for (byte y = 0; y < MY; y++) for (byte x = 0; x < MX; x++)
                    {
                        int i = x + y * MX + z * MX * MY;
                        byte v = state[i];
                        if (v != 0) voxels.Add((x, y, z, (byte)(v + 1)));
                    }

            var memoryStream = new System.IO.MemoryStream();
            using System.IO.BinaryWriter stream = new(memoryStream);

            stream.WriteString("VOX ");
            stream.Write(150);

            stream.WriteString("MAIN");
            stream.Write(0);
            stream.Write(1092 + voxels.Count * 4);

            stream.WriteString("PACK");
            stream.Write(4);
            stream.Write(0);
            stream.Write(1);

            stream.WriteString("SIZE");
            stream.Write(12);
            stream.Write(0);
            stream.Write((int)MX);
            stream.Write((int)MY);
            stream.Write((int)MZ);

            stream.WriteString("XYZI");
            stream.Write(4 + voxels.Count * 4);
            stream.Write(0);
            stream.Write(voxels.Count);

            foreach (var (x, y, z, color) in voxels)
            {
                stream.Write(x);
                //stream.Write((byte)(size.y - v.y - 1));
                stream.Write(y);
                stream.Write(z);
                stream.Write(color);
            }

            stream.WriteString("RGBA");
            stream.Write(1024);
            stream.Write(0);

            foreach (int c in palette)
            {
                //(byte R, byte G, byte B) = c.ToTuple();
                stream.Write((byte)((c & 0xff0000) >> 16));
                stream.Write((byte)((c & 0xff00) >> 8));
                stream.Write((byte)(c & 0xff));
                stream.Write((byte)0);
            }
            for (int i = palette.Length; i < 255; i++)
            {
                stream.Write((byte)(0xff - i - 1));
                stream.Write((byte)(0xff - i - 1));
                stream.Write((byte)(0xff - i - 1));
                stream.Write((byte)(0xff));
            }
            stream.Write(0);

            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            var bytes = memoryStream.ToArray();
            return bytes;
        }

        /// <summary>
        /// Saves a 3D grid state as array of colors Color32[x,y,z]
        /// </summary>
        public static Color32[,,] SaveColorsArray(byte[] state, int MX, int MY, int MZ, int[] palette)
        {
            static byte GetColorComp(int val, int comp) => (byte)((val >> (comp * 8)) & 255);

            var result = new Color32[MX, MY, MZ];
            for (int z = 0; z < MZ; z++) for (int y = 0; y < MY; y++) for (int x = 0; x < MX; x++)
                    {
                        int i = x + y * MX + z * MX * MY;
                        int c = palette[state[i]];
                        result[x, y, z] = new Color32(GetColorComp(c, 2), GetColorComp(c, 1), GetColorComp(c, 0), GetColorComp(c, 3));
                    }
            return result;
        }

        /// <summary>
        /// Saves a 3D grid state as array of integer representations of colors int[x,y,z]
        /// </summary>
        public static int[,,] SavePaletteArray(byte[] state, int MX, int MY, int MZ, int[] palette)
        {
            var result = new int[MX, MY, MZ];
            for (int z = 0; z < MZ; z++) for (int y = 0; y < MY; y++) for (int x = 0; x < MX; x++)
                    {
                        int i = x + y * MX + z * MX * MY;
                        result[x, y, z] = palette[state[i]];
                    }
            return result;
        }
    }
}