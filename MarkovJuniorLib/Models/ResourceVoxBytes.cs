namespace MarkovJuniorLib.Models
{
    public class ResourceVoxBytes : Resource
    {
        public ResourceVoxBytes(byte[] vox)
        {
            Vox = vox;
        }

        public byte[] Vox { get; set; }
    }
}
