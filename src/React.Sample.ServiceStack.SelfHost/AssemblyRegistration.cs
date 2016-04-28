using Funq;
using JavaScriptEngineSwitcher.Msie;
using JavaScriptEngineSwitcher.Msie.Configuration;
using JavaScriptEngineSwitcher.V8;
using React.TinyIoC;

namespace React.Sample.SSS.SelfHost
{
    /// <summary>
    /// Handles registration of ReactJS.NET components that are only applicable
    /// when used with Owin.
    /// </summary>
    public class AssemblyRegistration : IAssemblyRegistration
    {
        /// <summary>
        /// Registers components in the React IoC container
        /// </summary>
        /// <param name="container">Container to register components in</param>
        public void Register(TinyIoCContainer container)
        {
            container.Register<IReactSiteConfiguration>((c, o) => ReactSiteConfiguration.Configuration);
            container.Register<IFileCacheHash, FileCacheHash>().AsPerRequestSingleton();
            container.Register<IJavaScriptEngineFactory, JavaScriptEngineFactory>().AsSingleton();

            container.Register<IReactEnvironment, ReactEnvironment>().AsPerRequestSingleton();
            RegisterSupportedEngines(container);
        }

        /// <summary>
        /// Registers JavaScript engines that may be able to run in the current environment
        /// </summary>
        /// <param name="container"></param>
        private void RegisterSupportedEngines(TinyIoCContainer container)
        {
            if (JavaScriptEngineUtils.EnvironmentSupportsClearScript())
            {
                container.Register(new JavaScriptEngineFactory.Registration
                {
                    Factory = () => new V8JsEngine(),
                    Priority = 10
                }, "ClearScriptV8");
            }
            if (JavaScriptEngineUtils.EnvironmentSupportsVroomJs())
            {
                container.Register(new JavaScriptEngineFactory.Registration
                {
                    Factory = () => new VroomJsEngine(),
                    Priority = 10
                }, "VroomJs");
            }

            container.Register(new JavaScriptEngineFactory.Registration
            {
                Factory = () => new MsieJsEngine(new MsieConfiguration { EngineMode = JsEngineMode.ChakraEdgeJsRt }),
                Priority = 20
            }, "MsieChakraEdgeRT");
            container.Register(new JavaScriptEngineFactory.Registration
            {
                Factory = () => new MsieJsEngine(new MsieConfiguration { EngineMode = JsEngineMode.ChakraIeJsRt }),
                Priority = 30
            }, "MsieChakraIeRT");
            container.Register(new JavaScriptEngineFactory.Registration
            {
                Factory = () => new MsieJsEngine(new MsieConfiguration { EngineMode = JsEngineMode.ChakraActiveScript }),
                Priority = 40
            }, "MsieChakraActiveScript");
        }
    }
}