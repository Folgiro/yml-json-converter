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
        int importIndex = 0;
        string[] filenames = ""; //array list
        bool insideArray = false;

        var r = new StreamReader(filename);
        var yamlObject = deserializer.Deserialize(r);

        serializer.Serialize(writer, yamlObject);
        string json = writer.ToString();

        if (json.IndexOf("imports") == 2)
        {
            for (int i = 0; i < json.Length; i++)
            {

                int add = json[i] == ':' ? 3 : 1;

                if (insideArray && json[i] == '"' || add == 3)
                {
                    string temp = "";
                    i += add;
                    for (; i != '"'; i++)
                    {
                        temp += json[i];
                    }

                    filenames.push(temp);
                    insideArray = true;
                }

                if (json[i] == ']' && json[i + 1] == ',')
                {
                    importIndex = i + 1;
                    break;
                }
            }

            var files = loadAdditionalFiles(filenames);
            foreach (string file in files)
            {
                json.append(file); // insert possible? otherwise, end has to be deleted and reappended afterwards. (})
            }

            return json;

        }
    }


    static string[] loadAdditionalFiles(string[] filenames)
    {
        string[] files; // arraylist
        foreach (string filename in filenames)
        {
            files.push(convert(filename));
        }

        return files;
    }
}
