using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            //String qurey = @"select * from mytestdb.dbo.image"; 
            String qurey = @"SELECT TOP 1 * FROM[mytestdb].[dbo].[image] ORDER BY[imageId] DESC"; 


           String _sqldataSourse = _configuration.GetConnectionString("AspSqlServerCon");

            DataTable dataTable = new DataTable();
            SqlDataReader myReader;
            
            using (SqlConnection mycon = new SqlConnection(_sqldataSourse)) {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post() {
            var imageData = HttpContext.Request.Form["imageData"];
            String qurey = @"INSERT INTO mytestdb.dbo.image (imageData) VALUES('" + imageData + "');";

            DataTable dataTable = new DataTable();
            String sqldataSourse = _configuration.GetConnectionString("AspSqlServerCon");
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqldataSourse)) {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(qurey, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            //Console.WriteLine("slaw base64 image data: " + imageData);
            Console.WriteLine("bashy kaka gyan?");


            return new JsonResult("slawss: " + imageData);
        }

    }
}
