using System.Text;
using System.IO;
using YamlDotNet.Serialization;

class Converter
{

    static void Main(string[] args)
    {
        convert("config.yml");

        //write in to json file File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);
        System.Environment.Exit(0);
    }

    static string convert(string filename)
    {
        
        var serializer = new Serializer();
        var deserializer = new Deserializer();
        var writer = new StringWriter();

        var r = new StreamReader(filename);
        var yamlObject = deserializer.Deserialize(r);

        serializer.Serialize(writer, yamlObject);
        string json = writer.ToString();


    }


    static string loadAdditionalFiles(string[] files)
    {

    }
}
