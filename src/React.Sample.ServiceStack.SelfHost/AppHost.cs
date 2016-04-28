using System.IO;
using Funq;
using React.Sample.SSS.SelfHost.ServiceInterface;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Razor;

namespace React.Sample.SSS.SelfHost
{
    public class AppHost : AppSelfHostBase
    {
        /// <summary>
        ///     Default constructor.
        ///     Base constructor requires a name and assembly to locate web service classes.
        /// </summary>
        public AppHost() : base("React.Sample.SSS.SelfHost", typeof (MyServices).Assembly) { }

        /// <summary>
        ///     Application specific configuration
        ///     This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            ILog log = LogManager.GetLogger(GetType());

            container.Register<ILog>(log);
            
            ReactConfig.Configure();

            var environment = ReactEnvironment.Current;
            var component = environment.CreateComponent("HelloWorld", new { name = "Daniel" });
            var html = component.RenderHtml(renderServerOnly: true);
            
            Plugins.Add(new RazorFormat
            {
                LoadFromAssemblies = { typeof(MyServices).Assembly }
            });

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