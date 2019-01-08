using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace SettingsDiff
{
    public class NodeIdentity
    {
        public string Class { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public NodeIdentity()
        {
            Class = String.Empty;
            Name = String.Empty;
            Value = String.Empty;
        }

        public NodeIdentity(NodeIdentity clone)
        {
            Class = clone.Class;
            Name = clone.Name;
            Value = clone.Value;
        }

        public override string ToString()
        {
            return Class + "::" + Name + "::" + Value;
        }
    }
}
