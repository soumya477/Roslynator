// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CSharp.Refactorings
{
    internal static class ReturnTaskInsteadOfReturningNullRefactoring
    {
        public static async Task<Document> RefactorAsync(
            Document document,
            ConditionalAccessExpressionSyntax conditionalAccess,
            CancellationToken cancellationToken)
        {
            ExpressionSyntax expression = conditionalAccess.Expression;

            SemanticModel semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);

            int position = conditionalAccess.SpanStart;

            string localName = NameGenerator.Default.EnsureUniqueLocalName(DefaultNames.Variable, semanticModel, position);

            ITypeSymbol typeSymbol = semanticModel.GetTypeSymbol(expression, cancellationToken);

            TypeSyntax type = (typeSymbol?.SupportsExplicitDeclaration() == true)
                ? typeSymbol.ToMinimalTypeSyntax(semanticModel, position)
                : VarType();

            LocalDeclarationStatementSyntax localStatement = LocalDeclarationStatement(
                type,
                Identifier(localName).WithRenameAnnotation(),
                expression);

            InvocationExpressionSyntax newExpression = CreateTaskFromResultExpression(expression, semanticModel, cancellationToken);

            IfStatementSyntax ifStatement = IfStatement(
                CSharpSnippets.NotEqualsToNull(IdentifierName(localName)),
                Block(ExpressionStatement(conditionalAccess.RemoveOperatorToken())),
                ElseClause(Block(ExpressionStatement(newExpression))));

            SyntaxList<StatementSyntax> statements = List(new StatementSyntax[] { localStatement, ifStatement });

            if (conditionalAccess.Parent is ReturnStatementSyntax returnStatement)
            {
                statements = statements.WithTriviaFrom(returnStatement);

                if (returnStatement.IsEmbedded())
                {
                    return await document.ReplaceNodeAsync(returnStatement, Block(statements), cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    return await document.ReplaceNodeAsync(returnStatement, statements, cancellationToken).ConfigureAwait(false);
                }
            }

            var arrowExpressionClause = (ArrowExpressionClauseSyntax)conditionalAccess.Parent;

            SyntaxNode node = arrowExpressionClause.Parent;

            SyntaxNode newNode = CreateNewNode().WithFormatterAnnotation();

            return await document.ReplaceNodeAsync(node, newNode, cancellationToken).ConfigureAwait(false);

            SyntaxNode CreateNewNode()
            {
                switch (node)
                {
                    case MethodDeclarationSyntax methodDeclaration:
                        {
                            return methodDeclaration
                                .WithExpressionBody(null)
                                .WithBody(Block(statements));
                        }
                    case LocalFunctionStatementSyntax localFunction:
                        {
                            return localFunction
                                .WithExpressionBody(null)
                                .WithBody(Block(statements));
                        }
                    case PropertyDeclarationSyntax propertyDeclaration:
                        {
                            return propertyDeclaration
                                .WithExpressionBody(null)
                                .WithAccessorList(AccessorList(GetAccessorDeclaration(Block(statements))));
                        }
                    case IndexerDeclarationSyntax indexerDeclaration:
                        {
                            return indexerDeclaration
                                .WithExpressionBody(null)
                                .WithAccessorList(AccessorList(GetAccessorDeclaration(Block(statements))));
                        }
                    case AccessorDeclarationSyntax accessorDeclaration:
                        {
                            return accessorDeclaration
                                .WithExpressionBody(null)
                                .WithBody(Block(statements));
                        }
                    default:
                        {
                            Debug.Fail(node.Kind().ToString());
                            return node;
                        }
                }
            }
        }

        private static InvocationExpressionSyntax CreateTaskFromResultExpression(
            ExpressionSyntax expression,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            var typeSymbol = (INamedTypeSymbol)semanticModel.GetTypeInfo(expression, cancellationToken).ConvertedType;

            int position = expression.SpanStart;

            TypeSyntax type = typeSymbol.ToMinimalTypeSyntax(semanticModel, position);

            ExpressionSyntax defaultValue = typeSymbol.GetDefaultValueSyntax(type);

            return CreateTaskFromResultExpression(expression, type, defaultValue, position, semanticModel);
        }

        public static async Task<Document> RefactorAsync(
            Document document,
            ExpressionSyntax expression,
            CancellationToken cancellationToken)
        {
            SemanticModel semanticModel = await document.GetSemanticModelAsync().ConfigureAwait(false);

            var typeSymbol = (INamedTypeSymbol)semanticModel.GetTypeInfo(expression, cancellationToken).ConvertedType;

            int position = expression.SpanStart;

            ITypeSymbol typeArgument = typeSymbol.TypeArguments[0];

            TypeSyntax type = typeArgument.ToMinimalTypeSyntax(semanticModel, position);

            ExpressionSyntax defaultValue = typeArgument.GetDefaultValueSyntax(type);

            InvocationExpressionSyntax newExpression = CreateTaskFromResultExpression(expression, type, defaultValue, position, semanticModel);

            return await document.ReplaceNodeAsync(expression, newExpression, cancellationToken).ConfigureAwait(false);
        }

        private static InvocationExpressionSyntax CreateTaskFromResultExpression(
            ExpressionSyntax expression,
            TypeSyntax type,
            ExpressionSyntax defaultValue,
            int position,
            SemanticModel semanticModel)
        {
            SimpleNameSyntax name;

            if (defaultValue.IsKind(
                SyntaxKind.TrueLiteralExpression,
                SyntaxKind.FalseLiteralExpression,
                SyntaxKind.CharacterLiteralExpression,
                SyntaxKind.SimpleMemberAccessExpression,
                SyntaxKind.DefaultExpression))
            {
                name = IdentifierName("FromResult");
            }
            else
            {
                name = GenericName("FromResult", type);
            }

            INamedTypeSymbol taskOfTSymbol = semanticModel.GetTypeByMetadataName(MetadataNames.System_Threading_Tasks_Task);

            InvocationExpressionSyntax newNode = SimpleMemberInvocationExpression(
                taskOfTSymbol.ToMinimalTypeSyntax(semanticModel, position),
                name,
                Argument(defaultValue));

            return newNode.WithTriviaFrom(expression);
        }
    }
}
