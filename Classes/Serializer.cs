// Made by Lonami Exo (C) LonamiWebs
// Creation date: february 2016
// Modifications:
// - 21-03-2016. Added methods to manage files
using Capshot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

/* Requires System.Runtime.Serialization.dll
By default, any class will serialize it's public properties.
If you want to explicitely specify what will be serialized, see below example:

[DataContract]
class MyClass
{
    // will serialize
    [DataMember]
    <modifier> <name1>;
    
    [DataMember]
    <modifier> <name2> { get {...} set {...} }
    
    [XmlElement(ElementName = "<custom name>")]
    <modifer> <name3>
    
    // will not serialize
    <modifier> <name4>;
    <modifier> <name5> { get {...} set {...} }
}
*/

public static class Serializer
{
    /// <summary>
    /// Determines whether the generated XML should be indented or not 
    /// </summary>
    public static bool Indent = true;

    static readonly List<Type> KnownTypes = new List<Type> { typeof(SerializableColor) };

    /// <summary>
    /// Serializes a given object into the desired file
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize</typeparam>
    /// <param name="obj">The object to serialize</param>
    /// <param name="file">The file where the object will be serialized</param>
    public static void Serialize<T>(T obj, string file) {
        File.WriteAllText(file, SerializeToString(obj), Encoding.UTF8);
    }

    /// <summary>
    /// Serializes a given object into a string
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize</typeparam>
    /// <param name="obj">The object to serialize</param>
    /// <returns>The serialized object</returns>
    public static string SerializeToString<T>(T obj)
    {
        var settings = new XmlWriterSettings { Indent = Indent };

        using (var ms = new MemoryStream())
        using (var sr = new StreamReader(ms))
        {
            var serializer = new DataContractSerializer(typeof(T), KnownTypes);
            using (var writer = XmlWriter.Create(ms, settings))
                serializer.WriteObject(writer, obj);

            ms.Position = 0;
            return sr.ReadToEnd();
        }
    }

    /// <summary>
    /// Deserializes an object from the given file
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize</typeparam>
    /// <param name="file">The file from where the object will be deserialized</param>
    /// <returns>The deserialized object</returns>
    public static T Deserialize<T>(string file) {
        return DeserializeFromString<T>(File.ReadAllText(file, Encoding.UTF8));
    }

    /// <summary>
    /// Deserializes an object from a given XML string
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize</typeparam>
    /// <param name="xml">The serialized object</param>
    /// <returns>The deserialized object</returns>
    public static T DeserializeFromString<T>(string xml)
    {
        using (var ms = new MemoryStream())
        {
            byte[] data = Encoding.UTF8.GetBytes(xml);
            ms.Write(data, 0, data.Length);
            ms.Position = 0;
            var deserializer = new DataContractSerializer(typeof(T), KnownTypes);
            return (T)deserializer.ReadObject(ms);
        }
    }
}
