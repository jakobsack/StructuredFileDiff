using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlDiffLib.ComparableNodes
{
    public class RootNode : ComparableNode
    {
        public RootNode(XmlNode node)
            : base(node)
        {
            Name = "Root";
        }
    }
}
