// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp
{
    /// <summary>
    /// Represents a comparer for modifiers.
    /// </summary>
    public class ModifierInserter : ISyntaxTokenListInserter
    {
        internal const int MaxRank = 17;

        internal static readonly ModifierInserter Default = new ModifierInserter();

        private ModifierInserter()
        {
        }

        public SyntaxTokenList Insert(SyntaxTokenList tokens, SyntaxToken token)
        {
            return tokens.Insert(GetInsertIndex(tokens, token), token);
        }

        /// <summary>
        /// Returns an index the specified modifier should be inserted at.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token)
        {
            return GetInsertIndex(tokens, GetRank(token));
        }

        /// <summary>
        /// Returns an index the modifier of the specified kind should be inserted at.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public int GetInsertIndex(SyntaxTokenList tokens, SyntaxKind kind)
        {
            return GetInsertIndex(tokens, GetRank(kind));
        }

        private int GetInsertIndex(SyntaxTokenList modifiers, int rank)
        {
            int count = modifiers.Count;

            if (count > 0)
            {
                while (rank >= 0)
                {
                    SyntaxKind kind = GetKind(rank);

                    for (int i = count - 1; i >= 0; i--)
                    {
                        if (modifiers[i].Kind() == kind)
                            return i + 1;
                    }

                    rank--;
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns a rank of the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual int GetRank(SyntaxToken token)
        {
            return GetRank(token.Kind());
        }

        /// <summary>
        /// Returns a rank of a token with the specified kind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public virtual int GetRank(SyntaxKind kind)
        {
            switch (kind)
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
                        Debug.Fail($"unknown modifier '{kind}'");
                        return MaxRank;
                    }
            }
        }

        /// <summary>
        /// Returns <see cref="SyntaxKind"/> the corresponds to the specified rank.
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public virtual SyntaxKind GetKind(int rank)
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
