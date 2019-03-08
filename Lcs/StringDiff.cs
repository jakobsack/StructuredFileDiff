using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lcs
{
    public class StringDiff
    {
        public string Content { get; set; }
        public StreakType StreakType { get; set; }

        public static List<StringDiff> FindDifferences(string left, string right)
        {
            List<Streak<char>> result = Solver<char>.FindDifferences(left.ToCharArray(), right.ToCharArray());

            return result
                .Select(x => new StringDiff { Content = string.Join(string.Empty, x.Content), StreakType = x.StreakType })
                .ToList();
        }
    }
}
