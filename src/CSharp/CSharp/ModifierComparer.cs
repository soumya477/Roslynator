// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp
{
    /// <summary>
    /// Represents a comparer for modifiers.
    /// </summary>
    public class ModifierComparer : IModifierComparer
    {
        internal static readonly ModifierComparer Instance = new ModifierComparer();

        internal const int MaxRank = 16;

        private ModifierComparer()
        {
        }

        /// <summary>
        /// Compares two modifiers and returns a value indicating whether one should be before,
        /// at the same position, or after the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(SyntaxToken x, SyntaxToken y)
        {
            return GetRank(x).CompareTo(GetRank(y));
        }

        /// <summary>
        /// Returns an index the specified modifier should be inserted at.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public int GetInsertIndex(SyntaxTokenList modifiers, SyntaxToken modifier)
        {
            return GetInsertIndex(modifiers, GetRank(modifier));
        }

        /// <summary>
        /// Returns an index the modifier of the specified kind should be inserted at.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public int GetInsertIndex(SyntaxTokenList modifiers, SyntaxKind modifierKind)
        {
            return GetInsertIndex(modifiers, GetRank(modifierKind));
        }

        private int GetInsertIndex(SyntaxTokenList modifiers, int rank)
        {
            int count = modifiers.Count;

            if (modifiers.Count > 0)
            {
                for (int i = rank; i >= 0; i--)
                {
                    SyntaxKind kind = GetKind(i);

                    for (int j = count - 1; j >= 0; j--)
                    {
                        if (modifiers[j].Kind() == kind)
                            return j + 1;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a rank of the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected virtual int GetRank(SyntaxToken token)
        {
            return GetRank(token.Kind());
        }

        /// <summary>
        /// Returns a rank of a token with the specified kind.
        /// </summary>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        protected virtual int GetRank(SyntaxKind modifierKind)
        {
            switch (modifierKind)
            {
                case SyntaxKind.NewKeyword:
                    return 0;
                case SyntaxKind.PublicKeyword:
                    return 1;
                case SyntaxKind.PrivateKeyword:
                    return 2;
                case SyntaxKind.ProtectedKeyword:
                    return 3;
                case SyntaxKind.InternalKeyword:
                    return 4;
                case SyntaxKind.ConstKeyword:
                    return 5;
                case SyntaxKind.StaticKeyword:
                    return 6;
                case SyntaxKind.VirtualKeyword:
                    return 7;
                case SyntaxKind.SealedKeyword:
                    return 8;
                case SyntaxKind.OverrideKeyword:
                    return 9;
                case SyntaxKind.AbstractKeyword:
                    return 10;
                case SyntaxKind.ReadOnlyKeyword:
                    return 11;
                case SyntaxKind.ExternKeyword:
                    return 12;
                case SyntaxKind.UnsafeKeyword:
                    return 13;
                case SyntaxKind.VolatileKeyword:
                    return 14;
                case SyntaxKind.AsyncKeyword:
                    return 15;
                case SyntaxKind.PartialKeyword:
                    return 16;
                default:
                    {
                        Debug.Fail($"unknown modifier '{modifierKind}'");
                        return MaxRank;
                    }
            }
        }

        /// <summary>
        /// Returns <see cref="SyntaxKind"/> the corresponds to the specified rank.
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        protected virtual SyntaxKind GetKind(int rank)
        {
            switch (rank)
            {
                case 0:
                    return SyntaxKind.NewKeyword;
                case 1:
                    return SyntaxKind.PublicKeyword;
                case 2:
                    return SyntaxKind.ProtectedKeyword;
                case 3:
                    return SyntaxKind.InternalKeyword;
                case 4:
                    return SyntaxKind.PrivateKeyword;
                case 5:
                    return SyntaxKind.ConstKeyword;
                case 6:
                    return SyntaxKind.StaticKeyword;
                case 7:
                    return SyntaxKind.VirtualKeyword;
                case 8:
                    return SyntaxKind.SealedKeyword;
                case 9:
                    return SyntaxKind.OverrideKeyword;
                case 10:
                    return SyntaxKind.AbstractKeyword;
                case 11:
                    return SyntaxKind.ReadOnlyKeyword;
                case 12:
                    return SyntaxKind.ExternKeyword;
                case 13:
                    return SyntaxKind.UnsafeKeyword;
                case 14:
                    return SyntaxKind.VolatileKeyword;
                case 15:
                    return SyntaxKind.AsyncKeyword;
                case 16:
                    return SyntaxKind.PartialKeyword;
                default:
                    return SyntaxKind.None;
            }
        }
    }
}
