using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace StructuredFileDiff.Lib.Result
{
    public class Block
    {
        public List<Line> Before { get; set; }
        public List<Block> Children { get; set; }
        public List<Line> After { get; set; }
        public EntryType EntryType { get; set; }
        public INode Left { get; set; }
        public INode Right { get; set; }

        public Block()
        {
            Before = new List<Line>();
            Children = new List<Block>();
            After = new List<Line>();
            EntryType = EntryType.Unchanged;
        }

        public void Render()
        {
            foreach (Block child in Children)
            {
                child.Render();
            }

            if (Left == null && Right == null)
            {
                return;
            }

            INode right = Right == null ? Left : Right;
            INode left = Left == null ? Right : Left;

            Before = left.RenderBefore(right);
            After = left.RenderAfter(right);

            if (EntryType == EntryType.Added)
            {
                HasBeenAdded(Before);
                HasBeenAdded(After);
            }
            else if (EntryType == EntryType.Removed)
            {
                HasBeenRemoved(Before);
                HasBeenRemoved(After);
            }
            else if (EntryType == EntryType.MovedOrigin)
            {
                HasBeenMovedOrigin(Before);
                HasBeenMovedOrigin(After);
            }
            else if (EntryType == EntryType.Moved)
            {
                HasBeenMoved(Before);
                HasBeenMoved(After);
            }
            else
            {
                CheckForLineChanges(Before);
                CheckForLineChanges(After);
            }
        }

        private void CheckForLineChanges(List<Line> lines)
        {
            foreach (Line line in lines)
            {
                if (line.Left == line.Right)
                {
                    line.EntryType = EntryType.Unchanged;
                }
                else
                {
                    line.EntryType = EntryType.Changed;
                }
            }
        }

        private void HasBeenAdded(List<Line> lines)
        {
            foreach (Line line in lines)
            {
                line.Left = String.Empty;
                line.EntryType = EntryType.Added;
            }
        }

        private void HasBeenRemoved(List<Line> lines)
        {
            foreach (Line line in lines)
            {
                line.Right = String.Empty;
                line.EntryType = EntryType.Removed;
            }
        }

        private void HasBeenMovedOrigin(List<Line> lines)
        {
            foreach (Line line in lines)
            {
                line.Right = String.Empty;
                line.EntryType = EntryType.MovedOrigin;
            }
        }

        private void HasBeenMoved(List<Line> lines)
        {
            foreach (Line line in lines)
            {
                line.EntryType = EntryType.Moved;
            }
        }
    }
}
