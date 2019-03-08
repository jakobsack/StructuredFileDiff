using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StructuredFileDiff.Lib.Xml.ComparableNodes
{
    public abstract class IdentifiableNode : ComparableNode
    {
        public string IdentifiableNodeName { get; protected set; }

        public IdentifiableNode()
            : base()
        {
        }

        public override string IdentityValue
        {
            get
            {
                return BaseNode.SelectSingleNode(IdentifiableNodeName).InnerText;
            }
        }

        public abstract class Factory
        {

            public IComparableNode TryCreate<T>(XmlNode node, string nodeName) where T : IdentifiableNode, new()
            {
                if (node.SelectSingleNode(nodeName) == null)
                {
                    return null;
                }

                return new T
                {
                    BaseNode = node,
                    IdentifiableNodeName = nodeName,
                };
            }
        }
    }
}
