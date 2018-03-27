// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp
{
    //TODO: cmt
    public class MemberDeclarationInserter : ISyntaxListInserter<MemberDeclarationSyntax>
    {
        internal const int MaxRank = 18;

        private const int ConstRank = 0;
        private const int FieldRank = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberDeclarationInserter"/> class.
        /// </summary>
        public MemberDeclarationInserter()
        {
        }

        public static MemberDeclarationInserter Default { get; } = new MemberDeclarationInserter();

        //TODO: cmt
        public SyntaxList<MemberDeclarationSyntax> Insert(SyntaxList<MemberDeclarationSyntax> list, MemberDeclarationSyntax node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            return list.Insert(GetInsertIndex(list, node), node);
        }

        public int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> list, MemberDeclarationSyntax node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            return GetInsertIndex(list, GetRank(node));
        }

        public int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> list, SyntaxKind kind)
        {
            return GetInsertIndex(list, GetRank(kind));
        }

        public int GetFieldInsertIndex(SyntaxList<MemberDeclarationSyntax> list, bool isConst)
        {
            return GetInsertIndex(list, (isConst) ? ConstRank : FieldRank);
        }

        private int GetInsertIndex(SyntaxList<MemberDeclarationSyntax> members, int rank)
        {
            if (members.Any())
            {
                while (rank >= 0)
                {
                    SyntaxKind kind = GetKind(rank);

                    for (int i = members.Count - 1; i >= 0; i--)
                    {
                        if (IsMatch(members[i], kind, rank))
                            return i + 1;
                    }

                    rank--;
                }
            }

            return 0;
        }

        private static bool IsMatch(MemberDeclarationSyntax member, SyntaxKind kind, int rank)
        {
            switch (rank)
            {
                case ConstRank:
                    {
                        return member.Kind() == SyntaxKind.FieldDeclaration
                           && ((FieldDeclarationSyntax)member).Modifiers.Contains(SyntaxKind.ConstKeyword);
                    }
                case FieldRank:
                    {
                        return member.Kind() == SyntaxKind.FieldDeclaration
                           && !((FieldDeclarationSyntax)member).Modifiers.Contains(SyntaxKind.ConstKeyword);
                    }
                default:
                    {
                        return member.Kind() == kind;
                    }
            }
        }

        /// <summary>
        /// Returns a rank of the specified member.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public virtual int GetRank(MemberDeclarationSyntax member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            SyntaxKind kind = member.Kind();

            if (kind == SyntaxKind.FieldDeclaration)
            {
                return (((FieldDeclarationSyntax)member).Modifiers.Contains(SyntaxKind.ConstKeyword))
                    ? ConstRank
                    : FieldRank;
            }

            return GetRank(kind);
        }

        /// <summary>
        /// Returns a rank of a member of the specified kind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public virtual int GetRank(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.FieldDeclaration:
                    return FieldRank;
                case SyntaxKind.ConstructorDeclaration:
                    return 2;
                case SyntaxKind.DestructorDeclaration:
                    return 3;
                case SyntaxKind.DelegateDeclaration:
                    return 4;
                case SyntaxKind.EventDeclaration:
                    return 5;
                case SyntaxKind.EventFieldDeclaration:
                    return 6;
                case SyntaxKind.PropertyDeclaration:
                    return 7;
                case SyntaxKind.IndexerDeclaration:
                    return 8;
                case SyntaxKind.MethodDeclaration:
                    return 9;
                case SyntaxKind.ConversionOperatorDeclaration:
                    return 10;
                case SyntaxKind.OperatorDeclaration:
                    return 11;
                case SyntaxKind.EnumDeclaration:
                    return 12;
                case SyntaxKind.InterfaceDeclaration:
                    return 13;
                case SyntaxKind.StructDeclaration:
                    return 14;
                case SyntaxKind.ClassDeclaration:
                    return 15;
                case SyntaxKind.NamespaceDeclaration:
                    return 16;
                case SyntaxKind.IncompleteMember:
                    return 17;
                default:
                    {
                        Debug.Fail($"unknown member '{kind}'");
                        return MaxRank;
                    }
            }
        }

        /// <summary>
        /// Returns a <see cref="SyntaxKind"/> the corresponds to the specified rank.
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public virtual SyntaxKind GetKind(int rank)
        {
            switch (rank)
            {
                case FieldRank:
                    return SyntaxKind.FieldDeclaration;
                case 2:
                    return SyntaxKind.ConstructorDeclaration;
                case 3:
                    return SyntaxKind.DestructorDeclaration;
                case 4:
                    return SyntaxKind.DelegateDeclaration;
                case 5:
                    return SyntaxKind.EventDeclaration;
                case 6:
                    return SyntaxKind.EventFieldDeclaration;
                case 7:
                    return SyntaxKind.PropertyDeclaration;
                case 8:
                    return SyntaxKind.IndexerDeclaration;
                case 9:
                    return SyntaxKind.MethodDeclaration;
                case 10:
                    return SyntaxKind.ConversionOperatorDeclaration;
                case 11:
                    return SyntaxKind.OperatorDeclaration;
                case 12:
                    return SyntaxKind.EnumDeclaration;
                case 13:
                    return SyntaxKind.InterfaceDeclaration;
                case 14:
                    return SyntaxKind.StructDeclaration;
                case 15:
                    return SyntaxKind.ClassDeclaration;
                case 16:
                    return SyntaxKind.NamespaceDeclaration;
                case 17:
                    return SyntaxKind.IncompleteMember;
                default:
                    return SyntaxKind.None;
            }
        }

        internal static string GetName(MemberDeclarationSyntax member)
        {
            switch (member.Kind())
            {
                case SyntaxKind.FieldDeclaration:
                    {
                        return ((FieldDeclarationSyntax)member).Declaration?.Variables.FirstOrDefault()?.Identifier.ValueText;
                    }
                case SyntaxKind.ConstructorDeclaration:
                    return ((ConstructorDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.DelegateDeclaration:
                    return ((DelegateDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.EventDeclaration:
                    return ((EventDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.EventFieldDeclaration:
                    return ((EventFieldDeclarationSyntax)member).Declaration?.Variables.FirstOrDefault()?.Identifier.ValueText;
                case SyntaxKind.PropertyDeclaration:
                    return ((PropertyDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.MethodDeclaration:
                    return ((MethodDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.EnumDeclaration:
                    return ((EnumDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.InterfaceDeclaration:
                    return ((InterfaceDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.StructDeclaration:
                    return ((StructDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.ClassDeclaration:
                    return ((ClassDeclarationSyntax)member).Identifier.ValueText;
                case SyntaxKind.NamespaceDeclaration:
                    return ((NamespaceDeclarationSyntax)member).Name.ToString();
                case SyntaxKind.DestructorDeclaration:
                case SyntaxKind.IndexerDeclaration:
                case SyntaxKind.ConversionOperatorDeclaration:
                case SyntaxKind.OperatorDeclaration:
                case SyntaxKind.IncompleteMember:
                    return "";
                default:
                    {
                        Debug.Fail($"unknown member '{member.Kind()}'");
                        return "";
                    }
            }
        }
    }
}
