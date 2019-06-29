# NbtLib

.NET library to work with NBT data.

NBT specification can be found [here](https://minecraft.gamepedia.com/NBT_format)

## Basic usage

### Raw NBT

Reading

```C#
using (var inputStream = System.IO.File.OpenRead("inputfile.nbt")) {
    var nbtData = NbtConvert.ParseNbtStream(inputStream);
}
```

Writing
```C#
var nbtTag = new NbtCompoundTag();
var outputStream = NbtConvert.CreateNbtStream(nbtTag);
```

In addition to the static helper methods, it is possible to use the `NbtParser` and `NbtWriter` classes directly.

According to the specification, NBT files should be GZipped.
Parser functions will handle both compressed and uncompressed data.
Writing functions will generate data that has been GZipped. Companion methods with `Uncompressed` in the name exist to create output streams without compressing the data.

### (De)serialization

Reading
```C#
using (var inputStream = System.IO.File.OpenRead("inputfile.nbt")) {
    var myObject = NbtConvert.DeserializeObject<MyClass>(inputStream);
}
```