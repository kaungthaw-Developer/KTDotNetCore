﻿namespace KTDotNetCore.RestApi.Model
{
    public class BlogListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<BlogDataModels> Data { get; set; }
    }
}
