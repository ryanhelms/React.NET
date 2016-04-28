
using ServiceStack;

namespace React.Sample.SSS.SelfHost.ServiceModel
{
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class HelloModel : IReturn<HelloModelResponse>
    {
        public string Name { get; set; }
    }

    public class HelloModelResponse
    {
        public string Result { get; set; }
    }
}