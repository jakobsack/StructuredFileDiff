using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SettingsDiff;

namespace XmlDiffLib
{
    public interface IComparableNode : INode
    {
        XmlNode BaseNode { get; }

        string Name { get; set;}
        string IdentityValue { get; }

        void AnalyzeChildren();
    }
}
