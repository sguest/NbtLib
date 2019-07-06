# NbtLib

.NET library to work with NBT data.

NBT specification can be found [here](https://minecraft.gamepedia.com/NBT_format). Somewhat more technical specification [here](http://web.archive.org/web/20110723210920/http://www.minecraft.net/docs/NBT.txt) but it is older and missing a couple of the newer tag types (Int array and long array).

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

## (De)serialization

Reading
```C#
using (var inputStream = System.IO.File.OpenRead("inputfile.nbt")) {
    var myObject = NbtConvert.DeserializeObject<MyClass>(inputStream);
}

var tag = new NbtCompoundTag();
var myObject = NbtConvert.DeserializeObject<MyClass>(tag);
```

Writing
```C#
var myObject = new MyClass();
Stream outputStream = NbtConvert.SerializeObject(myObject);
NbtCompundTag tag = NbtConvert.SerializeObjectToTag(myObject);
```

In addition to the static helper methods, it is possible to use the `NbtDeserializer` and `NbtSerializer` classes directly.

(De)serialization can be further customized with the `NbtIgnore` or `NbtProperty` attributes, and/or by using `NbtSerializerSettings`

```C#
class MyClass
{
    // this property will not be populated by the deserializer
    // this property will not appear in serialized output
    [NbtIgnore]
    public string Ignored { get; set; }

    // this property will have the tag name TagName in the output, or will be read from a tag named TagName
    // this property will be serialized to a List tag with TagType Int instead of the default behavior of IntArray
    // if this list is empty, the tag type will be End instead of Int
    [NbtProperty(PropertyName="TagName", UseArrayType=false, EmptyListAsEnd=true)]
    public List<int> CustomizedProperty { get; set; }
}
```

```C#
var myObject = new MyClass();
var settings = new NbtSerializerSettings();
Stream outputStream = NbtConvert.SerializeObject(myObject, settings);
```

#### Available Settings

`UseArrayTypes` (Default `true`) If true, `IEnumerables` (including arrays) of `byte`, `int`, and `long` will be serialized to the corresponding array tag type. If false, they will be serialized to List tags

`EmptyListAsEnd` (Default: `false`) If true, empty `IEnumerables` that are serialized to lists will have their tag type set to `Empty`

`NamingStrategy` (Default: `DefaultNamingStrategy`) Object implementing `INamingStrategy` that will be used to format tag names. Default implementation leaves names unchanged.

## Formatting

All tags return sensible values when calling `ToString()`, and can also return a formatted JSON representation via `ToJsonString()`.
One of the easiest ways to get a reasonable representation of a NBT file is to parse it to a `NbtCompoundTag` then call `ToJsonString`.