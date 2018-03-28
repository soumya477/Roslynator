// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp
{
    internal abstract class ModifierComparer : IComparer<SyntaxToken>
    {
        internal const int MaxRank = 17;

        protected ModifierComparer()
        {
        }

        public static ModifierComparer Default { get; } = new ByKindModifierComparer();

        public abstract int Compare(SyntaxToken x, SyntaxToken y);

        public virtual int GetRank(SyntaxToken token)
        {
            return ModifierKindComparer.Default.GetRank(token.Kind());
        }

        public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token, IComparer<SyntaxToken> comparer)
        {
            if (comparer == null)
                comparer = Default;

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

        private sealed class ByKindModifierComparer : ModifierComparer
        {
            public override int Compare(SyntaxToken x, SyntaxToken y)
            {
                return GetRank(x).CompareTo(GetRank(y));
            }
        }
    }
}
