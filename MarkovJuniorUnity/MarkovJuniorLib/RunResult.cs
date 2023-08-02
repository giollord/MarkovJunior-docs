using UnityEngine;

namespace MarkovJuniorLib
{
    /// <summary>
    /// Result of running model. Will contain either texture, either .vox file contents, but not both
    /// </summary>
    public class RunResult
    {
        /// <summary>
        /// Result texture
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Result .vox file contents
        /// </summary>
        public byte[] Vox { get; set; }
    }
}
