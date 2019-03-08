using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lcs
{
    public static class Solver<T>
    {
        public static List<Streak<T>> FindDifferences(T[] left, T[] right)
        {
            List<Streak<T>> streaks = new List<Streak<T>>();

            int[,] lcsMatrix = GetLongestCommonSubsequenceMatrix(left, right);
            GetDiffTreeFromBacktrackMatrix(lcsMatrix, streaks, left, right, left.Count(), right.Count());

            return streaks;
        }

        private static int[,] GetLongestCommonSubsequenceMatrix(T[] left, T[] right)
        {
            int[,] lcsMatrix = new int[left.Length + 1, right.Length + 1];

            for (int i = 0; i < left.Count(); i++)
            {
                for (int j = 0; j < right.Count(); j++)
                {
                    if (left[i].Equals(right[j]))
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

        public static void GetDiffTreeFromBacktrackMatrix(int[,] lcsMatrix, List<Streak<T>> streaks, T[] left, T[] right, int i, int j)
        {
            if (i > 0 && j > 0 && left[i - 1].Equals(right[j - 1]))
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

        public static void Add(List<Streak<T>> streaks, StreakType streakType, T content)
        {
            Streak<T> streak = streaks.LastOrDefault();
            if (streak == null || streak.StreakType != streakType)
            {
                streaks.Add(new Streak<T>
                {
                    Content = new List<T> { content },
                    StreakType = streakType,
                });
            }
            else
            {
                streak.Content.Add(content);
            }
        }
    }
}
