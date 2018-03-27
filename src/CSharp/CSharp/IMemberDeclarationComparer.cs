// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp
{
    /// <summary>
    /// Defines methods that a type implements to compare and sort member declarations.
    /// Comparer for <see cref="MemberDeclarationSyntax"/>.
    /// </summary>
    public interface IMemberDeclarationComparer : IComparer<MemberDeclarationSyntax>
    {
        /// <summary>
        /// Returns an index the specified member should be inserted at.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> members, MemberDeclarationSyntax member);

        /// <summary>
        /// Returns an index the member of the specified kind should be inserted at.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="memberKind"></param>
        /// <returns></returns>
        int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> members, SyntaxKind memberKind);

        /// <summary>
        /// Returns an index the field or const declaration should be inserted at.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="isConst"></param>
        /// <returns></returns>
        int GetFieldInsertIndex(SyntaxList<MemberDeclarationSyntax> members, bool isConst);
    }
}
