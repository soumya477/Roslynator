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
    internal class ByKindMemberDeclarationComparer : MemberDeclarationComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByKindMemberDeclarationComparer"/> class.
        /// </summary>
        public ByKindMemberDeclarationComparer()
        {
        }

        /// <summary>
        /// Compares two member declarations and returns a value indicating whether one should be before,
        /// at the same position, or after the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override int Compare(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            return CompareCore(x, y);
        }

        protected virtual int CompareCore(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
        {
            return MemberDeclarationInserter.Default.GetRank(x).CompareTo(MemberDeclarationInserter.Default.GetRank(y));
        }

        public override bool Equals(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }



            throw new NotImplementedException();
        }

        public override int GetHashCode(MemberDeclarationSyntax obj)
        {
            if (obj == null)
                return 0;

            return GetHashCode(obj);

        }
    }
}
