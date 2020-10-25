// <copyright file="DaytimeService.cs" author="Philip 'Zeruxky' Wille">
// Copyright (c) Philip 'Zeruxky' Wille. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace DaytimeService.NET
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides common functionality for running a <see cref="DaytimeService"/> according to RFC 867.
    /// </summary>
    public abstract class DaytimeService
    {
        /// <summary>
        /// Runs the <see cref="DaytimeService"/> in an infinity loop.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronously run operation of the <see cref="DaytimeService"/>.</returns>
        public abstract Task RunAsync();

        /// <summary>
        /// Converts the given <paramref name="dateTime"/> to a <see cref="byte"/> array.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <remarks>
        /// To get the <see cref="string"/> representation of the <paramref name="dateTime"/> the function uses the <see cref="DateTimeOffset.ToString()"/> method.
        /// The format from RFC 1123 will be used (e.g 2009-06-15T13:45:30 -> Mon, 15 Jun 2009 20:45:30 GMT).
        /// </remarks>
        /// <returns>The <see cref="byte"/> array that are equivalent to the <see cref="string"/> representation of the <paramref name="dateTime"/>.</returns>
        protected static byte[] ToBytes(DateTimeOffset dateTime)
        {
            var dateTimeAsString = dateTime.ToString("R");
            var bytes = Encoding.ASCII.GetBytes(dateTimeAsString);
            return bytes;
        }
    }
}