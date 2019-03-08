using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lib.Xml
{
    public static class Extensions
    {
        private static XmlNodeType[] usefulNodeTypes = new[] { XmlNodeType.Element };

        public static List<XmlNode> UsefulChildren(this XmlNode node)
        {
            List<XmlNode> usefulChildren = new List<XmlNode>();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (!usefulNodeTypes.Contains(childNode.NodeType))
                {
                    continue;
                }

                usefulChildren.Add(childNode);
            }

            return usefulChildren;
        }
    }
};
