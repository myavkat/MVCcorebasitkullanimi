using Deneme1.Entities;
using Deneme1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace Deneme1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        DBContext dBContext = new DBContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        List<Ogrenci> ogrencis = new List<Ogrenci>();
        public IActionResult hesapla(string ad, string soyad)
        {
            Ogrenci o1 = new Ogrenci();
            o1.Ad = ad;
            o1.Soyad = soyad;

            dBContext.Ogrenci.Add(o1);
            dBContext.SaveChanges();
            return Anasayfa();
        }

        public IActionResult hesapla2(string ad, string soyad)
        {
            getQueryToDataTable($"insert into Ogrenci(ad,soyad) values('{ad}*','{soyad}')",dBContext);
            return Anasayfa();
        }
        public IActionResult sil(int Id)
        {
            var osil = dBContext.Ogrenci.Where(x => x.Id == Id).FirstOrDefault();
            if (osil != null)
            {
                dBContext.Remove(osil);
                dBContext.SaveChanges();
            }
            return Anasayfa();
        }
        public IActionResult Anasayfa()
        {
            DataTable data = getQueryToDataTable("select * from Ogrenci", dBContext);
            string json = JsonConvert.SerializeObject(data);
            List<Ogrenci> ogrenciler = JsonConvert.DeserializeObject<List<Ogrenci>>(json);

            ViewBag.ogrencis = ogrenciler;
            return View("Anasayfa");
        }

        public IActionResult Anasayfa2()
        {
            ViewBag.ogrencis = dBContext.Ogrenci.ToList();
            return View("Anasayfa");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ApiExplorerSettings(IgnoreApi = true)]

        [NonAction]

        public DataTable getQueryToDataTable(string query, DbContext context)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
            }

            return dt;

        }


    }
}