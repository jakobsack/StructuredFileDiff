using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SettingsDiff.Differ
{
    public class DiffResult
    {
            public INode Left { get; set; }
            public INode Right { get; set; }
    }
}
