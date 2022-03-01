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





namespace AspBackEnd.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class msAccessController : ControllerBase {


        private readonly IConfiguration _configuration;

        public msAccessController(IConfiguration configuration) {
            _configuration = configuration;
        }


        
        string dbPath = @"C:\Users\Black Rule\Desktop\govan.accdb";

        [HttpGet]
        public JsonResult Get() {
            //String qurey = @"select * from mytestdb.image";
            //assetData
            String qurey = @" SELECT TOP 1 * FROM assetData ORDER BY[imageId] DESC";
           

            DataTable dataTable = new DataTable();

            String sqldataSourse = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbPath;
            //String sqldataSourse = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Black Rule\Desktop\govan.accdb";

            OleDbDataReader myReader;

            using (OleDbConnection mycon = new OleDbConnection(sqldataSourse)) {
                mycon.Open();
                using (OleDbCommand myCommand = new OleDbCommand(qurey, mycon)) {
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
            String qurey = @"INSERT INTO assetData (`imageData`) VALUES('" + imageData + "');";


            DataTable dataTable = new DataTable();
            
            OleDbDataReader myReader;
            String sqldataSourse = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + dbPath;
            using (OleDbConnection mycon = new OleDbConnection(sqldataSourse)) {
                mycon.Open();
                using (OleDbCommand myCommand = new OleDbCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            Console.WriteLine("base64 image data: " + imageData);

            return new JsonResult("slaw: " + imageData);
        }
    }
}
