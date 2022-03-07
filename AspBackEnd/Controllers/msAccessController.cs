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
using System.Data.OleDb;




// ئەمە ئەی پی ئات ئەکسسەکەیە

namespace AspBackEnd.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class msAccessController : ControllerBase {


        private readonly IConfiguration _configuration;

        public msAccessController(IConfiguration configuration) {
            _configuration = configuration;
        }


        // server=127.0.0.1;port=3306;user id=root;database=mytestdb;password=@Boss1122
        string dbPath = @"C:\Users\Black Rule\Desktop\govan.accdb";

        [HttpGet]
        public JsonResult Get() {
            // Api/msAccess وەرگری مێثۆدی گێت لەم لینکەوە
            // ئەم ئیشانەی خوارەوەمان بۆ دەکات

            //ئەم کیوەیە کۆتا فایلمان بۆ ئەگەڕێنێتەوە لە تیەری یەکەوە بۆ تیەری دوو
            String qurey = @" SELECT TOP 1 * FROM assetData ORDER BY[imageId] DESC";

            // درووستکردنی دەیتاتەیبڵ وەک بۆکسێک
            DataTable dataTable = new DataTable();

            // ئەمە سترینگ کۆنێکشنەکەیە کە پێکهاتووە لە کماندێک و شوینی فایلی ئاکسسەکە
            String sqldataSourse = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbPath;

            // درووستکردنی خوێنەرەوەی کیوریەکە
            OleDbDataReader myReader;

            // لێرە کۆنێکشنەکە درووست دەکەین و کماندەکە ڕەن ئەکەین و ئیکسیکیوتی ئەکەین
            using (OleDbConnection mycon = new OleDbConnection(sqldataSourse)) {
                mycon.Open();
                using (OleDbCommand myCommand = new OleDbCommand(qurey, mycon)) {
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
            // Api/msAccess وەرگری مێثۆدی پۆست لەم لینکەوە
            // ئەم ئیشانەی خوارەوەمان بۆ دەکات

            // وەرگرتنەوەی ئەو دەیتایەی لە تیەری سێوە بۆمان یەت کە داتای فایلەکەیە
            var imageData = HttpContext.Request.Form["imageData"];

            //کیوری داخڵکردنی ئەو دەیتایەی لە تیەری سێوە بۆمان یەت و کردنە ناو کۆڵۆمی ئیمەیج دەیتا
            String qurey = @"INSERT INTO assetData (`imageData`) VALUES('" + imageData + "');";

            // درووستکردنی دەیتاتەیبڵ وەک بۆکسێک
            DataTable dataTable = new DataTable();

            // درووستکردنی خوێنەرەوەی کیوریەکە
            OleDbDataReader myReader;

            // ئەمە سترینگ کۆنێکشنەکەیە کە پێکهاتووە لە کماندێک و شوینی فایلی ئاکسسەکە
            String sqldataSourse = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbPath;

            // لێرە کۆنێکشنەکە درووست دەکەین و کماندەکە ڕەن ئەکەین و ئیکسیکیوتی ئەکەین
            using (OleDbConnection mycon = new OleDbConnection(sqldataSourse)) {
                mycon.Open();
                using (OleDbCommand myCommand = new OleDbCommand(qurey, mycon)) {
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
    }
}
