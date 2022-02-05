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
            //String qurey = @"select * from mytestdb.image";
            String qurey = @"select * from mytestdb.image ORDER BY imageId DESC limit 1";
            //String qurey = @"select MAX(imageId) from mytestdb.image";

            DataTable dataTable = new DataTable();
            // "Data Source=Mysql@127.0.0.1:3306;Initial Catalog=mytestdb;User Id=root;password=HHaa1414@"
            String sqldataSourse = _configuration.GetConnectionString("AspMySqlCon");
            //String sqldataSourse = "server=127.0.0.1;port=3306;user id=root;database=mytestdb;password=HHaa1414@";
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqldataSourse)) {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }


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
            var imageData = HttpContext.Request.Form["imageData"];
            String qurey = @"INSERT INTO mytestdb.image (`imageData`) VALUES('" + imageData + "');";

            DataTable dataTable = new DataTable();
            String sqldataSourse = _configuration.GetConnectionString("AspMySqlCon");
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqldataSourse)) {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            Console.WriteLine("base64 image data: " + imageData);

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
