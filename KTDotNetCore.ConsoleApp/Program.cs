﻿using KTDotNetCore.ConsoleApp.AdoDotNetExamples;
using System;
using KTDotNetCore.ConsoleApp.DapperExamples;
using KTDotNetCore.ConsoleApp.EFCoreExamples;
using KTDotNetCore.ConsoleApp.HttpClientExamples;

//namespace KTDotNetCore.ConsoleApp
//{
//    //internal class Program
//    //{
//    //    static void Main(string[] args)
//    //    {
//    //        Console.WriteLine("Hello World!");
//    //       //AdoDotNetExample ado = new AdoDotNetExample();
//    //       //DapperExample dapper=new DapperExample();
//    //         //   ado.Run();
//    //        //dapper.Run();
//    //       EFCoreExample efCore=new EFCoreExample();
//    //        efCore.Run();

//    //        Console.WriteLine("Press any key to continue....");
//    //        Console.ReadKey();

//    //    }
//    //}

//}

Console.WriteLine("Please wait for api...");
Console.ReadKey();

HttpClientExamples httpClientExamples = new HttpClientExamples();

await httpClientExamples.Run();
Console.ReadKey();