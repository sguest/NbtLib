namespace NbtLib
{
    public class DefaultNamingStrategy : INamingStrategy
    {
        public string GetTagName(string name) => name;
    }
}
