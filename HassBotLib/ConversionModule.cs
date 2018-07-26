﻿///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HassBot.cs
//  DESCRIPTION     : A class that implements various conversion commands
//                  :   c2f -> converts celsius to fahrenheit
//                  :   f2c -> converts fahrenheit to celsius
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Web;

namespace HassBotLib {

    public class ConversionModule : BaseModule {

        private static readonly string C2F_ERROR_USAGE =
            "Usage: c2f <numeric value of temperature in celsius>";
        private static readonly string F2C_ERROR_USAGE =
            "Usage: f2c <numeric value of temperature in fahrenheit>";
        private static readonly string HEX2DEC_ERROR_USAGE =
            "Usage: hex2dec <decimal value>";
        private static readonly string DEC2HEX_ERROR_USAGE =
            "Usage: dec2hex <hex value>";
        private static readonly string BIN2DEC_ERROR_USAGE =
            "Usage: bin2dec <binary value>";
        private static readonly string DEC2BIN_ERROR_USAGE =
            "Usage: dec2bin <decimal value>";

        [Command("c2f")]
        public async Task CelsiusToFahrenheit() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", C2F_ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("f2c")]
        public async Task FahrenheitToCelsius() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", F2C_ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("hex2dec")]
        public async Task HexToDec() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", HEX2DEC_ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("dec2hex")]
        public async Task DecToHex() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", DEC2HEX_ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("dec2bin")]
        public async Task Dec2Bin() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", DEC2BIN_ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("bin2dec")]
        public async Task Bin2Dec() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", BIN2DEC_ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("c2f")]
        public async Task CelsiusToFahrenheit([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":thermometer:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = GetMentionedUsers(ref cmd);

            double temp_c = 0.0;
            try {
                temp_c = double.Parse(cmd.Trim());
            }
            catch {
                temp_c = 0.0;
            }

            double temp_f = ConvertCelsiusToFahrenheit(temp_c);
            embed.AddInlineField("Celsius To Fahrenheit",
                string.Format("{0} {1} degrees celsius = {2} degrees fahrenheit!", mentionedUsers, temp_c, temp_f.ToString("#.##")));
            await ReplyAsync("", false, embed);
        }

        [Command("f2c")]
        public async Task FahrenheitToCelsius([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":thermometer:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = GetMentionedUsers(ref cmd);

            double temp_f = 0.0;
            try {
                temp_f = double.Parse(cmd.Trim());
            }
            catch {
                temp_f = 0.0;
            }

            double temp_c = ConvertFahrenheitToCelsius(temp_f);
            embed.AddInlineField("Fahrenheit To Celsius",
                string.Format("{0} {1} degrees fahrenheit = {2} degrees celsius!", mentionedUsers, temp_f, temp_c.ToString("#.##")));
            await ReplyAsync("", false, embed);
        }

        [Command("hex2dec")]
        public async Task Hex2Dec([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = GetMentionedUsers(ref cmd);

            int decValue = Hex2Decimal(cmd.Trim());
            embed.AddInlineField("Hex To Decimal",
                string.Format("{0} '{1}' in hex = '{2}' in decimal", mentionedUsers, cmd.Trim(), decValue));
            await ReplyAsync("", false, embed);
        }

        [Command("dec2hex")]
        public async Task Dec2Hex([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = GetMentionedUsers(ref cmd);
            int decValue = 0;
            try {
                decValue = int.Parse(cmd.Trim());
            }
            catch {
                decValue = 0;
            }

            string hexValue = Decimal2Hex(decValue);
            embed.AddInlineField("Decimal To Hex",
                string.Format("{0} '{1}' in dec = '{2}' in hex", mentionedUsers, cmd.Trim(), hexValue));
            await ReplyAsync("", false, embed);
        }

        [Command("dec2bin")]
        public async Task Dec2Bin([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = GetMentionedUsers(ref cmd);
            int decValue = 0;
            try {
                decValue = int.Parse(cmd.Trim());
            }
            catch {
                decValue = 0;
            }

            string binValue = Decimal2Binary(decValue);
            embed.AddInlineField("Decimal To Binary",
                string.Format("{0} '{1}' in decimal = '{2}' in binary", mentionedUsers, cmd.Trim(), binValue));
            await ReplyAsync("", false, embed);
        }

        [Command("bin2dec")]
        public async Task Bin2Dec([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = GetMentionedUsers(ref cmd);
            int decValue = Binary2Decimal(cmd.Trim());
            embed.AddInlineField("Decimal To Binary",
                string.Format("{0} '{1}' in binary = '{2}' in decimal", mentionedUsers, cmd.Trim(), decValue));
            await ReplyAsync("", false, embed);
        }

        private string GetMentionedUsers(ref string cmd) {
            string mentionedUsers = base.MentionUsers();
            if (string.Empty != mentionedUsers) {
                foreach (string user in mentionedUsers.Split(' '))
                    if (string.Empty != user) {
                        string userHandle = user.Replace("!", string.Empty);
                        cmd = cmd.Replace(userHandle.Trim(), string.Empty);
                    }
            }

            return mentionedUsers;
        }

        public static double ConvertCelsiusToFahrenheit(double c) {
            return ((9.0 / 5.0) * c) + 32;
        }

        public static double ConvertFahrenheitToCelsius(double f) {
            return (5.0 / 9.0) * (f - 32);
        }

        public static int Hex2Decimal(string hexValue) {
            int decValue = Convert.ToInt32(hexValue, 16);
            return decValue;
        }

        public static string Decimal2Hex(int decValue) {
            string hexValue = decValue.ToString("X");
            return hexValue;
        }

        public static string Decimal2Binary(int decValue) {
            try {
                string binary = Convert.ToString(decValue, 2);
                return binary;

            }
            catch {
                return "Unable to convert!";
            }
        }

        public static int Binary2Decimal(string binValue) {
            try {
                return Convert.ToInt32(binValue, 2);
            }
            catch {
                return 0;
            }
        }
    }
}