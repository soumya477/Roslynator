// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp
{
    //TODO: cmt
    public interface ISyntaxTokenListInserter
    {
        SyntaxTokenList Insert(SyntaxTokenList tokens, SyntaxToken token);

        int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token);

        int GetInsertIndex(SyntaxTokenList tokens, SyntaxKind kind);
    }
}
