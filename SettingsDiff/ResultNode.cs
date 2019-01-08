using System;
using System.Collections.Generic;
using System.Linq;

namespace SettingsDiff
{
    public class ResultNode
    {
        public INode Left { get; set; }
        public INode Right { get; set; }

        public List<ResultNode> Children  { get; set; }
    }
}
