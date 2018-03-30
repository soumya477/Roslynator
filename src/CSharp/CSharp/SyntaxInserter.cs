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
        internal static int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> members, MemberDeclarationSyntax member, IComparer<MemberDeclarationSyntax> comparer = null)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (comparer == null)
                comparer = MemberDeclarationComparer.ByKind;

            int index = -1;

            for (int i = members.Count - 1; i >= 0; i--)
            {
                int result = comparer.Compare(members[i], member);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result < 0
                    && index == -1)
                {
                    index = i + 1;
                }
            }

            if (index == -1)
                return members.Count;

            return index;
        }

        public static int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> members, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            if (comparer == null)
                comparer = MemberDeclarationKindComparer.Default;

            int index = -1;

            for (int i = members.Count - 1; i >= 0; i--)
            {
                int result = comparer.Compare(members[i].Kind(), kind);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result < 0
                    && index == -1)
                {
                    index = i + 1;
                }
            }

            if (index == -1)
                return members.Count;

            return index;
        }

        public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token, IComparer<SyntaxToken> comparer = null)
        {
            if (comparer == null)
                comparer = ModifierComparer.Default;

            int index = -1;

            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                int result = comparer.Compare(tokens[i], token);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result < 0
                    && index == -1)
                {
                    index = i + 1;
                }
            }

            if (index == -1)
                return tokens.Count;

            return index;
        }

        public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            if (comparer == null)
                comparer = ModifierKindComparer.Default;

            int index = -1;

            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                int result = comparer.Compare(tokens[i].Kind(), kind);

                if (result == 0)
                {
                    return i + 1;
                }
                else if (result < 0
                    && index == -1)
                {
                    index = i + 1;
                }
            }

            if (index == -1)
                return tokens.Count;

            return index;
        }
    }
}
