using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lib.Xml.ComparableNodes.IdentifiableNodes
{
    public class IdNode : IdentifiableNode
    {
        public IdNode()
            : base()
        {
        }

        public new class Factory : IdentifiableNode.Factory, IComparableNodeFactory
        {
            public IComparableNode TryCreate(XmlNode node)
            {
                return TryCreate<IdNode>(node, "ID");
            }

            public int Priority
            {
                get
                {
                    return 1000;
                }
            }
        }
    }
}
