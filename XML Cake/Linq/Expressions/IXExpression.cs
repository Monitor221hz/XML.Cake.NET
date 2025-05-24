
using System.Xml.Linq;

namespace XmlCake.Linq.Expressions;

public interface IXExpression
{
	public XMatchCollection Matches(IEnumerable<XNode> nodes);
	public XMatch Match(IEnumerable<XNode> nodes);

	public XMatchCollection Removes(IEnumerable<XNode> nodes);

}


