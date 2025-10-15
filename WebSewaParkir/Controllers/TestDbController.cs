using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSewaParkir.Controllers
{
    public class TestDbController : Controller
    {
        // GET: TestDb
        public ActionResult Index()
        {
            // Ambil koneksi dari Web.config
            string connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["MyDbConnection"].ConnectionString;

            string message = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    message = "✅ Koneksi ke database SewaParkir BERHASIL!";
                }
                catch (Exception ex)
                {
                    message = "❌ Gagal koneksi ke database: " + ex.Message;
                }
            }

            return Content(message);
        }
    }
}