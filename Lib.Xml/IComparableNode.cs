using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using StructuredFileDiff.Lib;

namespace StructuredFileDiff.Lib.Xml
{
    public interface IComparableNode : INode
    {
        XmlNode BaseNode { get; }

        string Name { get; set;}
        string IdentityValue { get; }

        void AnalyzeChildren();
    }
}
