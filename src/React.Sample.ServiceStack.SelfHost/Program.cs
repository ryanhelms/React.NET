using System;
using System.Configuration;
using System.Diagnostics;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Logging.EventLog;
using ServiceStack.Text;

namespace React.Sample.SSS.SelfHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var license = ConfigurationManager.AppSettings["servicestack:license"];
            
            if (!string.IsNullOrEmpty(license))
                Licensing.RegisterLicense(license);

            LogManager.LogFactory = new EventLogFactory("React.Sample.SSS.SelfHost", "Application");
            ILog log = LogManager.GetLogger(typeof(Program));

            try
            {
                var appHost = new AppHost().Init().Start("http://*:8088/");

                appHost.StartUpErrors.Each(error => log.Error(error)); 
                ;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            "ServiceStack Self Host with Razor listening at http://localhost:8088 ".Print();

            Process.Start("http://localhost:8088/");

            Console.ReadLine();
        }
    }
}