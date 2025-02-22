using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EntityGraphQL.Schema;

namespace EntityGraphQL.Compiler
{
    public class GraphQLFragmentStatement : IGraphQLNode
    {
        public Expression? NextFieldContext { get; }
        public IGraphQLNode? ParentNode { get; }
        public ParameterExpression? RootParameter { get; }

        public IField? Field { get; }
        public bool HasServices { get => Field?.Services.Any() == true; }

        public IReadOnlyDictionary<string, object> Arguments { get; }

        public string Name { get; }

        public List<BaseGraphQLField> QueryFields { get; } = new List<BaseGraphQLField>();

        public GraphQLFragmentStatement(string name, ParameterExpression selectContext, ParameterExpression rootParameter)
        {
            Name = name;
            NextFieldContext = selectContext;
            RootParameter = rootParameter;
            Arguments = new Dictionary<string, object>();
        }

        public GraphQLFragmentStatement(GraphQLFragmentStatement context, ParameterExpression? nextFieldContext)
        {
            Name = context.Name;
            RootParameter = context.RootParameter;
            NextFieldContext = nextFieldContext;
            Arguments = new Dictionary<string, object>();
        }

        public void AddField(BaseGraphQLField field)
        {
            QueryFields.Add(field);
        }
    }
}