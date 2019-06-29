using System.IO;

namespace NbtLib
{
    public static class NbtConvert
    {
        public static NbtCompoundTag ParseNbtStream(Stream stream)
        {
            var parser = new NbtParser();
            return parser.ParseNbtStream(stream);
        }

        public static Stream CreateNbtStream(NbtCompoundTag rootTag)
        {
            var writer = new NbtWriter();
            return writer.CreateNbtStream(rootTag);
        }

        public static Stream CreateNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var writer = new NbtWriter();
            return writer.CreateNbtStream(rootTag, rootTagName);
        }

        public static Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag)
        {
            var writer = new NbtWriter();
            return writer.CreateUncompressedNbtStream(rootTag);
        }

        public static Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var writer = new NbtWriter();
            return writer.CreateUncompressedNbtStream(rootTag, rootTagName);
        }
    }
}
