using System;
using System.Collections.Generic;
using System.Text;

namespace StructuredFileDiff.Lcs
{
    public class Streak<T>
    {
        public List<T> Content { get; set; }
        public StreakType StreakType { get; set; }
    }
}
