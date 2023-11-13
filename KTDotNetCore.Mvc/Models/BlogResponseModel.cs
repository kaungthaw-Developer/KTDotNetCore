using KTDotNetCore.Mvc.Model;

namespace KTDotNetCore.Mvc.Models
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModels Data { get; set; }
    }

    public class BlogListResponseModel
    {
        public List<BlogDataModels> BlogList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }   
    }
}
