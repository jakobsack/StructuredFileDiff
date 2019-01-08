using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using XmlDiffLib.ComparableNodes;
using SettingsDiff;

namespace XmlDiffLib
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

        public SettingsDiff.Result.Block DiffWith(ComparableXmlFile baseFile){
            return SettingsDiff.Worker.Run(rootNode, baseFile.rootNode);
        }
    }
}
