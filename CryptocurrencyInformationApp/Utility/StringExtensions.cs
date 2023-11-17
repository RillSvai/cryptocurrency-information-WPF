﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}