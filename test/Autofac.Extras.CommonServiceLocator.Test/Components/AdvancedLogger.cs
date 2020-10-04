// Copyright (c) Autofac Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Autofac.Extras.CommonServiceLocator.Test.Components
{
    public class AdvancedLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine("Log: {0}", msg);
        }
    }
}