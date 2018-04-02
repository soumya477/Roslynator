// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslynator.CSharp
{
    public abstract class ModifierList<TNode> where TNode : SyntaxNode
    {
        internal ModifierList()
        {
        }

        internal abstract SyntaxTokenList GetModifiers(TNode node);

        internal abstract TNode WithModifiers(TNode node, SyntaxTokenList modifiers);

        public static ModifierList<TNode> Instance { get; } = (ModifierList<TNode>)GetInstance();

        private static object GetInstance()
        {
            if (typeof(TNode) == typeof(ClassDeclarationSyntax))
                return new ClassDeclarationModifierList();

            if (typeof(TNode) == typeof(ConstructorDeclarationSyntax))
                return new ConstructorDeclarationModifierList();

            if (typeof(TNode) == typeof(ConversionOperatorDeclarationSyntax))
                return new ConversionOperatorDeclarationModifierList();

            if (typeof(TNode) == typeof(DelegateDeclarationSyntax))
                return new DelegateDeclarationModifierList();

            if (typeof(TNode) == typeof(DestructorDeclarationSyntax))
                return new DestructorDeclarationModifierList();

            if (typeof(TNode) == typeof(EnumDeclarationSyntax))
                return new EnumDeclarationModifierList();

            if (typeof(TNode) == typeof(EventDeclarationSyntax))
                return new EventDeclarationModifierList();

            if (typeof(TNode) == typeof(EventFieldDeclarationSyntax))
                return new EventFieldDeclarationModifierList();

            if (typeof(TNode) == typeof(FieldDeclarationSyntax))
                return new FieldDeclarationModifierList();

            if (typeof(TNode) == typeof(IndexerDeclarationSyntax))
                return new IndexerDeclarationModifierList();

            if (typeof(TNode) == typeof(InterfaceDeclarationSyntax))
                return new InterfaceDeclarationModifierList();

            if (typeof(TNode) == typeof(MethodDeclarationSyntax))
                return new MethodDeclarationModifierList();

            if (typeof(TNode) == typeof(OperatorDeclarationSyntax))
                return new OperatorDeclarationModifierList();

            if (typeof(TNode) == typeof(PropertyDeclarationSyntax))
                return new PropertyDeclarationModifierList();

            if (typeof(TNode) == typeof(StructDeclarationSyntax))
                return new StructDeclarationModifierList();

            if (typeof(TNode) == typeof(AccessorDeclarationSyntax))
                return new AccessorDeclarationModifierList();

            if (typeof(TNode) == typeof(LocalDeclarationStatementSyntax))
                return new LocalDeclarationStatementModifierList();

            if (typeof(TNode) == typeof(LocalFunctionStatementSyntax))
                return new LocalFunctionStatementModifierList();

            if (typeof(TNode) == typeof(ParameterSyntax))
                return new ParameterModifierList();

            if (typeof(TNode) == typeof(IncompleteMemberSyntax))
                return new IncompleteMemberModifierList();

            throw new InvalidOperationException();
        }

        public TNode Insert(TNode node, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            SyntaxTokenList modifiers = GetModifiers(node);

            int index = ModifierList.GetInsertIndex(modifiers, kind, comparer);

            return InsertModifier(node, modifiers, Token(kind), index);
        }

        public TNode Insert(TNode node, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            SyntaxTokenList modifiers = GetModifiers(node);

            int index = ModifierList.GetInsertIndex(modifiers, modifier, comparer);

            return InsertModifier(node, modifiers, modifier, index);
        }

        private TNode InsertModifier(TNode node, SyntaxTokenList modifiers, SyntaxToken modifier, int index)
        {
            var token = default(SyntaxToken);

            if (!modifiers.Any()
                || index == modifiers.Count)
            {
                //TODO: test
                token = (modifiers.Any())
                    ? node.FindToken(modifiers.FullSpan.End)
                    : node.GetFirstToken();
            }
            else
            {
                token = modifiers[index];
            }

            if (token != default(SyntaxToken))
            {
                SyntaxTriviaList newLeadingTrivia = token.LeadingTrivia;

                if (newLeadingTrivia.Any())
                {
                    SyntaxTriviaList leadingTrivia = modifier.LeadingTrivia;

                    if (!leadingTrivia.IsSingleElasticMarker())
                        newLeadingTrivia = newLeadingTrivia.AddRange(leadingTrivia);

                    modifier = modifier.WithLeadingTrivia(newLeadingTrivia);

                    SyntaxToken newToken = token.WithoutLeadingTrivia();

                    if (!modifiers.Any()
                        || index == modifiers.Count)
                    {
                        node = node.ReplaceToken(token, newToken);
                    }
                    else
                    {
                        modifiers = modifiers.ReplaceAt(index, newToken);
                    }
                }

                if (modifier.TrailingTrivia.IsSingleElasticMarker())
                    modifier = modifier.WithTrailingTrivia(TriviaList(Space));
            }

            modifiers = modifiers.Insert(index, modifier);

            return WithModifiers(node, modifiers);
        }

        public TNode Remove(TNode node, SyntaxKind kind)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            SyntaxTokenList modifiers = GetModifiers(node);

            int i = modifiers.IndexOf(kind);

            if (i != -1)
            {
                return Remove(node, modifiers, modifiers[i], i);
            }
            else
            {
                return node;
            }
        }

        public TNode Remove(TNode node, SyntaxToken modifier)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            SyntaxTokenList modifiers = GetModifiers(node);

            int i = modifiers.IndexOf(modifier);

            if (i != -1)
            {
                return Remove(node, modifiers, modifier, i);
            }
            else
            {
                return node;
            }
        }

        public TNode RemoveAt(TNode node, int index)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            SyntaxTokenList modifiers = GetModifiers(node);

            return Remove(node, modifiers, modifiers[index], index);
        }

        private TNode Remove(
            TNode node,
            SyntaxTokenList modifiers,
            SyntaxToken modifier,
            int index)
        {
            SyntaxTriviaList leading = modifier.LeadingTrivia;
            SyntaxTriviaList trailing = modifier.TrailingTrivia;

            if (modifiers.Count == 1)
            {
                SyntaxToken nextToken = modifier.GetNextToken();

                if (!nextToken.IsKind(SyntaxKind.None))
                {
                    SyntaxTriviaList trivia = AddIfNotEmptyOrWhitespace(leading, trailing, nextToken.LeadingTrivia);

                    node = node.ReplaceToken(nextToken, nextToken.WithLeadingTrivia(trivia));
                }
                else
                {
                    SyntaxToken previousToken = modifier.GetPreviousToken();

                    if (!previousToken.IsKind(SyntaxKind.None))
                    {
                        SyntaxTriviaList trivia = AddIfNotEmptyOrWhitespace(previousToken.TrailingTrivia, leading, trailing);

                        node = node.ReplaceToken(previousToken, previousToken.WithTrailingTrivia(trivia));
                    }
                }
            }
            else if (index == 0)
            {
                SyntaxToken nextModifier = modifiers[index + 1];

                SyntaxTriviaList trivia = AddIfNotEmptyOrWhitespace(leading, trailing, nextModifier.LeadingTrivia);

                modifiers = modifiers.Replace(nextModifier, nextModifier.WithLeadingTrivia(trivia));
            }
            else
            {
                SyntaxToken previousModifier = modifiers[index - 1];

                SyntaxTriviaList trivia = AddIfNotEmptyOrWhitespace(previousModifier.TrailingTrivia, leading, trailing);

                modifiers = modifiers.Replace(previousModifier, previousModifier.WithTrailingTrivia(trivia));
            }

            modifiers = modifiers.RemoveAt(index);

            return WithModifiers(node, modifiers);
        }

        private static SyntaxTriviaList AddIfNotEmptyOrWhitespace(SyntaxTriviaList trivia, SyntaxTriviaList triviaToAdd)
        {
            return (triviaToAdd.IsEmptyOrWhitespace()) ? trivia : trivia.AddRange(triviaToAdd);
        }

        private static SyntaxTriviaList AddIfNotEmptyOrWhitespace(SyntaxTriviaList trivia, SyntaxTriviaList triviaToAdd1, SyntaxTriviaList triviaToAdd2)
        {
            trivia = AddIfNotEmptyOrWhitespace(trivia, triviaToAdd1);

            return AddIfNotEmptyOrWhitespace(trivia, triviaToAdd2);
        }

        public TNode RemoveAll(TNode node)
        {
            SyntaxTokenList modifiers = GetModifiers(node);

            if (!modifiers.Any())
                return node;

            SyntaxToken firstModifier = modifiers.First();

            if (modifiers.Count == 1)
                return Remove(node, firstModifier);

            SyntaxToken nextToken = modifiers.Last().GetNextToken();

            if (!nextToken.IsKind(SyntaxKind.None))
            {
                SyntaxTriviaList trivia = firstModifier.LeadingTrivia;

                trivia = trivia.AddRange(firstModifier.TrailingTrivia.EmptyIfWhitespace());

                for (int i = 1; i < modifiers.Count; i++)
                    trivia = trivia.AddRange(modifiers[i].LeadingAndTrailingTrivia().EmptyIfWhitespace());

                trivia = trivia.AddRange(nextToken.LeadingTrivia.EmptyIfWhitespace());

                node = node.ReplaceToken(nextToken, nextToken.WithLeadingTrivia(trivia));
            }
            else
            {
                SyntaxToken previousToken = firstModifier.GetPreviousToken();

                if (!previousToken.IsKind(SyntaxKind.None))
                {
                    SyntaxTriviaList trivia = firstModifier.LeadingAndTrailingTrivia();

                    for (int i = 1; i < modifiers.Count; i++)
                        trivia = trivia.AddRange(modifiers[i].LeadingAndTrailingTrivia().EmptyIfWhitespace());

                    node = node.ReplaceToken(nextToken, nextToken.AppendToTrailingTrivia(trivia));
                }
            }

            return WithModifiers(node, default(SyntaxTokenList));
        }

        public TNode RemoveAll(TNode node, Func<SyntaxToken, bool> predicate)
        {
            SyntaxTokenList modifiers = GetModifiers(node);

            for (int i = modifiers.Count - 1; i >= 0; i--)
            {
                SyntaxToken modifier = modifiers[i];

                if (predicate(modifier))
                    node = Remove(node, modifiers, modifier, i);
            }

            return node;
        }

        private class AccessorDeclarationModifierList : ModifierList<AccessorDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(AccessorDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override AccessorDeclarationSyntax WithModifiers(AccessorDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class ClassDeclarationModifierList : ModifierList<ClassDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(ClassDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override ClassDeclarationSyntax WithModifiers(ClassDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class ConstructorDeclarationModifierList : ModifierList<ConstructorDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(ConstructorDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override ConstructorDeclarationSyntax WithModifiers(ConstructorDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class ConversionOperatorDeclarationModifierList : ModifierList<ConversionOperatorDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(ConversionOperatorDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override ConversionOperatorDeclarationSyntax WithModifiers(ConversionOperatorDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class DelegateDeclarationModifierList : ModifierList<DelegateDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(DelegateDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override DelegateDeclarationSyntax WithModifiers(DelegateDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class DestructorDeclarationModifierList : ModifierList<DestructorDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(DestructorDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override DestructorDeclarationSyntax WithModifiers(DestructorDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class EnumDeclarationModifierList : ModifierList<EnumDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(EnumDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override EnumDeclarationSyntax WithModifiers(EnumDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class EventDeclarationModifierList : ModifierList<EventDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(EventDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override EventDeclarationSyntax WithModifiers(EventDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class EventFieldDeclarationModifierList : ModifierList<EventFieldDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(EventFieldDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override EventFieldDeclarationSyntax WithModifiers(EventFieldDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class FieldDeclarationModifierList : ModifierList<FieldDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(FieldDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override FieldDeclarationSyntax WithModifiers(FieldDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class IncompleteMemberModifierList : ModifierList<IncompleteMemberSyntax>
        {
            internal override SyntaxTokenList GetModifiers(IncompleteMemberSyntax node)
            {
                return node.Modifiers;
            }

            internal override IncompleteMemberSyntax WithModifiers(IncompleteMemberSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class IndexerDeclarationModifierList : ModifierList<IndexerDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(IndexerDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override IndexerDeclarationSyntax WithModifiers(IndexerDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class InterfaceDeclarationModifierList : ModifierList<InterfaceDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(InterfaceDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override InterfaceDeclarationSyntax WithModifiers(InterfaceDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class LocalDeclarationStatementModifierList : ModifierList<LocalDeclarationStatementSyntax>
        {
            internal override SyntaxTokenList GetModifiers(LocalDeclarationStatementSyntax node)
            {
                return node.Modifiers;
            }

            internal override LocalDeclarationStatementSyntax WithModifiers(LocalDeclarationStatementSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class LocalFunctionStatementModifierList : ModifierList<LocalFunctionStatementSyntax>
        {
            internal override SyntaxTokenList GetModifiers(LocalFunctionStatementSyntax node)
            {
                return node.Modifiers;
            }

            internal override LocalFunctionStatementSyntax WithModifiers(LocalFunctionStatementSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class MethodDeclarationModifierList : ModifierList<MethodDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(MethodDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override MethodDeclarationSyntax WithModifiers(MethodDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class OperatorDeclarationModifierList : ModifierList<OperatorDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(OperatorDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override OperatorDeclarationSyntax WithModifiers(OperatorDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class ParameterModifierList : ModifierList<ParameterSyntax>
        {
            internal override SyntaxTokenList GetModifiers(ParameterSyntax node)
            {
                return node.Modifiers;
            }

            internal override ParameterSyntax WithModifiers(ParameterSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class PropertyDeclarationModifierList : ModifierList<PropertyDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(PropertyDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override PropertyDeclarationSyntax WithModifiers(PropertyDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }

        private class StructDeclarationModifierList : ModifierList<StructDeclarationSyntax>
        {
            internal override SyntaxTokenList GetModifiers(StructDeclarationSyntax node)
            {
                return node.Modifiers;
            }

            internal override StructDeclarationSyntax WithModifiers(StructDeclarationSyntax node, SyntaxTokenList modifiers)
            {
                return node.WithModifiers(modifiers);
            }
        }
    }
}
