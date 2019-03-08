using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lib.Differ
{
    public class LcsSolver
    {
        private int[,] lcsMatrix;
        private ReducedNode[] left;
        private ReducedNode[] right;
        public List<DiffResult> Results { get; set; }

        public LcsSolver(IEnumerable<INode> leftItems, IEnumerable<INode> rightItems)
        {
            lcsMatrix = new int[leftItems.Count() + 1, rightItems.Count() + 1];

            left = leftItems.Select(x => new ReducedNode(x)).ToArray();
            right = rightItems.Select(x => new ReducedNode(x)).ToArray();

            Results = new List<DiffResult>();

            ReduceDiffIdentities();
            GetLongestCommonSubsequenceMatrix();
            GetDiffTreeFromBacktrackMatrix(left.Count(), right.Count());
        }

        private void ReduceDiffIdentities()
        {
            List<NodeIdentity> identities = left
                .Select(x => x.Identity)
                .ToList();
            identities.AddRange(right
                .Select(x => x.Identity));

            foreach (NodeIdentity identity in identities)
            {
                if (left.Count(x => identity.Class == x.Identity.Class && identity.Name == x.Identity.Name) <= 1
                && right.Count(x => identity.Class == x.Identity.Class && identity.Name == x.Identity.Name) <= 1)
                {
                    identity.Value = String.Empty;
                }
            }
        }

        private void GetLongestCommonSubsequenceMatrix()
        {
            for (int i = 0; i < left.Length; i++)
            {
                for (int j = 0; j < right.Length; j++)
                {
                    string compareValueOriginal = left[i].Identity.ToString();
                    string compareValueChanged = right[j].Identity.ToString();

                    if (compareValueOriginal == compareValueChanged)
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
        }

        public void GetDiffTreeFromBacktrackMatrix(int i, int j)
        {
            if (i > 0 && j > 0 && left[i - 1].Identity.ToString() == right[j - 1].Identity.ToString())
            {
                GetDiffTreeFromBacktrackMatrix(i - 1, j - 1);
                Results.Add(new DiffResult
                {
                    Left = left[i - 1].Node,
                    Right = right[j - 1].Node,
                });
            }
            else
            {
                if (j > 0 && (i == 0 || lcsMatrix[i, j - 1] >= lcsMatrix[i - 1, j]))
                {
                    GetDiffTreeFromBacktrackMatrix(i, j - 1);
                    Results.Add(new DiffResult
                    {
                        Right = right[j - 1].Node,
                    });
                }
                else if (i > 0 && (j == 0 || lcsMatrix[i, j - 1] < lcsMatrix[i - 1, j]))
                {
                    GetDiffTreeFromBacktrackMatrix(i - 1, j);
                    Results.Add(new DiffResult
                    {
                        Left = left[i - 1].Node,
                    });
                }
            }
        }

        private class ReducedNode
        {
            public NodeIdentity Identity { get; set; }
            public INode Node { get; set; }

            public ReducedNode(INode node)
            {
                Node = node;
                Identity = new NodeIdentity(node.Identity);
            }
        }
    }
}
