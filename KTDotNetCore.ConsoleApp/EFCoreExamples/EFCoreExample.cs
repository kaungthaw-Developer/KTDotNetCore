using KTDotNetCore.ConsoleApp.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        public void Run()
        {
            //Create("title","author","contet");
            // Read();
            //Edit(12);
            //Edit(14);
            //Update(12, "title-1", "author-1", "contet-1");
            Delete(22);
        }
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.OrderByDescending(x =>x.Blog_Id).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);

            }
        }
        public void Create(string title,string author,string content)
        {
            BlogDataModels blogData = new BlogDataModels()
            {
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blogData);
            var result=db.SaveChanges();
            string message = result > 0 ? "succeful Create  " : "someting wrong";
            Console.WriteLine(message);
        }
        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModels item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            //db.Blogs.Where(x => x.Blog_Id==id).FirstOrDefault();

            if (item == null)
            {
                Console.WriteLine("Data not found");
                return;
            }

             db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "succeful Delete  " : "someting wrong";
            Console.WriteLine(message);
        }
        public void Update(int id,string author,string title,string content)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModels item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item == null)
            {
                Console.WriteLine("Data not found");
                return;
            }

            item.Blog_Id = id;
            item.Blog_Title = title;
            item.Blog_Author = author;  
            item.Blog_Content = content;
           int result= db.SaveChanges();

            string message = result > 0 ? "succeful Update  " : "someting wrong";
            Console.WriteLine(message);
        }
        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModels item =db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            //db.Blogs.Where(x => x.Blog_Id==id).FirstOrDefault();

            if (item == null)
            {
                Console.WriteLine("Data not found");
                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
            Console.WriteLine(item.Blog_Title);
        }
        
    }
}
