﻿using MarkovJuniorLib.ToOverride;

namespace MarkovJuniorLib.InputConfig
{
    public class ResourceTexture : Resource
    {
        public ResourceTexture(ITexture2D texture)
        {
            Texture = texture;
        }

        public ITexture2D Texture { get; set; }
    }
}