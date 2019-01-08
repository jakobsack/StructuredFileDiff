using System;
using System.Xml;

namespace XmlDiffLib
{
    public interface IComparableNodeFactory
    {
        IComparableNode TryCreate(XmlNode node);

        int Priority { get; }
    }
}
