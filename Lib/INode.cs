using System;
using System.Collections.Generic;
using System.Linq;

namespace StructuredFileDiff.Lib
{
    public interface INode
    {
        List<INode> Children { get; }

        NodeIdentity Identity { get; }

        List<Result.Line> RenderBefore(INode other);
        List<Result.Line> RenderAfter(INode other);
    }
}
