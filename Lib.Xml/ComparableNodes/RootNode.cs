using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lib.Xml.ComparableNodes
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
