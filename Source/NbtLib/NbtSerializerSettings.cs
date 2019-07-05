namespace NbtLib
{
    public class NbtSerializerSettings
    {
        public INamingStrategy NamingStrategy { get; set; } = new DefaultNamingStrategy();
        public bool UseArrayTypes { get; set; } = true;
        public bool EmptyListAsEnd { get; set; } = false;
    }
}
