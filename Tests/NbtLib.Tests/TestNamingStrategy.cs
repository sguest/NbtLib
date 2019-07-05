namespace NbtLib.Tests
{
    public class TestNamingStrategy : INamingStrategy
    {
        public string GetTagName(string name) => name + "-TEST";
    }
}
