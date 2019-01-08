using System;
using System.Collections.Generic;
using System.Linq;
using SettingsDiff.Differ;
using SettingsDiff.Result;

namespace SettingsDiff
{
    public static class Worker
    {
        public static Result.Block Run(INode left, INode right)
        {
            // First we do a simple diff based on the children of each node
            Result.Block result = new Result.Block
            {
                Children = CreateResultNodeTree(left.Children, right.Children),
            };

            FindMovedBlocks(result);

            result.Render();

            return result;
        }

        public static List<Result.Block> CreateResultNodeTree(IEnumerable<INode> leftChildren, IEnumerable<INode> rightChildren)
        {
            LcsSolver solver = new LcsSolver(leftChildren, rightChildren);

            List<Result.Block> results = new List<Result.Block>();
            foreach (Differ.DiffResult result in solver.Results)
            {
                if (result.Left == null)
                {
                    results.Add(new Result.Block
                    {
                        Right = result.Right,
                        Children = Add(result.Right.Children),
                        EntryType = EntryType.Added,
                    });
                }
                else if (result.Right == null)
                {
                    results.Add(new Result.Block
                    {
                        Left = result.Left,
                        Children = Remove(result.Left.Children),
                        EntryType = EntryType.Removed,
                    });
                }
                else
                {
                    results.Add(new Result.Block
                    {
                        Left = result.Left,
                        Right = result.Right,
                        Children = CreateResultNodeTree(result.Left.Children, result.Right.Children),
                        EntryType = EntryType.Unchanged,
                    });
                }
            }

            return results;
        }

        public static List<Result.Block> Add(IEnumerable<INode> children)
        {
            List<Result.Block> results = new List<Result.Block>();

            foreach (INode node in children)
            {
                results.Add(new Result.Block
                {
                    Right = node,
                    Children = Add(node.Children),
                    EntryType = EntryType.Added,
                });
            }

            return results;
        }

        public static List<Result.Block> Remove(IEnumerable<INode> children)
        {
            List<Result.Block> results = new List<Result.Block>();

            foreach (INode node in children)
            {
                results.Add(new Result.Block
                {
                    Left = node,
                    Children = Remove(node.Children),
                    EntryType = EntryType.Removed,
                });
            }

            return results;
        }

        public static void FindMovedBlocks(Result.Block block)
        {
            foreach (Result.Block addedBlock in block.Children.Where(x => x.EntryType == EntryType.Added))
            {
                string nodeIdentity = addedBlock.Right.Identity.ToString();

                Block origin = block
                    .Children
                    .Where(x => x.EntryType == EntryType.Removed && x.Left.Identity.ToString() == nodeIdentity)
                    .FirstOrDefault();

                if (origin == null)
                {
                    continue;
                }

                // This Block has been moved!
                addedBlock.Left = origin.Left;
                addedBlock.EntryType = EntryType.Moved;
                addedBlock.Children = CreateResultNodeTree(addedBlock.Left.Children, addedBlock.Right.Children);

                MarkAsMoved(origin);
            }

            foreach (Block child in block.Children)
            {
                FindMovedBlocks(child);
            }
        }

        public static void MarkAsMoved(Result.Block block)
        {
            block.EntryType = EntryType.MovedOrigin;

            foreach (Block child in block.Children)
            {
                MarkAsMoved(child);
            }
        }
    }
}
