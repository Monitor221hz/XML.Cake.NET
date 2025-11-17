using System.Xml;
using System.Xml.Linq;

namespace XmlCake.Linq
{
    public interface IXPathLookup
    {
        bool AddTrackedNode(XmlReader reader, string path);
        bool AddTrackedNode(XmlReader reader, XPathTracker tracker);
        bool AddTrackedNode(XNode node, string path);
        bool AddTrackedNode(XNode node, XPathTracker tracker);
        string LookupPath(XNode node);
        IEnumerable<XNode> MapFromElement(XElement root);
        bool PathExists(XNode node);
        bool TryLookupPath(XNode node, out string? path);
    }
}