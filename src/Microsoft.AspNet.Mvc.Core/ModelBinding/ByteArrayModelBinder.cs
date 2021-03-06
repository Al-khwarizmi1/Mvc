// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;

namespace Microsoft.AspNet.Mvc.ModelBinding
{
    /// <summary>
    /// ModelBinder to bind Byte Arrays.
    /// </summary>
    public class ByteArrayModelBinder : IModelBinder
    {
        /// <inheritdoc />
        public Task<ModelBindingResult> BindModelAsync([NotNull] ModelBindingContext bindingContext)
        {
            return Task.FromResult(BindModel(bindingContext));
        }

        private ModelBindingResult BindModel(ModelBindingContext bindingContext)
        {
            // Check if this binder applies.
            if (bindingContext.ModelType != typeof(byte[]))
            {
                return null;
            }

            // Check for missing data case 1: There was no <input ... /> element containing this data.
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return new ModelBindingResult(model: null, key: bindingContext.ModelName, isModelSet: false);
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            // Check for missing data case 2: There was an <input ... /> element but it was left blank.
            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return new ModelBindingResult(model: null, key: bindingContext.ModelName, isModelSet: false);
            }

            try
            {
                var model = Convert.FromBase64String(value);
                var validationNode = new ModelValidationNode(
                    bindingContext.ModelName,
                    bindingContext.ModelMetadata,
                    model);

                return new ModelBindingResult(
                    model,
                    bindingContext.ModelName,
                    isModelSet: true,
                    validationNode: validationNode);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ex);
            }

            // Matched the type (byte[]) only this binder supports. As in missing data cases, always tell the model
            // binding system to skip other model binders i.e. return non-null.
            return new ModelBindingResult(model: null, key: bindingContext.ModelName, isModelSet: false);
        }
    }
}