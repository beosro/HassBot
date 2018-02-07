///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : ValidateYaml.cs
//  DESCRIPTION     : Utility class that validates given YAML code
///////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace HassBotUtils {

    public sealed class ValidateYaml {

        public static bool Validate(string yamlData, out string errorMessage) {
            try {
                yamlData = yamlData.Replace("```yaml", string.Empty).Replace("```", string.Empty);
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
    }
}
