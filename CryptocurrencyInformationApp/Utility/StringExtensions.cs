using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace CryptocurrencyInformationApp.Utility
{
    public static class StringExtensions
    {
        public static string GetLastWord(this string str, char[]? separators = null)
        {
            separators ??= new char [] {' '};
            string[] words = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return words.Any() ? words[^1] : "";
        }
        public static string ToCapital(this string str) 
        {
            if (string.IsNullOrEmpty(str.Trim()))
                return str;
            if (Regex.Count(str, @"[a-b][A-B]") == 1) 
            {
                str.ToUpper();
            }
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
        public static int? GetFirstNumber(this string str)
        {
            bool isContainNumber = int.TryParse(Regex.Match(str, @"[1-9][0-9]*").ToString(), out int res);
            if (isContainNumber) 
            {
                return res;
            }
            return null;
        }
        public static void OpenUrl(this string url) 
        {
            try
            {
                Process.Start(url!);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url!.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url!);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url!);
                }
                else
                {
                    throw new Exception("Something went wrong with navigation!");
                }
            }
        }
    }
}
