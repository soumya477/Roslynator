// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp
{
    internal static class SyntaxInserter
    {
        internal static int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> list, MemberDeclarationSyntax node, IComparer<MemberDeclarationSyntax> comparer)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (comparer == null)
                comparer = MemberDeclarationComparer.ByKind;

            int index = list.Count;

            for (int i = index - 1; i >= 0; i--)
            {
                int result = comparer.Compare(list[i], node);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result > 0)
                {
                    index = i;
                }
            }

            return index;
        }

        public static int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> members, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            if (comparer == null)
                comparer = MemberDeclarationKindComparer.Default;

            int index = members.Count;

            for (int i = index - 1; i >= 0; i--)
            {
                int result = comparer.Compare(members[i].Kind(), kind);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result > 0)
                {
                    index = i;
                }
            }

            return index;
        }

        public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token, IComparer<SyntaxToken> comparer)
        {
            if (comparer == null)
                comparer = ModifierComparer.Default;

            int index = tokens.Count;

            for (int i = index - 1; i >= 0; i--)
            {
                int result = comparer.Compare(tokens[i], token);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result > 0)
                {
                    index = i;
                }
            }

            return index;
        }

        public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxKind kind, IComparer<SyntaxKind> comparer)
        {
            if (comparer == null)
                comparer = ModifierKindComparer.Default;

            int index = tokens.Count;

            for (int i = index - 1; i >= 0; i--)
            {
                int result = comparer.Compare(tokens[i].Kind(), kind);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result > 0)
                {
                    index = i;
                }
            }

            return index;
        }
    }
}
