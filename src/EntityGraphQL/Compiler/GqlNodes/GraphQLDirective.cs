using System.Collections.Generic;
using System.Linq.Expressions;
using EntityGraphQL.Directives;
using EntityGraphQL.Extensions;
using EntityGraphQL.Schema;

namespace EntityGraphQL.Compiler;

public class GraphQLDirective
{
    private readonly IDirectiveProcessor processor;
    private readonly Dictionary<string, object> inlineArgValues;
    private readonly string name;

    public GraphQLDirective(string name, IDirectiveProcessor processor, Dictionary<string, object> inlineArgValues)
    {
        this.processor = processor;
        this.inlineArgValues = inlineArgValues;
        this.name = name;
    }

    public Expression? Process(ISchemaProvider schema, Expression fieldExpression, IReadOnlyDictionary<string, object> args, ParameterExpression? docParam, object? docVariables)
    {
        var validationErrors = new List<string>();
        var arguments = ArgumentUtil.BuildArgumentsObject(schema, name, null, inlineArgValues.MergeNew(args), processor.GetArguments(schema), processor.GetArgumentsType(), docParam, docVariables, validationErrors);

        if (validationErrors.Count > 0)
        {
            throw new EntityGraphQLValidationException(validationErrors);
        }

        return processor.ProcessExpression(fieldExpression, arguments);
    }

    public BaseGraphQLField? ProcessField(ISchemaProvider schema, BaseGraphQLField field, IReadOnlyDictionary<string, object> args, ParameterExpression? docParam, object? docVariables)
    {
        var validationErrors = new List<string>();
        var arguments = ArgumentUtil.BuildArgumentsObject(schema, name, null, inlineArgValues.MergeNew(args), processor.GetArguments(schema), processor.GetArgumentsType(), docParam, docVariables, validationErrors);

        if (validationErrors.Count > 0)
        {
            throw new EntityGraphQLValidationException(validationErrors);
        }

        return processor.ProcessField(field, arguments);
    }
}