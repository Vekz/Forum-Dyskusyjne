using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Forum_Dyskusyjne.Areas.Utils
{
    public class JsonUtils
    {
        public static List<string> ReadStringListFromJson(string path)
        {
            List<string> stringList;
            if (File.Exists(path))
            {
                var fileContent = File.ReadAllText(path).Replace("\r\n", string.Empty);
                var json = JsonConvert.DeserializeObject<List<string>>(fileContent);
                stringList = json;
            }
            else
            {
                File.Create(path);
                stringList = new List<string>();
            }

            return stringList;
        }

        public static void SaveListToJson(string path, List<string> data)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        } 
        
        
        public static string ReadStringFromJson(string path)
        {
            string result;
            if (File.Exists(path))
            {
                var fileContent = File.ReadAllText(path).Replace("\r\n", string.Empty);
                var json = JsonConvert.DeserializeObject<string>(fileContent);
                result = json;
            }
            else
            {
                File.Create(path);
                result = "";
            }

            return result;
        }

        public static void SaveToJson(string path, string data)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}