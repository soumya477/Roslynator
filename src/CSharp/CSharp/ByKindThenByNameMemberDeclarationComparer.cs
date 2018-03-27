// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp
{
    internal class ByKindThenByNameMemberDeclarationComparer : ByKindMemberDeclarationComparer
    {
        protected override int CompareCore(MemberDeclarationSyntax x, MemberDeclarationSyntax y)
        {
            int result = base.CompareCore(x, y);

            if (result == 0)
            {
                return string.Compare(MemberDeclarationInserter.GetName(x), MemberDeclarationInserter.GetName(y), StringComparison.CurrentCulture);
            }
            else
            {
                return result;
            }
        }
    }
}
