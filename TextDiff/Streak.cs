using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace TextDiff
{
    public class Streak
    {
        public string Content { get; set; }
        public StreakType StreakType { get; set; }


        public static List<Streak> FindDifferences(string left, string right)
        {
            List<Streak> streaks = new List<Streak>();

            int[,] lcsMatrix = GetLongestCommonSubsequenceMatrix(left, right);
            GetDiffTreeFromBacktrackMatrix(lcsMatrix, streaks, left, right, left.Length, right.Length);

            return streaks;
        }

        private static int[,] GetLongestCommonSubsequenceMatrix(string left, string right)
        {
            int[,] lcsMatrix = new int[left.Count() + 1, right.Count() + 1];

            for (int i = 0; i < left.Length; i++)
            {
                for (int j = 0; j < right.Length; j++)
                {
                    if (left[i] == right[j])
                    {
                        lcsMatrix[i + 1, j + 1] = lcsMatrix[i, j] + 1;
                    }
                    else if (lcsMatrix[i, j + 1] > lcsMatrix[i + 1, j])
                    {
                        lcsMatrix[i + 1, j + 1] = lcsMatrix[i, j + 1];
                    }
                    else
                    {
                        lcsMatrix[i + 1, j + 1] = lcsMatrix[i + 1, j];
                    }
                }
            }

            return lcsMatrix;
        }

        public static void GetDiffTreeFromBacktrackMatrix(int[,] lcsMatrix, List<Streak> streaks, string left, string right, int i, int j)
        {
            if (i > 0 && j > 0 && left[i - 1] == right[j - 1])
            {
                GetDiffTreeFromBacktrackMatrix(lcsMatrix, streaks, left, right, i - 1, j - 1);
                Add(streaks, StreakType.Unchanged, left[i - 1]);
            }
            else
            {
                if (j > 0 && (i == 0 || lcsMatrix[i, j - 1] >= lcsMatrix[i - 1, j]))
                {
                    GetDiffTreeFromBacktrackMatrix(lcsMatrix, streaks, left, right, i, j - 1);
                    Add(streaks, StreakType.Added, right[j - 1]);
                }
                else if (i > 0 && (j == 0 || lcsMatrix[i, j - 1] < lcsMatrix[i - 1, j]))
                {
                    GetDiffTreeFromBacktrackMatrix(lcsMatrix, streaks, left, right, i - 1, j);
                    Add(streaks, StreakType.Removed, left[i - 1]);
                }
            }
        }

        public static void Add(List<Streak> streaks, StreakType streakType, char content)
        {
            Streak streak = streaks.LastOrDefault();
            if (streak == null || streak.StreakType != streakType)
            {
                streaks.Add(new Streak
                {
                    Content = content.ToString(),
                    StreakType = streakType,
                });
            }
            else
            {
                streak.Content += content;
            }
        }
    }

    public enum StreakType
    {
        Unchanged,
        Added,
        Removed,
    }
}
