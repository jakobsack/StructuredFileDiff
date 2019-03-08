using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using StructuredFileDiff.Lib;

namespace StructuredFileDiff.Lib.Xml
{
    public static class ComparableNodeFactory
    {
        private static IComparableNodeFactory[] factories;

        static ComparableNodeFactory()
        {
            Type iComparableNodeFactory = typeof(IComparableNodeFactory);
            List<IComparableNodeFactory> newFactories = new List<IComparableNodeFactory>();

            IEnumerable<Type> types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => iComparableNodeFactory.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (Type type in types)
            {
                try
                {
                    IComparableNodeFactory factory = Activator.CreateInstance(type) as IComparableNodeFactory;
                    newFactories.Add(factory);
                }
                catch
                {
                    // NOOP
                }
            }

            // Sort by descending priority
            factories = newFactories
                .OrderByDescending(x => x.Priority)
                .ToArray();
        }

        public static IComparableNode AnalyzeNode(XmlNode node)
        {
            foreach (IComparableNodeFactory factory in factories)
            {
                IComparableNode comparableNode = factory.TryCreate(node);

                if (comparableNode == null)
                {
                    continue;
                }

                comparableNode.AnalyzeChildren();
                return comparableNode;
            }

            throw new Exception("Cannot convert node to IComparableNode");
        }
    }
}
