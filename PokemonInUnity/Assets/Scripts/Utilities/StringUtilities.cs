﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities
{
    public class StringUtilities
    {

        public static List<string> ParseAndSanitizeDialogueList(string[] strs)
        {
            List<string> news = new List<string>();
            foreach (string s in strs)
            {
                news.Add(ParseAndSanitizeDialogueString(s));
            }
            return news;
        }
        public static List<string> ParseAndSanitizeDialogueList(List<string> strs)
        {
            List<string> news = new List<string>();
            foreach(string s in strs)
            {
                news.Add(ParseAndSanitizeDialogueString(s));
            }
            return news;
        }

        public static string ParseAndSanitizeDialogueString(string old)
        {
            string str= old;
            str = str.Replace("@", GameInformation.GameManager.Manager.player.playerName);
            str = str.Replace("<PlayersName>", GameInformation.GameManager.Manager.player.playerName);
            return str;
        }


        public static List<string> FormatStringList(List<string> strings, params object[] objs)
        {
            List<string> replacedStrings = new List<string>();
            foreach (string s in strings) {
                string clean=String.Format(s, objs);
                replacedStrings.Add(clean);
            }
            return replacedStrings;
        }

        public static List<string> FormatStringList(List<string> strings, object objs)
        {
            List<string> replacedStrings = new List<string>();
            foreach (string s in strings)
            {
                string clean = String.Format(s, objs);
                replacedStrings.Add(clean);
            }
            return replacedStrings;
        }

        public static string[] FormatStringArray(string[] strings, params object[] objs)
        {
            List<string> replacedStrings = new List<string>();
            foreach (string s in strings)
            {
                string clean = String.Format(s, objs);
                replacedStrings.Add(clean);
            }
            return replacedStrings.ToArray();
        }

        public static string[] FormatStringArray(string[] strings, object objs)
        {
            List<string> replacedStrings = new List<string>();
            foreach (string s in strings)
            {
                string clean = String.Format(s, objs);
                replacedStrings.Add(clean);
            }
            return replacedStrings.ToArray();
        }
    }
}
