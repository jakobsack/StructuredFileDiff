using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lib.Xml.ComparableNodes.IdentifiableNodes
{
    public class NameNode : IdentifiableNode
    {
        public NameNode()
            : base()
        {
        }

        public new class Factory : IdentifiableNode.Factory, IComparableNodeFactory
        {
            public IComparableNode TryCreate(XmlNode node)
            {
                return TryCreate<NameNode>(node, "NAME");
            }

            public int Priority
            {
                get
                {
                    return 50;
                }
            }
        }
    }
}
