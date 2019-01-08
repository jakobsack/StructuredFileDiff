using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlDiffLib.ComparableNodes.IdentifiableNodes
{
    public class ForeignIdNode : IdentifiableNode
    {
        public ForeignIdNode()
            : base()
        {
        }

        public new class Factory : IdentifiableNode.Factory, IComparableNodeFactory
        {
            public IComparableNode TryCreate(XmlNode node)
            {
                return TryCreate<ForeignIdNode>(node, "FOREIGNID");
            }

            public int Priority
            {
                get
                {
                    return 80;
                }
            }
        }
    }
}
