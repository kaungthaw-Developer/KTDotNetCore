namespace KTDotNetCore.RestApi.Model
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModels Data { get; set; }
    }

    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModels> Data { get; set; }
    }
}
