namespace KTDotNetCore.ConsoleApp.Model
{
    public class BlogResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public BlogDataModels Data { get; set; }
    }
}
