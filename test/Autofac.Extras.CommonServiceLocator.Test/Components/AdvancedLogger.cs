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