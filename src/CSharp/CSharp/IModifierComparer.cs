// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp
{
    /// <summary>
    /// Defines methods that a type implements to compare and sort modifiers.
    /// </summary>
    public interface IModifierComparer : IComparer<SyntaxToken>
    {
        /// <summary>
        /// Returns an index the specified modifier should be inserted at.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        int GetInsertIndex(SyntaxTokenList modifiers, SyntaxToken modifier);

        /// <summary>
        /// Returns an index the modifier of the specified kind should be inserted at.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        int GetInsertIndex(SyntaxTokenList modifiers, SyntaxKind modifierKind);
    }
}
