///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : Utils.cs
//  DESCRIPTION     : A Util class with bunch of utility methods
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;
using System.IO;
using YamlDotNet.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace HassBotUtils
{
    public sealed class Utils
    {
        private static readonly string HASTEBIN_POSTURL = "https://hastebin.com/documents";
        private static readonly string HASTEBIN_RETURN = "https://hastebin.com/{0}";

        private static readonly log4net.ILog logger =
                    log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool LineCountCheck(string message) {
            if (string.Empty == message)
                return true;

            int maxLinesLimit = AppSettingsUtil.AppSettingsInt("maxLinesLimit", false, 15);
            bool yamlHeader = false;
            string YAML_START = @"```yaml";
            string YAML_START_1 = @"```";
            string YAML_END = @"```";

            int start = message.IndexOf(YAML_START);
            if ( -1 == start)
                start = message.IndexOf(YAML_START_1);

            int end = message.IndexOf(YAML_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                yamlHeader = false;
            else
                yamlHeader = true;

            if (yamlHeader)
                maxLinesLimit = maxLinesLimit +2;

            if (message.Split('\n').Length > maxLinesLimit) {
                return false;
            }
            return true;
        }

        public static string Yaml2Json(string yaml) {
            try {
                var r = new StringReader(yaml);
                var deserializer = new Deserializer();
                var yamlObject = deserializer.Deserialize(r);

                string json = JsonConvert.SerializeObject(yamlObject, Formatting.Indented);
                return json;

            }
            catch (Exception e) {
                return e.ToString();
            }
        }

        public static string Json2Yaml(string json) {
            try {
                var swaggerDocument = ConvertJTokenToObject(JsonConvert.DeserializeObject<JToken>(json));

                var serializer = new YamlDotNet.Serialization.Serializer();

                using (var writer = new StringWriter()) {
                    serializer.Serialize(writer, swaggerDocument);
                    var yaml = writer.ToString();
                    return yaml;
                }
            }
            catch (Exception e) {
                return e.ToString();
            }
        }

        private static object ConvertJTokenToObject(JToken token) {
            if (token is JValue)
                return ((JValue)token).Value;
            if (token is JArray)
                return token.AsEnumerable().Select(ConvertJTokenToObject).ToList();
            if (token is JObject)
                return token.AsEnumerable().Cast<JProperty>().ToDictionary(x => x.Name, x => ConvertJTokenToObject(x.Value));
            throw new InvalidOperationException("Unexpected token: " + token);
        }

        public static string Base64Encode(string plainText) {
            try {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception e) {
                return "Could not encode! Error: " + e.Message;
            }
        }

        public static string Base64Decode(string base64EncodedData) {
            try {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception e) {
                return "Could not decode! Error: " + e.Message;
            }
        }

        public static string Post2HasteBin(string payload) {
            if (payload.Trim() == string.Empty)
                return string.Empty;

            try {
                var data = Encoding.ASCII.GetBytes(payload);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(HASTEBIN_POSTURL);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                using (var stream = request.GetRequestStream()) {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                dynamic stuff = JsonConvert.DeserializeObject(responseString);
                return SafeFormatter.Format(HASTEBIN_RETURN, stuff["key"]);
            }
            catch (Exception e) {
                logger.Error(e.Message);
            }

            return string.Empty;
        }
    }
}