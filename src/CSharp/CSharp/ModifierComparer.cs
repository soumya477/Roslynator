// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.CSharp
{
    //TODO: cmt
    internal abstract class ModifierComparer : SyntaxTokenComparer
    {
        protected ModifierComparer()
        {
        }

        public static ModifierComparer Default { get; } = new ModifierKindComparer();

        private sealed class ModifierKindComparer : ModifierComparer
        {
            /// <summary>
            /// Compares two modifiers and returns a value indicating whether one should be before,
            /// at the same position, or after the other.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public override int Compare(SyntaxToken x, SyntaxToken y)
            {
                return GetRank(x).CompareTo(GetRank(y));
            }

            public override bool Equals(SyntaxToken x, SyntaxToken y)
            {
                return GetRank(x) == GetRank(y);
            }

            public override int GetHashCode(SyntaxToken obj)
            {
                return GetRank(obj);
            }

            private static int GetRank(SyntaxToken x)
            {
                return ModifierInserter.Default.GetRank(x);
            }
        }
    }
}
