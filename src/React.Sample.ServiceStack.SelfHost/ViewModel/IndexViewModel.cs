using System.Collections.Generic;
using React.Sample.SSS.SelfHost.ServiceModel;

namespace React.Sample.SSS.SelfHost.ViewModel
{
    public class IndexViewModel
    {
        public List<CommentModel> Comments { get; set; }
        public int CommentsPerPage { get; set; }
        public int Page { get; set; }
    }
}