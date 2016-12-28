﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp.Refactorings
{
    internal static class AsynchronousMethodNameShouldEndWithAsyncRefactoring
    {
        private const string AsyncSuffix = "Async";

        public static void Analyze(SyntaxNodeAnalysisContext context, MethodDeclarationSyntax methodDeclaration)
        {
            IMethodSymbol methodSymbol = context.SemanticModel.GetDeclaredSymbol(methodDeclaration, context.CancellationToken);

            if (methodSymbol?.IsAsync == true
                && !methodSymbol.Name.EndsWith(AsyncSuffix, StringComparison.Ordinal)
                && ContainsAwait(methodDeclaration))
            {
                context.ReportDiagnostic(
                    DiagnosticDescriptors.AsynchronousMethodNameShouldEndWithAsync,
                    methodDeclaration.Identifier.GetLocation());
            }
        }

        private static bool ContainsAwait(MethodDeclarationSyntax methodDeclaration)
        {
            return methodDeclaration
                .DescendantNodes(node => !CSharpUtility.IsMethodInsideMethod(node))
                .Any(f => f.IsKind(SyntaxKind.AwaitExpression));
        }
    }
}