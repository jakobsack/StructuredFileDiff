using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlDiffLib.ComparableNodes
{
    public class CommonNode : ComparableNode
    {
       public CommonNode(XmlNode node)
            : base(node)
        {
        }

        public class Factory : IComparableNodeFactory
        {
            public IComparableNode TryCreate(XmlNode node)
            {
                return new CommonNode(node);
            }

            public int Priority
            {
                get
                {
                    return 0;
                }
            }
        }
    }
}
