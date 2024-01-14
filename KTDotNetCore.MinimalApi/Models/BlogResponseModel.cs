
namespace KTDotNetCore.MinimalApi.Models
{

    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModels Data { get; set; }

    }
}
