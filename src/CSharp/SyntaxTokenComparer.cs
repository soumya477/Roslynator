// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Roslynator
{
    //TODO: cmt
    public abstract class SyntaxTokenComparer : IComparer<SyntaxToken>, IEqualityComparer<SyntaxToken>
    {
        protected SyntaxTokenComparer()
        {
        }

        /// <summary>
        /// Compares two modifiers and returns a value indicating whether one should be before,
        /// at the same position, or after the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public abstract int Compare(SyntaxToken x, SyntaxToken y);

        public abstract bool Equals(SyntaxToken x, SyntaxToken y);

        public abstract int GetHashCode(SyntaxToken obj);
    }
}
