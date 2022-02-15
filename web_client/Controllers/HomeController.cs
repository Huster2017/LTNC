using Ga.Models;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System;

namespace Ga.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public JsonResult getuser()
		{
			var x = Ga.data.DBdata.MainDB.GetCollection("user").FindById<user>("1");
			return Json(JsonConvert.SerializeObject(x));
		}
		public IActionResult Index()
		{
			var users = Ga.data.DBdata.MainDB.GetCollection("user").ToList<user>();
			return View(users);
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[HttpPost]
		public JsonResult SetData(string ketqua)
		{
			var ID = ketqua.Substring(0, 3); //get ID (form ID: Mxx vd: M01)
			var add = ketqua.Substring(3); // get address (form add: XX vd: 1A is that the data sent from client1A)
			var check = 0;
			var obj = Ga.data.DBdata.MainDB.GetCollection("user").FindById<user>(ID);
			//Check $ trong tai khoan
			if (obj.Id != null)
			{
				if (obj.Money < 10000)
				{
					check = 0;
				}
				else
				{
					if (obj.Start == "0")
					{
						obj.Start = add;
						Ga.data.DBdata.MainDB.GetCollection("user").Update(obj);
					}
					else
					{
						obj.End = add;
						int money;
						int add1 = int.Parse(obj.Start.Substring(0,1));
						int add2 = int.Parse(obj.End.Substring(0,1));
						if(obj.Age <= 10) { money = obj.Money - (add2 - add1) * 10000; }
						else { money = obj.Money - (add2 - add1) * 15000; }
						obj.Money = money;
						obj.Start = "0";
						obj.End = "0";
						Ga.data.DBdata.MainDB.GetCollection("user").Update(obj);
					}
					check = 1;
				}
			}
			else
			{
				check = 0;
			}
			//

			return Json(JsonConvert.SerializeObject(check + add));
		}
		// trong hàm này sẽ cập nhật vị trí và trừ tiền
		//private static void Update_data(string id, string add)
		//{
		//	var data = Ga.data.DBdata.MainDB.GetCollection("user").FindById<user>(id);
		//	if(data.Start == "0")
		//	{
		//		data.Start = add;
		//		Ga.data.DBdata.MainDB.GetCollection("user").Update(data);
		//	}
		//	else
		//	{
		//		data.End = add;
		//		Ga.data.DBdata.MainDB.GetCollection("user").Update(data);
		//	}
			
		//}	

	}
}