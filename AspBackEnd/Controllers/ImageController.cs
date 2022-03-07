using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using System.IO;


// ئەمە ئەی پی ئای مای ئێسکیو ئێڵەکەیە

namespace AspBackEnd.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase {

        private readonly IConfiguration _configuration;

        public ImageController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() {

            // Api/Image وەرگری مێثۆدی گێت لەم لینکەوە
            // ئەم ئیشانەی خوارەوەمان بۆ دەکات

            //ئەم کیوەیە کۆتا فایلمان بۆ ئەگەڕێنێتەوە لە تیەری یەکەوە بۆ تیەری دوو
            String qurey = @"select * from mytestdb.image ORDER BY imageId DESC limit 1";

            // وەرگرتنەوەی کۆنیکشن سترینگەکەی لە فایلی ئاپ سێتینگ کە هەموو ڕێکخستنەکانی تیایە
            String sqldataSourse = _configuration.GetConnectionString("AspMySqlCon");

            // درووستکردنی دەیتاتەیبڵ وەک بۆکسێک
            DataTable dataTable = new DataTable();
            
            // درووستکردنی خوێنەرەوەی کیوریەکە
            MySqlDataReader myReader;

            // لێرە کۆنێکشنەکە درووست دەکەین و کماندەکە ڕەن ئەکەین و ئیکسیکیوتی ئەکەین
            using (MySqlConnection mycon = new MySqlConnection(sqldataSourse)) {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(qurey, mycon)) {
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

           // return new JsonResult(
            //    new {
             //       timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
             //       message = "slaw gyana",
              //      key = 12,
              //      newJson = new { 
             //           data = "slaw",
              //      }
             //   }
           // );

            // string valueReturn = "{''Operations'': ''failed''}";
            //return new JsonResult(valueReturn);
        }


        [HttpPost]
        public JsonResult Post() {
            // Api/Image وەرگری مێثۆدی پۆست لەم لینکەوە
            // ئەم ئیشانەی خوارەوەمان بۆ دەکات

            // وەرگرتنەوەی ئەو دەیتایەی لە تیەری سێوە بۆمان یەت کە داتای فایلەکەیە
            var imageData = HttpContext.Request.Form["imageData"];
            
            //کیوری داخڵکردنی ئەو دەیتایەی لە تیەری سێوە بۆمان یەت و کردنە ناو کۆڵۆمی ئیمەیج دەیتا
            String qurey = @"INSERT INTO mytestdb.image (`imageData`) VALUES('" + imageData + "');";
            
            // درووستکردنی دەیتاتەیبڵ وەک بۆکسێک
            DataTable dataTable = new DataTable();

            // وەرگرتنەوەی کۆنیکشن سترینگەکەی لە فایلی ئاپ سێتینگ کە هەموو ڕێکخستنەکانی تیایە
            String sqldataSourse = _configuration.GetConnectionString("AspMySqlCon");

            // درووستکردنی خوێنەرەوەی کیوریەکە
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqldataSourse)) {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    // ئەنجامەکەی ئەخەینە ناو ئەم بۆکسەوە کە وەک تەیبڵێک وایە
                    dataTable.Load(myReader);

                    // داخستنەوەی ڕیدەر و کۆنێکشنەکە بۆی سێرڤەرەکەمان قورس نەکا
                    myReader.Close();
                    mycon.Close();
                }
            }
            Console.WriteLine("base64 image data: " + imageData);

            // گەڕانەوەی دەیتاکان بۆ تیەری سێ لێرەوە دەبێت لەسەر شێوەی جەیسن
            return new JsonResult("slaw: " + imageData);
        }

        //[NonAction]
        //public async Task<String> saveImage(IFormFile imageFile) {

        //    String imageName = new string(
        //        Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()
        //        ).Replace(' ', '-');

        //    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

        //}
    }
}
