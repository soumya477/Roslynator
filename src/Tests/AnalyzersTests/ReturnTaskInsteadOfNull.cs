// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

#pragma warning disable CS0162, CS0168, CS8321, RCS1004, RCS1007, RCS1016, RCS1021, RCS1048, RCS1163

namespace Roslynator.CSharp.Analyzers.Tests
{
    internal static class ReturnTaskInsteadOfNull
    {
        private class FooNull
        {
            internal Task<string> FooAsync()
            {
                Func<string, Task<string>> func1 = f => null;

                Func<string, Task<string>> func2 = f =>
                {
                    if (true)
                        return null;

                    return null;
                };

                Func<string, Task<string>> func3 = (f) => null;

                Func<string, Task<string>> func4 = (f) =>
                {
                    if (true)
                        return null;

                    return null;
                };

                Func<string, Task<string>> func5 = delegate (string f)
                {
                    if (true)
                        return null;

                    return null;
                };

                return null;

                Task<string> FooLocalAsync(string value)
                {
                    if (true)
                        return null;

                    return null;
                }
            }

            internal Task<string> PropertyAsync
            {
                get
                {
                    if (true)
                        return null;

                    return null;
                }
            }

            internal Task<string> Property2Async
            {
                get => null;
            }

            internal Task<string> Property3Async => null;

            internal Task<string> this[int index]
            {
                get
                {
                    if (true)
                        return null;

                    return null;
                }
            }

            internal Task<string> this[int index1, int index2]
            {
                get => null;
            }

            internal Task<string> this[int index, int index2, int index3] => null;
        }

        private class FooDefault
        {
            internal Task<string> FooAsync()
            {
                Func<string, Task<string>> func1 = f => default(Task<string>);

                Func<string, Task<string>> func2 = f =>
                {
                    if (true)
                        return default(Task<string>);

                    return default(Task<string>);
                };

                Func<string, Task<string>> func3 = (f) => default(Task<string>);

                Func<string, Task<string>> func4 = (f) =>
                {
                    if (true)
                        return default(Task<string>);

                    return default(Task<string>);
                };

                Func<string, Task<string>> func5 = delegate (string f)
                {
                    if (true)
                        return default(Task<string>);

                    return default(Task<string>);
                };

                return default(Task<string>);

                Task<string> FooLocalAsync()
                {
                    if (true)
                        return default(Task<string>);

                    return default(Task<string>);
                }
            }

            internal Task<string> PropertyAsync
            {
                get
                {
                    if (true)
                        return default(Task<string>);

                    return default(Task<string>);
                }
            }

            internal Task<string> Property2Async
            {
                get => default(Task<string>);
            }

            internal Task<string> Property3Async => default(Task<string>);

            internal Task<string> this[int index]
            {
                get
                {
                    if (true)
                        return default(Task<string>);

                    return default(Task<string>);
                }
            }

            internal Task<string> this[int index1, int index2]
            {
                get => default(Task<string>);
            }

            internal Task<string> this[int index, int index2, int index3] => default(Task<string>);
        }

        private class FooConditionalAccess
        {
            internal Task<string> FooAsync()
            {
                Func<string, Task<string>> func1 = f => _foo?.GetAsync();

                Func<string, Task<string>> func2 = f =>
                {
                    if (true)
                        return _foo?.GetAsync();

                    return _foo?.GetAsync();
                };

                Func<string, Task<string>> func3 = (f) => _foo?.GetAsync();

                Func<string, Task<string>> func4 = (f) =>
                {
                    if (true)
                        return _foo?.GetAsync();

                    return _foo?.GetAsync();
                };

                Func<string, Task<string>> func5 = delegate (string f)
                {
                    if (true)
                        return _foo?.GetAsync();

                    return _foo?.GetAsync();
                };

                return _foo?.GetAsync();

                Task<string> FooLocalAsync(string value)
                {
                    if (true)
                        return _foo?.GetAsync();

                    return _foo?.GetAsync();
                }
            }

            internal Task<string> PropertyAsync
            {
                get
                {
                    if (true)
                        return _foo?.GetAsync();

                    return _foo?.GetAsync();
                }
            }

            internal Task<string> Property2Async
            {
                get => _foo?.GetAsync();
            }

            internal Task<string> Property3Async => _foo?.GetAsync();

            internal Task<string> this[int index]
            {
                get
                {
                    if (true)
                        return _foo?.GetAsync();

                    return _foo?.GetAsync();
                }
            }

            internal Task<string> this[int index1, int index2]
            {
                get => _foo?.GetAsync();
            }

            internal Task<string> this[int index, int index2, int index3] => _foo?.GetAsync();

            private class Foo
            {
                internal Task<string> GetAsync()
                {
                    return Task.FromResult<string>(null);
                }
            }

            private readonly Foo _foo;
        }
    }
}
