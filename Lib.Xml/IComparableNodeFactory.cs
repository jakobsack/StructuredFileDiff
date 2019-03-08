using System;
using System.Xml;

namespace StructuredFileDiff.Lib.Xml
{
    public interface IComparableNodeFactory
    {
        IComparableNode TryCreate(XmlNode node);

        int Priority { get; }
    }
}
