using Memorizer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memorizer.Controllers
{
    public class HomeController : Controller
    {
        List<Item> items = new List<Item>();
        public ActionResult Index()
        {
            GetItem("");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Memorizer()
        {
            ViewBag.Message = "Memorise!!!";

            return View();
        }

        public JsonResult GetItem(string id)
        {
            ReadAllItems("~/Resources/words.txt");
            if (String.IsNullOrEmpty(id))
            {
                Random rnd = new Random();
                return Json(items.ElementAt(rnd.Next(0, items.Count)), JsonRequestBehavior.AllowGet);
            }
            return Json(items.First(x => x.Key.Equals(id)), JsonRequestBehavior.AllowGet);
        }

        public void ReadAllItems(string path)
        {
            string line = "";
            var items = new List<Item>();

            var filestream = new System.IO.FileStream(Server.MapPath(path),
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            while ((line = file.ReadLine()) != null)
            {
                string[] lineArray = line.Split(';');
                Item item = new Item();
                item.Key = lineArray[0];
                item.Value = lineArray[1];
                items.Add(item);
            }

            this.items = new List<Item>(items);
        }

        public JsonResult GetAllItems()
        {
            ReadAllItems("~/Resources/words.txt");
            return Json(JsonConvert.SerializeObject(items.Shuffle()), JsonRequestBehavior.AllowGet);
        }
    }

    public static class Extensions
    {
        public static List<T> Shuffle<T>(
            this List<T> source)
        {
            Random r = new Random();
            return source.OrderBy(x => r.Next())
               .ToList();
        }
    }
}