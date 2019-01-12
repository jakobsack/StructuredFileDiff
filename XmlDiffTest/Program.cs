using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using XmlDiffLib;
using SettingsDiff.Result;
using System.Reflection;
using Scriban;
using TextDiff;

namespace XmlDiffTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ComparableXmlFile originalFile = new ComparableXmlFile(args[0]);
            ComparableXmlFile changedFile = new ComparableXmlFile(args[1]);

            Block diff = originalFile.DiffWith(changedFile);

            Assembly assembly = typeof(XmlDiffTest.Program).Assembly;
            string templateString = ReadResource(assembly, "XmlDiffTest.Resources.template.html");
            string rowString = ReadResource(assembly, "XmlDiffTest.Resources.row.html");

            Template htmlTemplate = Template.Parse(templateString);
            Template rowTemplate = Template.Parse(rowString);

            string tableContent = CreateTable(rowTemplate, diff, String.Empty, String.Empty);

            string output = htmlTemplate.Render(new
            {
                tableContent = tableContent,
            });
            Console.WriteLine(output);
        }

        private static string ReadResource(Assembly assembly, string resourceName)
        {
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader resourceReader = new StreamReader(resourceStream))
            {
                return resourceReader.ReadToEnd();
            }
        }

        private static string CreateTable(Template rowTemplate, Block diff, string oldIndentBegin, string oldIndentEnd)
        {
            string newIndentBegin = oldIndentBegin + "<span class=\"block block-" + diff.EntryType.ToString().ToLower() + "\">";
            string newIndentEnd = oldIndentEnd + "</span>";

            string output = "";
            output += RenderRows(rowTemplate, diff.Before, newIndentBegin, newIndentEnd);
            foreach (Block child in diff.Children)
            {
                output += CreateTable(rowTemplate, child, newIndentBegin, newIndentEnd);
            }
            output += RenderRows(rowTemplate, diff.After, newIndentBegin, newIndentEnd);

            output += "";
            return output;
        }

        private static string RenderRows(Template rowTemplate, List<Line> lines, string indentBegin, string indentEnd)
        {
            string output = "";
            foreach (var line in lines)
            {
                string left = XmlEscape(line.Left);
                string right = XmlEscape(line.Right);
                if (line.EntryType == EntryType.Changed)
                {
                    List<Streak> streaks = Streak.FindDifferences(left, right);
                    left = RenderStreaks(streaks, StreakType.Unchanged, StreakType.Removed);
                    right = RenderStreaks(streaks, StreakType.Unchanged, StreakType.Added);
                }

                output += rowTemplate.Render(new
                {
                    LineType = line.EntryType.ToString(),
                    LineTypeLc = line.EntryType.ToString().ToLower(),
                    IndentBegin = indentBegin,
                    IndentEnd = indentEnd,
                    Left = left,
                    Right = right,
                });
            }
            return output;
        }

        private static string RenderStreaks(List<Streak> streaks, params StreakType[] allowedStreakTypes)
        {
            string output = String.Empty;
            foreach (Streak streak in streaks.Where(x => allowedStreakTypes.Contains(x.StreakType)))
            {
                output += "<span class=\"char-" + streak.StreakType.ToString().ToLower() + "\">";
                output += streak.Content;
                output += "</span>";
            }
            return output;
        }

        public static string XmlEscape(string unescaped)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("root");
            node.InnerText = unescaped;
            return node.InnerXml;
        }

        public static string XmlUnescape(string escaped)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("root");
            node.InnerXml = escaped;
            return node.InnerText;
        }
    }
}
