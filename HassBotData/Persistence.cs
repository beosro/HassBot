///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Persistence.cs
//  DESCRIPTION     : A class that persists custom commands and such
///////////////////////////////////////////////////////////////////////////////
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using HassBotDTOs;
using System;

namespace HassBotData {
    public class Persistence {

        public static List<CommandDTO> LoadCommands(string filePath) {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<CommandDTO>>(json);
        }

        public static void SaveCommands(List<CommandDTO> commands, string filePath) {
            string json = JsonConvert.SerializeObject(commands, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<AFKDTO> LoadAFKUsers(string filePath) {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<AFKDTO>>(json);
        }

        public static void SaveAFKUsers(List<AFKDTO> afkUsers, string filePath) {
            string json = JsonConvert.SerializeObject(afkUsers, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static bool FileExists(string filePath) {
            return File.Exists(filePath);
        }
    }
}