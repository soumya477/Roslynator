// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp
{
    //TODO: cmt
    public interface ISyntaxListInserter<TNode> where TNode : SyntaxNode
    {
        SyntaxList<TNode> Insert(SyntaxList<TNode> list, TNode node);

        int GetInsertIndex(SyntaxList<TNode> list, TNode node);

        int GetInsertIndex(SyntaxList<TNode> list, SyntaxKind kind);

        int GetFieldInsertIndex(SyntaxList<TNode> list, bool isConst);
    }
}
