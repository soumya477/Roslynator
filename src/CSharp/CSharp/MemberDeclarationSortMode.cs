// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp
{
    /// <summary>
    /// Defines modes how member declarations can be sorted.
    /// </summary>
    public enum MemberDeclarationSortMode
    {
        /// <summary>
        /// Sort members by its kind.
        /// </summary>
        ByKind = 0,

        /// <summary>
        /// Sort members by its kind, then by its name if the kinds are equal.
        /// </summary>
        ByKindThenByName = 1,
    }
}
