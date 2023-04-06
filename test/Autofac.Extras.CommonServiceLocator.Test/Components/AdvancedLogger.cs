// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Autofac.Extras.CommonServiceLocator.Test.Components
{
    public class AdvancedLogger : ILogger
    {
        [SuppressMessage("CA1303", "CA1303", Justification = "We don't need to localize strings in unit tests.")]
        public void Log(string msg)
        {
            Console.WriteLine("Log: {0}", msg);
        }
    }
}
