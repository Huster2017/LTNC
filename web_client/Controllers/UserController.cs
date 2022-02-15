using DocumentFormat.OpenXml.Bibliography;
using Ga.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ga.Controllers
{
	public class UserController : Controller
	{
		Context context = new Context();
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public ActionResult createUser(user id)
		{
			context.Users.Add(id);
			context.SaveChanges();
			string message = "SUCCESS";
			return Json(message);
		}
		//[HttpGet]
		//public IActionResult GetData(string id)
		//{
		//	var users = Station.data.DBdata.MainDB.GetCollection("user").ToList<user>();
		//	return View(users);

		//}
		public IActionResult Save(user users)
		{
			if (Ga.data.DBdata.MainDB.GetCollection("user").FindById<user>(users.Id) == null)
			{
				users.userId = users.Id;
				Ga.data.DBdata.MainDB.GetCollection("user").Insert(users.Id, users);
			}
			else
			{
				Ga.data.DBdata.MainDB.GetCollection("user").Update(users);
			}
			return RedirectToAction("Index", "Home");
		}
		

	}
}
