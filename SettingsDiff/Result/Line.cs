using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace SettingsDiff.Result
{
    public class Line
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public EntryType EntryType { get; set; }

        public Line()
        {
            EntryType = EntryType.Unchanged;
        }
    }
}
