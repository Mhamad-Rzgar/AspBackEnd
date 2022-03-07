using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // لێرەوە پرۆگرامەکە ڕەن ئەبێ
            CreateHostBuilder(args).Build().Run(); 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // فایلی ستارت ئەپ بانگەکەینەوە بۆی لێرەوە
                    // ڕەن ببێ و دەوری هۆستەکە ببینی لەسەر کۆمپیوتەرەکەمان
                    webBuilder.UseStartup<Startup>(); 
                });
    }
}
