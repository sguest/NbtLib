using System.IO;

namespace NbtLib.Tests
{
    public static class TestHelpers
    {
        public static bool StreamsEqual(Stream stream1, Stream stream2)
        {
            if(stream1.Length != stream2.Length)
            {
                return false;
            }

            for(var i = 0; i < stream1.Length; i++)
            {
                if (stream1.ReadByte() != stream2.ReadByte())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
