using System.IO;
using Funq;
using React.Sample.ServiceStack.SelfHost.ServiceInterface;
using ServiceStack;
using ServiceStack.Razor;

namespace React.Sample.ServiceStack.SelfHost
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        ///     Default constructor.
        ///     Base constructor requires a name and assembly to locate web service classes.
        /// </summary>
        public AppHost()
            : base("React.Sample.ServiceStack.SelfHost", typeof (MyServices).Assembly)
        {
        }

        /// <summary>
        ///     Application specific configuration
        ///     This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());

            Plugins.Add(new RazorFormat());
            SetConfig(new HostConfig
            {
#if DEBUG
                DebugMode = true,
                WebHostPhysicalPath = Path.GetFullPath(Path.Combine("~".MapServerPath(), "..", ".."))
#endif
            });
        }
    }
}