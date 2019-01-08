using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SettingsDiff;

namespace XmlDiffLib.ComparableNodes
{
    public class ListNode : ComparableNode
    {
        public ListNode(XmlNode node)
            : base(node)
        {
        }

        public override string IdentityValue
        {
            get
            {
                return BaseNode.InnerText;
            }
        }

        public override void AnalyzeChildren()
        {
            List<XmlNode> usefulChildren = BaseNode.UsefulChildren();
            Children.Add(ComparableNodeFactory.AnalyzeNode(usefulChildren.First()));
            foreach (XmlNode node in usefulChildren.Skip(1))
            {
                IComparableNode child = ComparableNodeFactory.AnalyzeNode(node);
                child.Name = "ITEM";
                Children.Add(child);
            }
        }

        public class Factory : IComparableNodeFactory
        {
            public IComparableNode TryCreate(XmlNode node)
            {
                List<XmlNode> children = node.UsefulChildren();
                if (!children.Any())
                {
                    return null;
                }

                if (children.First().Name != "NUMBER")
                {
                    return null;
                }

                foreach(XmlNode child in children.Skip(1))
                {
                    if (!child.Name.StartsWith("NO"))
                    {
                        return null;
                    }
                }

                return new ListNode(node);
            }

            public int Priority
            {
                get
                {
                    return 60;
                }
            }
        }
    }
}
