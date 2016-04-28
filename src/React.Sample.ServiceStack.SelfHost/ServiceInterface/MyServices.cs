using React.Sample.SSS.SelfHost.ServiceModel;
using ServiceStack;

namespace React.Sample.SSS.SelfHost.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(HelloModel request)
        {
            return new HelloModelResponse {Result = "Hello, {0}!".Fmt(request.Name)};
        }
    }
}