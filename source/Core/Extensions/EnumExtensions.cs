﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis;

namespace Roslynator
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        #region Accessibility
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessibility"></param>
        /// <param name="accessibility1"></param>
        /// <param name="accessibility2"></param>
        /// <returns></returns>
        public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2)
        {
            return accessibility == accessibility1
                || accessibility == accessibility2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessibility"></param>
        /// <param name="accessibility1"></param>
        /// <param name="accessibility2"></param>
        /// <param name="accessibility3"></param>
        /// <returns></returns>
        public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2, Accessibility accessibility3)
        {
            return accessibility == accessibility1
                || accessibility == accessibility2
                || accessibility == accessibility3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessibility"></param>
        /// <param name="accessibility1"></param>
        /// <param name="accessibility2"></param>
        /// <param name="accessibility3"></param>
        /// <param name="accessibility4"></param>
        /// <returns></returns>
        public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2, Accessibility accessibility3, Accessibility accessibility4)
        {
            return accessibility == accessibility1
                || accessibility == accessibility2
                || accessibility == accessibility3
                || accessibility == accessibility4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessibility"></param>
        /// <param name="accessibility1"></param>
        /// <param name="accessibility2"></param>
        /// <param name="accessibility3"></param>
        /// <param name="accessibility4"></param>
        /// <param name="accessibility5"></param>
        /// <returns></returns>
        public static bool Is(this Accessibility accessibility, Accessibility accessibility1, Accessibility accessibility2, Accessibility accessibility3, Accessibility accessibility4, Accessibility accessibility5)
        {
            return accessibility == accessibility1
                || accessibility == accessibility2
                || accessibility == accessibility3
                || accessibility == accessibility4
                || accessibility == accessibility5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessibility"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsMoreRestrictiveThan(this Accessibility accessibility, Accessibility other)
        {
            switch (other)
            {
                case Accessibility.Public:
                    {
                        return accessibility == Accessibility.Internal
                            || accessibility == Accessibility.ProtectedOrInternal
                            || accessibility == Accessibility.ProtectedAndInternal
                            || accessibility == Accessibility.Protected
                            || accessibility == Accessibility.Private;
                    }
                case Accessibility.Internal:
                    {
                        return accessibility == Accessibility.ProtectedAndInternal
                            || accessibility == Accessibility.Private;
                    }
                case Accessibility.ProtectedOrInternal:
                    {
                        return accessibility == Accessibility.Internal
                            || accessibility == Accessibility.Protected
                            || accessibility == Accessibility.ProtectedAndInternal
                            || accessibility == Accessibility.Private;
                    }
                case Accessibility.ProtectedAndInternal:
                case Accessibility.Protected:
                    {
                        return accessibility == Accessibility.Private;
                    }
                case Accessibility.Private:
                    {
                        return false;
                    }
            }

            return false;
        }

        internal static bool IsSingleTokenAccessibility(this Accessibility accessibility)
        {
            return accessibility.Is(
                Accessibility.Public,
                Accessibility.Internal,
                Accessibility.Protected,
                Accessibility.Private);
        }

        internal static bool ContainsProtected(this Accessibility accessibility)
        {
            return accessibility.Is(Accessibility.Protected, Accessibility.ProtectedAndInternal, Accessibility.ProtectedOrInternal);
        }

        internal static Accessibilities GetAccessibilities(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.NotApplicable:
                    return Accessibilities.None;
                case Accessibility.Private:
                    return Accessibilities.Private;
                case Accessibility.ProtectedAndInternal:
                    return Accessibilities.ProtectedAndInternal;
                case Accessibility.Protected:
                    return Accessibilities.Protected;
                case Accessibility.Internal:
                    return Accessibilities.Internal;
                case Accessibility.ProtectedOrInternal:
                    return Accessibilities.ProtectedOrInternal;
                case Accessibility.Public:
                    return Accessibilities.Public;
                default:
                    throw new ArgumentException("", nameof(accessibility));
            }
        }
        #endregion Accessibility

        #region MethodKind
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodKind"></param>
        /// <param name="methodKind1"></param>
        /// <param name="methodKind2"></param>
        /// <returns></returns>
        public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2)
        {
            return methodKind == methodKind1
                || methodKind == methodKind2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodKind"></param>
        /// <param name="methodKind1"></param>
        /// <param name="methodKind2"></param>
        /// <param name="methodKind3"></param>
        /// <returns></returns>
        public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2, MethodKind methodKind3)
        {
            return methodKind == methodKind1
                || methodKind == methodKind2
                || methodKind == methodKind3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodKind"></param>
        /// <param name="methodKind1"></param>
        /// <param name="methodKind2"></param>
        /// <param name="methodKind3"></param>
        /// <param name="methodKind4"></param>
        /// <returns></returns>
        public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2, MethodKind methodKind3, MethodKind methodKind4)
        {
            return methodKind == methodKind1
                || methodKind == methodKind2
                || methodKind == methodKind3
                || methodKind == methodKind4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodKind"></param>
        /// <param name="methodKind1"></param>
        /// <param name="methodKind2"></param>
        /// <param name="methodKind3"></param>
        /// <param name="methodKind4"></param>
        /// <param name="methodKind5"></param>
        /// <returns></returns>
        public static bool Is(this MethodKind methodKind, MethodKind methodKind1, MethodKind methodKind2, MethodKind methodKind3, MethodKind methodKind4, MethodKind methodKind5)
        {
            return methodKind == methodKind1
                || methodKind == methodKind2
                || methodKind == methodKind3
                || methodKind == methodKind4
                || methodKind == methodKind5;
        }
        #endregion MethodKind

        #region SpecialType
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specialType"></param>
        /// <param name="specialType1"></param>
        /// <param name="specialType2"></param>
        /// <returns></returns>
        public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2)
        {
            return specialType == specialType1
                || specialType == specialType2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specialType"></param>
        /// <param name="specialType1"></param>
        /// <param name="specialType2"></param>
        /// <param name="specialType3"></param>
        /// <returns></returns>
        public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2, SpecialType specialType3)
        {
            return specialType == specialType1
                || specialType == specialType2
                || specialType == specialType3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specialType"></param>
        /// <param name="specialType1"></param>
        /// <param name="specialType2"></param>
        /// <param name="specialType3"></param>
        /// <param name="specialType4"></param>
        /// <returns></returns>
        public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2, SpecialType specialType3, SpecialType specialType4)
        {
            return specialType == specialType1
                || specialType == specialType2
                || specialType == specialType3
                || specialType == specialType4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specialType"></param>
        /// <param name="specialType1"></param>
        /// <param name="specialType2"></param>
        /// <param name="specialType3"></param>
        /// <param name="specialType4"></param>
        /// <param name="specialType5"></param>
        /// <returns></returns>
        public static bool Is(this SpecialType specialType, SpecialType specialType1, SpecialType specialType2, SpecialType specialType3, SpecialType specialType4, SpecialType specialType5)
        {
            return specialType == specialType1
                || specialType == specialType2
                || specialType == specialType3
                || specialType == specialType4
                || specialType == specialType5;
        }
        #endregion SpecialType

        #region TypeKind
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeKind"></param>
        /// <param name="typeKind1"></param>
        /// <param name="typeKind2"></param>
        /// <returns></returns>
        public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2)
        {
            return typeKind == typeKind1
                || typeKind == typeKind2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeKind"></param>
        /// <param name="typeKind1"></param>
        /// <param name="typeKind2"></param>
        /// <param name="typeKind3"></param>
        /// <returns></returns>
        public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2, TypeKind typeKind3)
        {
            return typeKind == typeKind1
                || typeKind == typeKind2
                || typeKind == typeKind3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeKind"></param>
        /// <param name="typeKind1"></param>
        /// <param name="typeKind2"></param>
        /// <param name="typeKind3"></param>
        /// <param name="typeKind4"></param>
        /// <returns></returns>
        public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2, TypeKind typeKind3, TypeKind typeKind4)
        {
            return typeKind == typeKind1
                || typeKind == typeKind2
                || typeKind == typeKind3
                || typeKind == typeKind4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeKind"></param>
        /// <param name="typeKind1"></param>
        /// <param name="typeKind2"></param>
        /// <param name="typeKind3"></param>
        /// <param name="typeKind4"></param>
        /// <param name="typeKind5"></param>
        /// <returns></returns>
        public static bool Is(this TypeKind typeKind, TypeKind typeKind1, TypeKind typeKind2, TypeKind typeKind3, TypeKind typeKind4, TypeKind typeKind5)
        {
            return typeKind == typeKind1
                || typeKind == typeKind2
                || typeKind == typeKind3
                || typeKind == typeKind4
                || typeKind == typeKind5;
        }
        #endregion TypeKind
    }
}
