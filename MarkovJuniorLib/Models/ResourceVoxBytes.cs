namespace MarkovJuniorLib.Models
{
    public class ResourceVoxBytes : Resource
    {
        public ResourceVoxBytes(byte[] vox)
        {
            Vox = vox;
        }

        public new byte[] Vox { get; set; }
    }
}
