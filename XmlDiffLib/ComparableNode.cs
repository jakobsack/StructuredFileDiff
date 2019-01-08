using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SettingsDiff;

namespace XmlDiffLib
{
    public abstract class ComparableNode : IComparableNode, INode
    {
        public XmlNode BaseNode { get; protected set; }

        private string _name;
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    return BaseNode.Name;
                }

                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public List<INode> Children { get; } = new List<INode>();

        public ComparableNode()
        {
        }

        public ComparableNode(XmlNode node)
            : this()
        {
            BaseNode = node;
        }

        public virtual void AnalyzeChildren()
        {
            foreach (XmlNode childNode in BaseNode.UsefulChildren())
            {
                Children.Add(ComparableNodeFactory.AnalyzeNode(childNode));
            }
        }

        public virtual string IdentityValue
        {
            get
            {
                return BaseNode.InnerText;
            }
        }

        public NodeIdentity Identity
        {
            get
            {
                return new NodeIdentity()
                {
                    Class = GetType().ToString(),
                    Name = Name,
                    Value = IdentityValue,
                };
            }
        }

        public virtual List<SettingsDiff.Result.Line> RenderBefore(INode rightNode)
        {
            IComparableNode left = this;
            IComparableNode right = rightNode as IComparableNode;

            List<SettingsDiff.Result.Line> lines = new List<SettingsDiff.Result.Line>();
            if (left.Name != right.Name)
            {
                return ExpendedElementDiffLines(left, right);
            }

            Dictionary<string, string[]> attributes = GetAttributes(left, right);
            if (attributes.Any(x => x.Value.Any(y => y == null)))
            {
                return ExpendedElementDiffLines(left, right, attributes);
            }

            string leftString = "<" + left.Name;
            string rightString = "<" + right.Name;

            foreach (string key in attributes.Keys.OrderBy(x => x))
            {
                leftString += " " + RenderAttribute(key, attributes[key][0]);
                rightString = " " + RenderAttribute(key, attributes[key][1]);
            }

            leftString += ">";
            rightString += ">";

            return new List<SettingsDiff.Result.Line>{
                new SettingsDiff.Result.Line
                {
                    Left = leftString,
                    Right = rightString,
                }
            };
        }

        public virtual List<SettingsDiff.Result.Line> RenderAfter(INode rightNode)
        {
            IComparableNode left = this;
            IComparableNode right = rightNode as IComparableNode;

            return new List<SettingsDiff.Result.Line>{
                new SettingsDiff.Result.Line
                {
                    Left = "</" + left.BaseNode.Name + ">",
                    Right = "</" + right.BaseNode.Name + ">",
                },
            };
        }

        protected static List<SettingsDiff.Result.Line> ExpendedElementDiffLines(IComparableNode left, IComparableNode right, Dictionary<string, string[]> attributes = null)
        {
            List<SettingsDiff.Result.Line> lines = new List<SettingsDiff.Result.Line>();

            if (attributes == null)
            {
                attributes = GetAttributes(left, right);
            }

            lines.Add(new SettingsDiff.Result.Line
            {
                Left = "<" + left.BaseNode.Name,
                Right = "<" + right.BaseNode.Name,
            });

            foreach (string key in attributes.Keys.OrderBy(x => x))
            {
                lines.Add(new SettingsDiff.Result.Line
                {
                    Left = RenderAttribute(key, attributes[key][0]),
                    Right = RenderAttribute(key, attributes[key][1]),
                });
            }

            lines.Last().Left += ">";
            lines.Last().Right += ">";

            return lines;
        }

        protected static string RenderAttribute(string key, string value)
        {
            if (value == null)
            {
                return null;
            }

            return key + "=\"" + value + "\"";
        }

        protected static Dictionary<string, string[]> GetAttributes(IComparableNode left, IComparableNode right)
        {
            Dictionary<string, string[]> attributes = new Dictionary<string, string[]>();

            ExtractAttributes(left, attributes, 0);
            ExtractAttributes(right, attributes, 1);

            return attributes;
        }

        private static void ExtractAttributes(IComparableNode node, Dictionary<string, string[]> attributes, int position)
        {
            foreach (XmlAttribute attribute in node.BaseNode.Attributes)
            {
                if (!attributes.ContainsKey(attribute.Name))
                {
                    attributes[attribute.Name] = new string[2];
                }

                attributes[attribute.Name][position] = attribute.Value;
            }
        }
    }
}
