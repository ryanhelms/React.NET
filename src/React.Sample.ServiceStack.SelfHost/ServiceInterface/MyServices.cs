using React.Sample.ServiceStack.SelfHost.ServiceModel;
using ServiceStack;

namespace React.Sample.ServiceStack.SelfHost.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse {Result = "Hello, {0}!".Fmt(request.Name)};
        }
    }
}