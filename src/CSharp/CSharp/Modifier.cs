﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.ModifierHelpers;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslynator.CSharp
{
    /// <summary>
    /// A set of static methods that allows manipulation with modifiers.
    /// </summary>
    public static class Modifier
    {
        internal static SyntaxNode Insert(SyntaxNode node, Accessibility accessibility, IComparer<SyntaxKind> comparer)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    {
                        return Insert(node, SyntaxKind.PrivateKeyword, comparer);
                    }
                case Accessibility.Protected:
                    {
                        return Insert(node, SyntaxKind.ProtectedKeyword, comparer);
                    }
                case Accessibility.ProtectedAndInternal:
                    {
                        node = Insert(node, SyntaxKind.PrivateKeyword, comparer);

                        return Insert(node, SyntaxKind.ProtectedKeyword, comparer);
                    }
                case Accessibility.Internal:
                    {
                        return Insert(node, SyntaxKind.InternalKeyword, comparer);
                    }
                case Accessibility.Public:
                    {
                        return Insert(node, SyntaxKind.PublicKeyword, comparer);
                    }
                case Accessibility.ProtectedOrInternal:
                    {
                        node = Insert(node, SyntaxKind.ProtectedKeyword, comparer);

                        return Insert(node, SyntaxKind.InternalKeyword, comparer);
                    }
            }

            return node;
        }

        /// <summary>
        /// Creates a new node with a modifier of the specified kind inserted.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static TNode Insert<TNode>(TNode node, SyntaxKind kind, IComparer<SyntaxKind> comparer = null) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Insert((ClassDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Insert((ConstructorDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Insert((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Insert((DelegateDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Insert((DestructorDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Insert((EnumDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Insert((EventDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Insert((EventFieldDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Insert((FieldDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Insert((IndexerDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Insert((InterfaceDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Insert((MethodDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Insert((OperatorDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Insert((PropertyDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Insert((StructDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Insert((AccessorDeclarationSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Insert((LocalDeclarationStatementSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Insert((LocalFunctionStatementSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Insert((ParameterSyntax)(SyntaxNode)node, kind, comparer);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)Insert((IncompleteMemberSyntax)(SyntaxNode)node, kind, comparer);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new node with the specified modifier inserted.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static TNode Insert<TNode>(TNode node, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Insert((ClassDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Insert((ConstructorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Insert((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Insert((DelegateDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Insert((DestructorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Insert((EnumDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Insert((EventDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Insert((EventFieldDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Insert((FieldDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Insert((IndexerDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Insert((InterfaceDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Insert((MethodDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Insert((OperatorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Insert((PropertyDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Insert((StructDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Insert((AccessorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Insert((LocalDeclarationStatementSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Insert((LocalFunctionStatementSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Insert((ParameterSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)Insert((IncompleteMemberSyntax)(SyntaxNode)node, modifier, comparer);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new node with a modifier of the specified kind removed.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static TNode Remove<TNode>(TNode node, SyntaxKind kind) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Remove((ClassDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConstructorDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Remove((DelegateDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((DestructorDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Remove((EnumDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventFieldDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((FieldDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Remove((IndexerDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Remove((InterfaceDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Remove((MethodDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((OperatorDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Remove((PropertyDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Remove((StructDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Remove((AccessorDeclarationSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Remove((LocalDeclarationStatementSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Remove((LocalFunctionStatementSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Remove((ParameterSyntax)(SyntaxNode)node, kind);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)Remove((IncompleteMemberSyntax)(SyntaxNode)node, kind);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new node with the specified modifier removed.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static TNode Remove<TNode>(TNode node, SyntaxToken modifier) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Remove((ClassDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConstructorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Remove((DelegateDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((DestructorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Remove((EnumDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventFieldDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((FieldDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Remove((IndexerDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Remove((InterfaceDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Remove((MethodDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((OperatorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Remove((PropertyDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Remove((StructDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Remove((AccessorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Remove((LocalDeclarationStatementSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Remove((LocalFunctionStatementSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Remove((ParameterSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)Remove((IncompleteMemberSyntax)(SyntaxNode)node, modifier);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new node with a modifier at the specified index removed.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static TNode RemoveAt<TNode>(TNode node, int index) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((ClassDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((ConstructorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((DelegateDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((DestructorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((EnumDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((EventDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((EventFieldDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((FieldDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((IndexerDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((InterfaceDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((MethodDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((OperatorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((PropertyDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((StructDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((AccessorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)RemoveAt((LocalDeclarationStatementSyntax)(SyntaxNode)node, index);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)RemoveAt((LocalFunctionStatementSyntax)(SyntaxNode)node, index);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)RemoveAt((ParameterSyntax)(SyntaxNode)node, index);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)RemoveAt((IncompleteMemberSyntax)(SyntaxNode)node, index);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new node with accessibility modifiers removed.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TNode RemoveAccessibility<TNode>(TNode node) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ClassDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ConstructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ConversionOperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((DelegateDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((DestructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((EnumDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((EventDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((EventFieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((FieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((IndexerDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((InterfaceDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((MethodDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((OperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((PropertyDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((StructDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((AccessorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)RemoveAccessibility((LocalDeclarationStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)RemoveAccessibility((LocalFunctionStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ParameterSyntax)(SyntaxNode)node);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)RemoveAccessibility((IncompleteMemberSyntax)(SyntaxNode)node);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new node with all modifiers removed.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TNode RemoveAll<TNode>(TNode node) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((ClassDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((ConstructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((ConversionOperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((DelegateDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((DestructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((EnumDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((EventDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((EventFieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((FieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((IndexerDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((InterfaceDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((MethodDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((OperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((PropertyDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((StructDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((AccessorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)RemoveAll((LocalDeclarationStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)RemoveAll((LocalFunctionStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)RemoveAll((ParameterSyntax)(SyntaxNode)node);
                case SyntaxKind.IncompleteMember:
                    return (TNode)(SyntaxNode)RemoveAll((IncompleteMemberSyntax)(SyntaxNode)node);
            }

            throw new ArgumentException($"'{node.Kind()}' does not have modifiers.", nameof(node));
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Insert(ClassDeclarationSyntax classDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return ClassDeclarationModifierHelper.Instance.InsertModifier(classDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Insert(ClassDeclarationSyntax classDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return ClassDeclarationModifierHelper.Instance.InsertModifier(classDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Insert(ConstructorDeclarationSyntax constructorDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return ConstructorDeclarationModifierHelper.Instance.InsertModifier(constructorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Insert(ConstructorDeclarationSyntax constructorDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return ConstructorDeclarationModifierHelper.Instance.InsertModifier(constructorDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Insert(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.InsertModifier(conversionOperatorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Insert(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.InsertModifier(conversionOperatorDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Insert(DelegateDeclarationSyntax delegateDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return DelegateDeclarationModifierHelper.Instance.InsertModifier(delegateDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Insert(DelegateDeclarationSyntax delegateDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return DelegateDeclarationModifierHelper.Instance.InsertModifier(delegateDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Insert(DestructorDeclarationSyntax destructorDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return DestructorDeclarationModifierHelper.Instance.InsertModifier(destructorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Insert(DestructorDeclarationSyntax destructorDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return DestructorDeclarationModifierHelper.Instance.InsertModifier(destructorDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Insert(EnumDeclarationSyntax enumDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return EnumDeclarationModifierHelper.Instance.InsertModifier(enumDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Insert(EnumDeclarationSyntax enumDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return EnumDeclarationModifierHelper.Instance.InsertModifier(enumDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Insert(EventDeclarationSyntax eventDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return EventDeclarationModifierHelper.Instance.InsertModifier(eventDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Insert(EventDeclarationSyntax eventDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return EventDeclarationModifierHelper.Instance.InsertModifier(eventDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Insert(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return EventFieldDeclarationModifierHelper.Instance.InsertModifier(eventFieldDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Insert(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return EventFieldDeclarationModifierHelper.Instance.InsertModifier(eventFieldDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Insert(FieldDeclarationSyntax fieldDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return FieldDeclarationModifierHelper.Instance.InsertModifier(fieldDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Insert(FieldDeclarationSyntax fieldDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return FieldDeclarationModifierHelper.Instance.InsertModifier(fieldDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Insert(IndexerDeclarationSyntax indexerDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return IndexerDeclarationModifierHelper.Instance.InsertModifier(indexerDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Insert(IndexerDeclarationSyntax indexerDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return IndexerDeclarationModifierHelper.Instance.InsertModifier(indexerDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Insert(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return InterfaceDeclarationModifierHelper.Instance.InsertModifier(interfaceDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Insert(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return InterfaceDeclarationModifierHelper.Instance.InsertModifier(interfaceDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Insert(MethodDeclarationSyntax methodDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return MethodDeclarationModifierHelper.Instance.InsertModifier(methodDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Insert(MethodDeclarationSyntax methodDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return MethodDeclarationModifierHelper.Instance.InsertModifier(methodDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Insert(OperatorDeclarationSyntax operatorDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return OperatorDeclarationModifierHelper.Instance.InsertModifier(operatorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Insert(OperatorDeclarationSyntax operatorDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return OperatorDeclarationModifierHelper.Instance.InsertModifier(operatorDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Insert(PropertyDeclarationSyntax propertyDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return PropertyDeclarationModifierHelper.Instance.InsertModifier(propertyDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Insert(PropertyDeclarationSyntax propertyDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return PropertyDeclarationModifierHelper.Instance.InsertModifier(propertyDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Insert(StructDeclarationSyntax structDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return StructDeclarationModifierHelper.Instance.InsertModifier(structDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Insert(StructDeclarationSyntax structDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return StructDeclarationModifierHelper.Instance.InsertModifier(structDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Insert(AccessorDeclarationSyntax accessorDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return AccessorDeclarationModifierHelper.Instance.InsertModifier(accessorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Insert(AccessorDeclarationSyntax accessorDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return AccessorDeclarationModifierHelper.Instance.InsertModifier(accessorDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier inserted.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Insert(LocalDeclarationStatementSyntax localDeclaration, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return LocalDeclarationStatementModifierHelper.Instance.InsertModifier(localDeclaration, modifier, comparer);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Insert(LocalDeclarationStatementSyntax localDeclaration, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return LocalDeclarationStatementModifierHelper.Instance.InsertModifier(localDeclaration, kind, comparer);
        }

        /// <summary>
        /// Creates a new local function with the specified modifier inserted.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Insert(LocalFunctionStatementSyntax localFunction, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return LocalFunctionStatementModifierHelper.Instance.InsertModifier(localFunction, modifier, comparer);
        }

        /// <summary>
        /// Creates a new local function with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Insert(LocalFunctionStatementSyntax localFunction, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return LocalFunctionStatementModifierHelper.Instance.InsertModifier(localFunction, kind, comparer);
        }

        /// <summary>
        /// Creates a new parameter with the specified modifier inserted.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ParameterSyntax Insert(ParameterSyntax parameter, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return ParameterModifierHelper.Instance.InsertModifier(parameter, modifier, comparer);
        }

        /// <summary>
        /// Creates a new parameter with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ParameterSyntax Insert(ParameterSyntax parameter, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return ParameterModifierHelper.Instance.InsertModifier(parameter, kind, comparer);
        }

        /// <summary>
        /// Creates a new incomplete member with the specified modifier inserted.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax Insert(IncompleteMemberSyntax incompleteMember, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            return IncompleteMemberModifierHelper.Instance.InsertModifier(incompleteMember, modifier, comparer);
        }

        /// <summary>
        /// Creates a new incomplete member with a modifier of the specified kind inserted.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax Insert(IncompleteMemberSyntax incompleteMember, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            return IncompleteMemberModifierHelper.Instance.InsertModifier(incompleteMember, kind, comparer);
        }

        /// <summary>
        /// Creates a new list of modifiers with the modifier of the specified kind inserted.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="kind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
        {
            if (!modifiers.Any())
                return modifiers.Add(Token(kind));

            return Insert(modifiers, Token(kind), ModifierKindComparer.GetInsertIndex(modifiers, kind, comparer));
        }

        /// <summary>
        /// Creates a new list of modifiers with a specified modifier inserted.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxToken modifier, IComparer<SyntaxToken> comparer = null)
        {
            if (!modifiers.Any())
                return modifiers.Add(modifier);

            return Insert(modifiers, modifier, ModifierComparer.GetInsertIndex(modifiers, modifier, comparer));
        }

        private static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxToken modifier, int index)
        {
            if (index == 0)
            {
                SyntaxToken firstModifier = modifiers[index];

                SyntaxTriviaList trivia = firstModifier.LeadingTrivia;

                if (trivia.Any())
                {
                    SyntaxTriviaList leadingTrivia = modifier.LeadingTrivia;

                    if (!leadingTrivia.IsSingleElasticMarker())
                        trivia = trivia.AddRange(leadingTrivia);

                    modifier = modifier.WithLeadingTrivia(trivia);

                    modifiers = modifiers.ReplaceAt(index, firstModifier.WithoutLeadingTrivia());
                }
            }

            return modifiers.Insert(index, modifier);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Remove(ClassDeclarationSyntax classDeclaration, SyntaxToken modifier)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifier(classDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Remove(ClassDeclarationSyntax classDeclaration, SyntaxKind kind)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifier(classDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Remove(ConstructorDeclarationSyntax constructorDeclaration, SyntaxToken modifier)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifier(constructorDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Remove(ConstructorDeclarationSyntax constructorDeclaration, SyntaxKind kind)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifier(constructorDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Remove(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxToken modifier)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifier(conversionOperatorDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Remove(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxKind kind)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifier(conversionOperatorDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Remove(DelegateDeclarationSyntax delegateDeclaration, SyntaxToken modifier)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifier(delegateDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Remove(DelegateDeclarationSyntax delegateDeclaration, SyntaxKind kind)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifier(delegateDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Remove(DestructorDeclarationSyntax destructorDeclaration, SyntaxToken modifier)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifier(destructorDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Remove(DestructorDeclarationSyntax destructorDeclaration, SyntaxKind kind)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifier(destructorDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Remove(EnumDeclarationSyntax enumDeclaration, SyntaxToken modifier)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifier(enumDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Remove(EnumDeclarationSyntax enumDeclaration, SyntaxKind kind)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifier(enumDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Remove(EventDeclarationSyntax eventDeclaration, SyntaxToken modifier)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifier(eventDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Remove(EventDeclarationSyntax eventDeclaration, SyntaxKind kind)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifier(eventDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Remove(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxToken modifier)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifier(eventFieldDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Remove(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxKind kind)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifier(eventFieldDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Remove(FieldDeclarationSyntax fieldDeclaration, SyntaxToken modifier)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifier(fieldDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Remove(FieldDeclarationSyntax fieldDeclaration, SyntaxKind kind)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifier(fieldDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Remove(IndexerDeclarationSyntax indexerDeclaration, SyntaxToken modifier)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifier(indexerDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Remove(IndexerDeclarationSyntax indexerDeclaration, SyntaxKind kind)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifier(indexerDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Remove(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxToken modifier)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifier(interfaceDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Remove(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxKind kind)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifier(interfaceDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Remove(MethodDeclarationSyntax methodDeclaration, SyntaxToken modifier)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifier(methodDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Remove(MethodDeclarationSyntax methodDeclaration, SyntaxKind kind)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifier(methodDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Remove(OperatorDeclarationSyntax operatorDeclaration, SyntaxToken modifier)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifier(operatorDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Remove(OperatorDeclarationSyntax operatorDeclaration, SyntaxKind kind)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifier(operatorDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Remove(PropertyDeclarationSyntax propertyDeclaration, SyntaxToken modifier)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifier(propertyDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Remove(PropertyDeclarationSyntax propertyDeclaration, SyntaxKind kind)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifier(propertyDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Remove(StructDeclarationSyntax structDeclaration, SyntaxToken modifier)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifier(structDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Remove(StructDeclarationSyntax structDeclaration, SyntaxKind kind)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifier(structDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Remove(AccessorDeclarationSyntax accessorDeclaration, SyntaxToken modifier)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifier(accessorDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Remove(AccessorDeclarationSyntax accessorDeclaration, SyntaxKind kind)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifier(accessorDeclaration, kind);
        }

        /// <summary>
        /// Creates a new declaration with the specified modifier removed.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Remove(LocalDeclarationStatementSyntax localDeclaration, SyntaxToken modifier)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifier(localDeclaration, modifier);
        }

        /// <summary>
        /// Creates a new declaration with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Remove(LocalDeclarationStatementSyntax localDeclaration, SyntaxKind kind)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifier(localDeclaration, kind);
        }

        /// <summary>
        /// Creates a new local function with the specified modifier removed.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Remove(LocalFunctionStatementSyntax localFunction, SyntaxToken modifier)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifier(localFunction, modifier);
        }

        /// <summary>
        /// Creates a new local function with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Remove(LocalFunctionStatementSyntax localFunction, SyntaxKind kind)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifier(localFunction, kind);
        }

        /// <summary>
        /// Creates a new parameter with the specified modifier removed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ParameterSyntax Remove(ParameterSyntax parameter, SyntaxToken modifier)
        {
            return ParameterModifierHelper.Instance.RemoveModifier(parameter, modifier);
        }

        /// <summary>
        /// Creates a new parameter with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static ParameterSyntax Remove(ParameterSyntax parameter, SyntaxKind kind)
        {
            return ParameterModifierHelper.Instance.RemoveModifier(parameter, kind);
        }

        /// <summary>
        /// Creates a new incomplete member with the specified modifier removed.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax Remove(IncompleteMemberSyntax incompleteMember, SyntaxToken modifier)
        {
            return IncompleteMemberModifierHelper.Instance.RemoveModifier(incompleteMember, modifier);
        }

        /// <summary>
        /// Creates a new incomplete member with a modifier of the specified kind removed.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax Remove(IncompleteMemberSyntax incompleteMember, SyntaxKind kind)
        {
            return IncompleteMemberModifierHelper.Instance.RemoveModifier(incompleteMember, kind);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax RemoveAt(ClassDeclarationSyntax classDeclaration, int index)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifierAt(classDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax RemoveAt(ConstructorDeclarationSyntax constructorDeclaration, int index)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifierAt(constructorDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax RemoveAt(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, int index)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifierAt(conversionOperatorDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax RemoveAt(DelegateDeclarationSyntax delegateDeclaration, int index)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifierAt(delegateDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax RemoveAt(DestructorDeclarationSyntax destructorDeclaration, int index)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifierAt(destructorDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax RemoveAt(EnumDeclarationSyntax enumDeclaration, int index)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifierAt(enumDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax RemoveAt(EventDeclarationSyntax eventDeclaration, int index)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifierAt(eventDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax RemoveAt(EventFieldDeclarationSyntax eventFieldDeclaration, int index)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifierAt(eventFieldDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax RemoveAt(FieldDeclarationSyntax fieldDeclaration, int index)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifierAt(fieldDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax RemoveAt(IndexerDeclarationSyntax indexerDeclaration, int index)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifierAt(indexerDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax RemoveAt(InterfaceDeclarationSyntax interfaceDeclaration, int index)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifierAt(interfaceDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax RemoveAt(MethodDeclarationSyntax methodDeclaration, int index)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifierAt(methodDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax RemoveAt(OperatorDeclarationSyntax operatorDeclaration, int index)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifierAt(operatorDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax RemoveAt(PropertyDeclarationSyntax propertyDeclaration, int index)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifierAt(propertyDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax RemoveAt(StructDeclarationSyntax structDeclaration, int index)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifierAt(structDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax RemoveAt(AccessorDeclarationSyntax accessorDeclaration, int index)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifierAt(accessorDeclaration, index);
        }

        /// <summary>
        /// Creates a new declaration with a modifier at the specified index removed.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax RemoveAt(LocalDeclarationStatementSyntax localDeclaration, int index)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifierAt(localDeclaration, index);
        }

        /// <summary>
        /// Creates a new local function with a modifier at the specified index removed.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax RemoveAt(LocalFunctionStatementSyntax localFunction, int index)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifierAt(localFunction, index);
        }

        /// <summary>
        /// Creates a new parameter with a modifier at the specified index removed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ParameterSyntax RemoveAt(ParameterSyntax parameter, int index)
        {
            return ParameterModifierHelper.Instance.RemoveModifierAt(parameter, index);
        }

        /// <summary>
        /// Creates a new incomplete member with a modifier at the specified index removed.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax RemoveAt(IncompleteMemberSyntax incompleteMember, int index)
        {
            return IncompleteMemberModifierHelper.Instance.RemoveModifierAt(incompleteMember, index);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax RemoveAccessibility(ClassDeclarationSyntax classDeclaration)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveAccessibility(classDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax RemoveAccessibility(ConstructorDeclarationSyntax constructorDeclaration)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveAccessibility(constructorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax RemoveAccessibility(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveAccessibility(conversionOperatorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax RemoveAccessibility(DelegateDeclarationSyntax delegateDeclaration)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveAccessibility(delegateDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax RemoveAccessibility(DestructorDeclarationSyntax destructorDeclaration)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveAccessibility(destructorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax RemoveAccessibility(EnumDeclarationSyntax enumDeclaration)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveAccessibility(enumDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax RemoveAccessibility(EventDeclarationSyntax eventDeclaration)
        {
            return EventDeclarationModifierHelper.Instance.RemoveAccessibility(eventDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax RemoveAccessibility(EventFieldDeclarationSyntax eventFieldDeclaration)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveAccessibility(eventFieldDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax RemoveAccessibility(FieldDeclarationSyntax fieldDeclaration)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveAccessibility(fieldDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax RemoveAccessibility(IndexerDeclarationSyntax indexerDeclaration)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveAccessibility(indexerDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax RemoveAccessibility(InterfaceDeclarationSyntax interfaceDeclaration)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveAccessibility(interfaceDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax RemoveAccessibility(MethodDeclarationSyntax methodDeclaration)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveAccessibility(methodDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax RemoveAccessibility(OperatorDeclarationSyntax operatorDeclaration)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveAccessibility(operatorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax RemoveAccessibility(PropertyDeclarationSyntax propertyDeclaration)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveAccessibility(propertyDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax RemoveAccessibility(StructDeclarationSyntax structDeclaration)
        {
            return StructDeclarationModifierHelper.Instance.RemoveAccessibility(structDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax RemoveAccessibility(AccessorDeclarationSyntax accessorDeclaration)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveAccessibility(accessorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with accessibility modifiers removed.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax RemoveAccessibility(LocalDeclarationStatementSyntax localDeclaration)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveAccessibility(localDeclaration);
        }

        /// <summary>
        /// Creates a new local function with accessibility modifiers removed.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax RemoveAccessibility(LocalFunctionStatementSyntax localFunction)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveAccessibility(localFunction);
        }

        /// <summary>
        /// Creates a new parameter with accessibility modifiers removed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ParameterSyntax RemoveAccessibility(ParameterSyntax parameter)
        {
            return ParameterModifierHelper.Instance.RemoveAccessibility(parameter);
        }

        /// <summary>
        /// Creates a new incomplete member with accessibility modifiers removed.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax RemoveAccessibility(IncompleteMemberSyntax incompleteMember)
        {
            return IncompleteMemberModifierHelper.Instance.RemoveAccessibility(incompleteMember);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax RemoveAll(ClassDeclarationSyntax classDeclaration)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifiers(classDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax RemoveAll(ConstructorDeclarationSyntax constructorDeclaration)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifiers(constructorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax RemoveAll(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifiers(conversionOperatorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax RemoveAll(DelegateDeclarationSyntax delegateDeclaration)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifiers(delegateDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax RemoveAll(DestructorDeclarationSyntax destructorDeclaration)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifiers(destructorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax RemoveAll(EnumDeclarationSyntax enumDeclaration)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifiers(enumDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax RemoveAll(EventDeclarationSyntax eventDeclaration)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifiers(eventDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax RemoveAll(EventFieldDeclarationSyntax eventFieldDeclaration)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifiers(eventFieldDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax RemoveAll(FieldDeclarationSyntax fieldDeclaration)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifiers(fieldDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax RemoveAll(IndexerDeclarationSyntax indexerDeclaration)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifiers(indexerDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax RemoveAll(InterfaceDeclarationSyntax interfaceDeclaration)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifiers(interfaceDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax RemoveAll(MethodDeclarationSyntax methodDeclaration)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifiers(methodDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax RemoveAll(OperatorDeclarationSyntax operatorDeclaration)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifiers(operatorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax RemoveAll(PropertyDeclarationSyntax propertyDeclaration)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifiers(propertyDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax RemoveAll(StructDeclarationSyntax structDeclaration)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifiers(structDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax RemoveAll(AccessorDeclarationSyntax accessorDeclaration)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifiers(accessorDeclaration);
        }

        /// <summary>
        /// Creates a new declaration with all modifiers removed.
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax RemoveAll(LocalDeclarationStatementSyntax localDeclaration)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifiers(localDeclaration);
        }

        /// <summary>
        /// Creates a new local function with all modifiers removed.
        /// </summary>
        /// <param name="localFunction"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax RemoveAll(LocalFunctionStatementSyntax localFunction)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifiers(localFunction);
        }

        /// <summary>
        /// Creates a new parameter with all modifiers removed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ParameterSyntax RemoveAll(ParameterSyntax parameter)
        {
            return ParameterModifierHelper.Instance.RemoveModifiers(parameter);
        }

        /// <summary>
        /// Creates a new incomplete member with all modifiers removed.
        /// </summary>
        /// <param name="incompleteMember"></param>
        /// <returns></returns>
        public static IncompleteMemberSyntax RemoveAll(IncompleteMemberSyntax incompleteMember)
        {
            return IncompleteMemberModifierHelper.Instance.RemoveModifiers(incompleteMember);
        }
    }
}