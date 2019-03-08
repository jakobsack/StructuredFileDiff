using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using StructuredFileDiff.Lib.Xml.ComparableNodes;
using StructuredFileDiff.Lib;

namespace StructuredFileDiff.Lib.Xml
{
    public class ComparableXmlFile
    {
        private IComparableNode rootNode;

        public ComparableXmlFile(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(fileName);

            rootNode = new RootNode(doc);
            rootNode.AnalyzeChildren();
        }

        public StructuredFileDiff.Lib.Result.Block DiffWith(ComparableXmlFile baseFile){
            return StructuredFileDiff.Lib.Worker.Run(rootNode, baseFile.rootNode);
        }
    }
}
