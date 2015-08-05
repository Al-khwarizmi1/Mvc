﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor.Precompilation;
using Xunit;

namespace Microsoft.Framework.DependencyInjection
{
    public class MvcRazorMvcBuilderExtensionsTest
    {
        [Fact]
        public void IsValidRazorFileInfoCollection_ReturnsFalse_IfTypeIsAbstract()
        {
            // Arrange
            var type = typeof(AbstractRazorFileInfoCollection);

            // Act
            var result = MvcRazorMvcBuilderExtensions.IsValidRazorFileInfoCollection(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidRazorFileInfoCollection_ReturnsFalse_IfTypeHasGenericParameters()
        {
            // Arrange
            var type = typeof(GenericRazorFileInfoCollection<>);

            // Act
            var result = MvcRazorMvcBuilderExtensions.IsValidRazorFileInfoCollection(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidRazorFileInfoCollection_ReturnsFalse_IfTypeDoesNotHaveDefaultConstructor()
        {
            // Arrange
            var type = typeof(ParameterConstructorRazorFileInfoCollection);

            // Act
            var result = MvcRazorMvcBuilderExtensions.IsValidRazorFileInfoCollection(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidRazorFileInfoCollection_ReturnsFalse_IfTypeDoesNotDeriveFromRazorFileInfoCollection()
        {
            // Arrange
            var type = typeof(NonSubTypeRazorFileInfoCollection);

            // Act
            var result = MvcRazorMvcBuilderExtensions.IsValidRazorFileInfoCollection(type);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidRazorFileInfoCollection_ReturnsTrue_IfTypeDerivesFromRazorFileInfoCollection()
        {
            // Arrange
            var type = typeof(ViewCollection);

            // Act
            var result = MvcRazorMvcBuilderExtensions.IsValidRazorFileInfoCollection(type);

            // Assert
            Assert.True(result);
        }

        private abstract class AbstractRazorFileInfoCollection : RazorFileInfoCollection
        {
        }

        private class GenericRazorFileInfoCollection<TVal> : RazorFileInfoCollection
        {
        }

        private class ParameterConstructorRazorFileInfoCollection : RazorFileInfoCollection
        {
            public ParameterConstructorRazorFileInfoCollection(string value)
            {
            }
        }

        private class NonSubTypeRazorFileInfoCollection : Controller
        {
        }

        private class ViewCollection : RazorFileInfoCollection
        {
        }
    }
}
