using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;



// ئەمە ئەی پی ئای مای ئێس کیوئێڵ سیەرڤەرەکەیە
namespace AspBackEnd.Controllers {
    [Route("api/SqlServer")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() {
            // Api/SqlServer وەرگری مێثۆدی گێت لەم لینکەوە
            // ئەم ئیشانەی خوارەوەمان بۆ دەکات

            //ئەم کیوەیە کۆتا فایلمان بۆ ئەگەڕێنێتەوە لە تیەری یەکەوە بۆ تیەری دوو
            String qurey = @"SELECT TOP 1 * FROM[mytestdb].[dbo].[image] ORDER BY[imageId] DESC";
            
            // وەرگرتنەوەی کۆنیکشن سترینگەکەی لە فایلی ئاپ سێتینگ کە هەموو ڕێکخستنەکانی تیایە
           String _sqldataSourse = _configuration.GetConnectionString("AspSqlServerCon");

            // درووستکردنی دەیتاتەیبڵ وەک بۆکسێک
            DataTable dataTable = new DataTable();

            // درووستکردنی خوێنەرەوەی کیوریەکە
            SqlDataReader myReader;

            // لێرە کۆنێکشنەکە درووست دەکەین و کماندەکە ڕەن ئەکەین و ئیکسیکیوتی ئەکەین
            using (SqlConnection mycon = new SqlConnection(_sqldataSourse)) {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    // ئەنجامەکەی ئەخەینە ناو ئەم بۆکسەوە کە وەک تەیبڵێک وایە
                    dataTable.Load(myReader);

                    // داخستنەوەی ڕیدەر و کۆنێکشنەکە بۆی سێرڤەرەکەمان قورس نەکا
                    myReader.Close();
                    mycon.Close();
                }
            }

            // گەڕانەوەی دەیتاکان بۆ تیەری سێ لێرەوە دەبێت لەسەر شێوەی جەیسن
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post() {
            // Api/SqlServer وەرگری مێثۆدی پۆست لەم لینکەوە
            // ئەم ئیشانەی خوارەوەمان بۆ دەکات


            // وەرگرتنەوەی ئەو دەیتایەی لە تیەری سێوە بۆمان یەت کە داتای فایلەکەیە
            var imageData = HttpContext.Request.Form["imageData"];

            //کیوری داخڵکردنی ئەو دەیتایەی لە تیەری سێوە بۆمان یەت و کردنە ناو کۆڵۆمی ئیمەیج دەیتا
            String qurey = @"INSERT INTO mytestdb.dbo.image (imageData) VALUES('" + imageData + "');";
           
            // درووستکردنی دەیتاتەیبڵ وەک بۆکسێک
            DataTable dataTable = new DataTable();

            // وەرگرتنەوەی کۆنیکشن سترینگەکەی لە فایلی ئاپ سێتینگ کە هەموو ڕێکخستنەکانی تیایە
            String sqldataSourse = _configuration.GetConnectionString("AspSqlServerCon");

            // درووستکردنی خوێنەرەوەی کیوریەکە
            SqlDataReader myReader;


            // لێرە کۆنێکشنەکە درووست دەکەین و کماندەکە ڕەن ئەکەین و ئیکسیکیوتی ئەکەین
            using (SqlConnection mycon = new SqlConnection(sqldataSourse)) {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();

                    // ئەنجامەکەی ئەخەینە ناو ئەم بۆکسەوە کە وەک تەیبڵێک وایە
                    dataTable.Load(myReader);

                    // داخستنەوەی ڕیدەر و کۆنێکشنەکە بۆی سێرڤەرەکەمان قورس نەکا
                    myReader.Close();
                    mycon.Close();
                }
            }
            //Console.WriteLine("slaw base64 image data: " + imageData);
            Console.WriteLine("bashy kaka gyan?");

            // گەڕانەوەی دەیتاکان بۆ تیەری سێ لێرەوە دەبێت لەسەر شێوەی جەیسن
            return new JsonResult("slawss: " + imageData);
        }

    }
}
