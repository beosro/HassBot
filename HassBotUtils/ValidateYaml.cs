///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : ValidateYaml.cs
//  DESCRIPTION     : Utility class that validates given YAML code
///////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using YamlDotNet.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HassBotUtils {

    public sealed class ValidateHelper{
        private static readonly string YAML_START = @"```yaml";
        private static readonly string YAML_END = @"```";
        private static readonly string JSON_START = @"```json";
        private static readonly string JSON_END = @"```";

        public static bool ValidateYaml(string yamlData, out string errorMessage) {
            try {
                yamlData = yamlData.Replace(YAML_START, string.Empty).Replace(YAML_END, string.Empty);
                errorMessage = string.Empty;
                var input = new StringReader(yamlData);
                var deserializer = new Deserializer();
                object o = deserializer.Deserialize(input);
                if (o == null)
                    return false;
                return true;
            }
            catch (Exception e) {
                errorMessage = e.Message;
                return false;
            }
        }

        public static bool ValidateJson(string stringValue, out string errorMessage) {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(stringValue)) {
                return false;
            }

            var value = stringValue.Replace(JSON_START, string.Empty).Replace(JSON_END, string.Empty).Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || // For object
                (value.StartsWith("[") && value.EndsWith("]")))   // For array
            {
                try {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException e) {
                    errorMessage = e.ToString();
                    return false;
                }
            }
            return false;
        }
    }
}
