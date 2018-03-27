// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp
{

    /// <summary>
    /// Represents a comparer for member declarations.
    /// </summary>
    public abstract class DefaultMemberDeclarationComparer : IComparer<MemberDeclarationSyntax>, IEqualityComparer<MemberDeclarationSyntax>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberDeclarationComparer"/> class.
        /// </summary>
        /// <param name="sortMode"></param>
        public MemberDeclarationComparer(MemberDeclarationSortMode sortMode)
        {
            SortMode = sortMode;
        }

        /// <summary>
        /// The sort mode used by this instance.
        /// </summary>
        public MemberDeclarationSortMode SortMode { get; }

        internal static MemberDeclarationComparer ByKind { get; } = new MemberDeclarationComparer(MemberDeclarationSortMode.ByKind);

        internal static MemberDeclarationComparer ByKindThenByName { get; } = new MemberDeclarationComparer(MemberDeclarationSortMode.ByKindThenByName);

        internal static MemberDeclarationComparer GetInstance(MemberDeclarationSortMode sortMode)
        {
            switch (sortMode)
            {
                case MemberDeclarationSortMode.ByKind:
                    return ByKind;
                case MemberDeclarationSortMode.ByKindThenByName:
                    return ByKindThenByName;
                default:
                    throw new ArgumentException("", nameof(sortMode));
            }
        }

        /// <summary>
        /// Compares two member declarations and returns a value indicating whether one should be before,
        /// at the same position, or after the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            int result = MemberDeclarationInserter.Default.GetRank(x).CompareTo(MemberDeclarationInserter.Default.GetRank(y));

            if (SortMode == MemberDeclarationSortMode.ByKindThenByName
                && result == 0)
            {
                return string.Compare(MemberDeclarationInserter.GetName(x), MemberDeclarationInserter.GetName(y), StringComparison.CurrentCulture);
            }
            else
            {
                return result;
            }
        }

        internal static bool CanBeSortedByName(SyntaxKind memberKind)
        {
            switch (memberKind)
            {
                case SyntaxKind.FieldDeclaration:
                case SyntaxKind.ConstructorDeclaration:
                case SyntaxKind.DelegateDeclaration:
                case SyntaxKind.EventDeclaration:
                case SyntaxKind.EventFieldDeclaration:
                case SyntaxKind.PropertyDeclaration:
                case SyntaxKind.MethodDeclaration:
                case SyntaxKind.EnumDeclaration:
                case SyntaxKind.InterfaceDeclaration:
                case SyntaxKind.StructDeclaration:
                case SyntaxKind.ClassDeclaration:
                case SyntaxKind.NamespaceDeclaration:
                    return true;
                case SyntaxKind.DestructorDeclaration:
                case SyntaxKind.IndexerDeclaration:
                case SyntaxKind.ConversionOperatorDeclaration:
                case SyntaxKind.OperatorDeclaration:
                case SyntaxKind.IncompleteMember:
                    return false;
                default:
                    {
                        Debug.Fail($"unknown member '{memberKind}'");
                        return false;
                    }
            }
        }
    }
}
