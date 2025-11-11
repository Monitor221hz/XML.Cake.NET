using System.Xml.Linq;

namespace XmlCake.Linq.Expressions;

public class XExpression : IXExpression
{
    public XExpression(List<IXStep> steps) => matchSteps = steps;

    public XExpression(params IXStep[] steps) => matchSteps = steps.ToList();

    public XMatch Match(IEnumerable<XNode> nodes)
    {
        int p = 0;
        List<XNode> buffer = new List<XNode>();
        foreach (XNode node in nodes)
        {
            if (!matchSteps[p].IsMatch(node))
            {
                buffer.Clear();
                p = 0;
                continue;
            }

            buffer.Add(node);
            p++;

            if (p == matchSteps.Count)
            {
                return new XMatch(buffer);
            }
        }
        return new XMatch();
    }

    public XMatchCollection Matches(IEnumerable<XNode> nodes)
    {
        int p = 0;
        List<XMatch> matchList = new List<XMatch>();
        List<XNode> buffer = new List<XNode>();
        foreach (XNode node in nodes)
        {
            if (!matchSteps[p].IsMatch(node))
            {
                buffer.Clear();
                p = 0;
                continue;
            }

            buffer.Add(node);
            p++;

            if (p == matchSteps.Count)
            {
                p = 0;
                matchList.Add(new XMatch(buffer));
                buffer.Clear();
            }
        }
        return new XMatchCollection(matchList);
    }

    public XMatchCollection Removes(IEnumerable<XNode> nodes)
    {
        throw new NotImplementedException();
    }

    private List<IXStep> matchSteps { get; set; } = new List<IXStep>();
}
