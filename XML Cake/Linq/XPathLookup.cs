using System.Xml;
using System.Xml.Linq;

namespace XmlCake.Linq;


public class XPathLookup
{

	public XPathLookup() { }

	Dictionary<XNode, string> nodePaths = new Dictionary<XNode, string>();


	public IEnumerable<XNode> MapFromElement(XElement root)
	{
		XPathTracker tracker = new XPathTracker();



		IEnumerable<XNode> nodes = root.DescendantNodes();

		tracker.ResolvePath(root);
		foreach (XNode node in nodes)
		{
			tracker.ResolvePath(node);

			AddTrackedNode(node, tracker.GetCurrentPathByElement(node));
		}
		return nodes;
	}


	public string LookupPath(XNode node) => nodePaths[node];

	public bool TryLookupPath(XNode node, out string? path) => nodePaths.TryGetValue(node, out path);

	public bool PathExists(XNode node) => nodePaths.ContainsKey(node);


	public bool AddTrackedNode(XmlReader reader, string path)
	{

		switch (reader.NodeType)
		{
			case XmlNodeType.EndElement:
				return false;
			case XmlNodeType.Whitespace:
				return false;
			default:
				XNode node = XNode.ReadFrom(reader);

				nodePaths.Add(node, path);
				return true;
		}

	}

	public bool AddTrackedNode(XmlReader reader, XPathTracker tracker) => AddTrackedNode(reader, tracker.GetCurrentPath(reader));

	public bool AddTrackedNode(XNode node, string path)
	{
		lock (nodePaths)
			lock (node)
			{
				switch (node.NodeType)
				{
					case XmlNodeType.EndElement:
						return false;
					case XmlNodeType.Whitespace:
						return false;
					default:
						nodePaths.Add(node, path);
						return true;
				}
			}

	}

	public bool AddTrackedNode(XNode node, XPathTracker tracker) => AddTrackedNode(node, tracker.GetCurrentPathByElement(node));
}