﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace CheckContracts
{
    /// <summary>
    /// Do convertions with detailed exceptions
    /// </summary>
    public static class SafeCast
    {
        /// <summary>
        /// Cast input value to the target type. It can be useful during implementing <typeparamref name="IValueConverter"/> interface.
        /// Method raises ArgumentNullException in case of null argument.
        /// </summary>
        [ContractAnnotation("input:null => halt")]
        public static TResult CastArgument<TResult>(object input, [InvokerParameterName] string argumentName)
            where TResult : class
        {
            Validate.ArgumentIsNotNull(input, argumentName);

            var result = input as TResult;

            Validate.ArgumentCondition(
                !ReferenceEquals(null, result),
                argumentName,
                "Invalid argument {0} with type {1}: unable to cast it to the {2}",
                argumentName,
                input.GetType().Name,
                typeof(TResult).Name);

            return (TResult)input;
        }

        /// <summary>
        /// Cast input value to the target type. It can be useful during implementing <typeparamref name="IValueConverter"/> interface.
        /// Method raises InvalidOperationException in case of <paramref name="input"/>.
        /// </summary>
        [ContractAnnotation("input:null => halt")]
        [StringFormatMethod("messageFormat")]
        public static TResult Cast<TResult>([NotNull] object input, string messageFormat, params object[] arguments)
            where TResult : class
        {
            Validate.IsNotNull(input, messageFormat, arguments);

            var result = input as TResult;

            Validate.Condition(!ReferenceEquals(null, result), messageFormat, arguments);

            return (TResult)input;
        }
    }
}
