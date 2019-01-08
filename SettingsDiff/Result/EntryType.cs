using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace SettingsDiff.Result
{
    public enum EntryType
    {
        Unchanged,
        Added,
        Removed,
        MovedOrigin,
        Moved,
        Changed,
    }
}
