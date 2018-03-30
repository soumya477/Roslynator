﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

#pragma warning disable RCS1016, RCS1081, RCS1163, RCS1164, RCS1175, RCS1176

namespace Roslynator.CSharp.Analyzers.Tests
{
    internal static class UnusedMemberDeclaration
    {
        private static class Foo
        {
            private const string _f = "";
            private static readonly string _f2, _f3;

            private static void FooMethod()
            {
                string s = _f2;

                EventHandler eh = FooEvent3;

                FooMethod<string>();

                s.FooExtensionMethod<string>();
            }

            private static void FooMethod<T>()
            {
            }

            private static string FooProperty { get; } = _f3;

            private static event EventHandler FooEvent;
            private static event EventHandler FooEvent2, FooEvent3;

            private delegate void FooDelegate();
        }

        private static void FooExtensionMethod<T>(this T value)
        {
        }

        private static void FooRecursion()
        {
            FooRecursion();
        }

        private static void FooRecursion<T>()
        {
            FooRecursion<T>();
        }

        private static void FooRecursion2<T>()
        {
            FooRecursion2<string>();
        }

        private static void FooExtensionMethodRecursion(this string value)
        {
            FooExtensionMethodRecursion(value);
        }

        private static void FooExtensionMethodRecursion2(this string value)
        {
            value.FooExtensionMethodRecursion2();
        }

        private static void FooExtensionMethodRecursion<T>(this T value)
        {
            FooExtensionMethodRecursion<T>(value);
        }

        private static void FooExtensionMethodRecursion2<T>(this T value)
        {
            value.FooExtensionMethodRecursion2<T>();
        }

        private static void FooExtensionMethodRecursion3<T>(this T value)
        {
            string s = null;

            FooExtensionMethodRecursion3<string>(s);
        }

        private static void FooExtensionMethodRecursion4<T>(this T value)
        {
            string s = null;

            s.FooExtensionMethodRecursion4<string>();
        }

        // n

        private partial class FooPartial
        {
            private void FooMethod()
            {
            }
        }

        private partial class FooPartial
        {
        }

        private class Base
        {
            public Base(string value)
            {
            }
        }

        private class Derived : Base
        {
            public Derived(string value)
                : base(Bar())
            {
            }

            private static string Bar()
            {
                return null;
            }
        }

        [FooAttribute(A)]
        public class Foo2
        {
            private const string A = "";
            private const string B = "";
            private const string C = "";
            private const int D = 0;

            [FooAttribute(B)]
            public Foo2(string x = C)
            {
            }

            public enum FooEnum
            {
                None = D
            }
        }

        [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
        private sealed class FooAttribute : Attribute
        {
            public FooAttribute(string value)
            {
            }
        }

        private static class Program
        {
            private static void Main(string[] args)
            {
            }

            private static void Main()
            {
            }
        }
    }
}
