using MarkovJuniorLib.ToOverride;

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
        public ITexture2D Texture { get; set; }
        /// <summary>
        /// Result .vox file contents
        /// </summary>
        public byte[] Vox { get; set; }
    }

    public abstract class RunResult<TTexture> where TTexture : class
    {
        /// <summary>
        /// Result texture
        /// </summary>
        public TTexture Texture { get; set; }
        /// <summary>
        /// Result .vox file contents
        /// </summary>
        public byte[] Vox { get; set; }
    }
}
