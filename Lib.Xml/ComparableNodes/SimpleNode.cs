using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using StructuredFileDiff.Lib;

namespace StructuredFileDiff.Lib.Xml.ComparableNodes
{
    public class SimpleNode : ComparableNode
    {
        public SimpleNode(XmlNode node)
            : base(node)
        {
        }

        public override List<StructuredFileDiff.Lib.Result.Line> RenderBefore(INode rightNode)
        {
            IComparableNode left = this;
            IComparableNode right = rightNode as IComparableNode;

            List<StructuredFileDiff.Lib.Result.Line> lines = new List<StructuredFileDiff.Lib.Result.Line>();
            if (left.Name != right.Name)
            {
                return RenderDetailledBefore(right);
            }

            Dictionary<string, string[]> attributes = GetAttributes(left, right);
            if (attributes.Any(x => x.Value.Any(y => y == null)))
            {
                return RenderDetailledBefore(right, attributes);
            }

            string leftString = "<" + left.Name;
            string rightString = "<" + right.Name;

            foreach (string key in attributes.Keys.OrderBy(x => x))
            {
                leftString += " " + RenderAttribute(key, attributes[key][0]);
                rightString = " " + RenderAttribute(key, attributes[key][1]);
            }

            leftString += ">" + left.BaseNode.InnerText;
            rightString += ">" + right.BaseNode.InnerText;
            leftString += "</" + left.Name + ">";
            rightString += "</" + right.Name + ">";

            return new List<StructuredFileDiff.Lib.Result.Line>{
                new StructuredFileDiff.Lib.Result.Line
                {
                    Left = leftString,
                    Right = rightString,
                }
            };
        }

        protected List<StructuredFileDiff.Lib.Result.Line> RenderDetailledBefore(IComparableNode right, Dictionary<string, string[]> attributes = null)
        {
            IComparableNode left = this;

            List<StructuredFileDiff.Lib.Result.Line> lines = ExpendedElementDiffLines(left, right, attributes);
            lines.Add(new StructuredFileDiff.Lib.Result.Line
            {
                Left = left.BaseNode.InnerText,
                Right = right.BaseNode.InnerText,
            });
            lines.Add(new StructuredFileDiff.Lib.Result.Line
            {
                Left = "</" + left.Name + ">",
                Right = "</" + right.Name + ">",
            });

            return lines;
        }

        public override List<StructuredFileDiff.Lib.Result.Line> RenderAfter(INode rightNode)
        {
            return new List<StructuredFileDiff.Lib.Result.Line>();
        }

        public class Factory : IComparableNodeFactory
        {
            public IComparableNode TryCreate(XmlNode node)
            {
                if (node.UsefulChildren().Any())
                {
                    return null;
                }

                return new SimpleNode(node);
            }

            public int Priority
            {
                get
                {
                    return 900;
                }
            }
        }
    }
}
