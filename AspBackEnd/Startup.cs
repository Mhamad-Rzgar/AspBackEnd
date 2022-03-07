using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization; // جەیسن سیریەڵایزەر بەکارەهێنین

namespace AspBackEnd {
    public class Startup {
        // لەم ئۆنجیکتەوە سێرڤەرەکەمان ڕێکەخەین
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // enable CORS for allowing react js server to connect
            // کۆرس مەبەستمان ئەو یاسایەیە کە ڕێگەمان پێدەیات ڕیکوێست و داواکاری لە تیەری سێ وەرگرین
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // serializer krdny Json
            // جەیسنەکە سیریەڵایزەکەین
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            // zyakrdny hamw controllery apiyakan
            // هەموو هەمووی کۆنتڕۆلەری ئەی پی ئایەکان
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            // کۆنفیگەرکردنەکە لێرەوە دەکرێت
            // Enale cors
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            // ڕاوتین زیا ئەکەین بۆ سیرڤەرەکە کە تیەری دووە
            app.UseRouting();   

            app.UseAuthorization();

            // ئەمە وامان لێکردەوە بەس ئەی پی ئایەکان بەکارەهێنین
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
