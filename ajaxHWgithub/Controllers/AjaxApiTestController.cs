using ajaxHWgithub.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Text;
using System.Text.Unicode;

namespace _01ajaxTest.Controllers
{
	public class AjaxApiTestController : Controller
	{

		private readonly DemoContext _context;

		private readonly IWebHostEnvironment _host;

		public AjaxApiTestController(DemoContext context, IWebHostEnvironment host)
		{
			_context = context;

			_host = host;
		}
		public IActionResult Index(string name)
		{

			//System.Threading.Thread.Sleep(1000);

			if (string.IsNullOrEmpty(name))
			{
				name = "nothing";
			}

			return Content($"<h1>Hello ,{name}</h1>", "text/html", Encoding.UTF8);
		}


		//	public IActionResult Register(Member member, IFormFile photo)
		//	{
		//		//return Content($"Hello, {member.Name}, You are {member.Age} years old, {member.Email}。", "text/plain", Encoding.UTF8);

		//		string filePath = Path.Combine(_host.WebRootPath, "uploads", photo.FileName);
		//		using (var fileStream = new FileStream(filePath, FileMode.Create))
		//		{
		//			photo.CopyTo(fileStream);
		//			//return Content($"{filePath}");
		//		}


		//		member.FileName = photo.FileName;

		//		byte[] imgByte = new byte[1];
		//		using (var memoryStream = new MemoryStream()) { 
		//				photo.CopyTo(memoryStream);
		//				imgByte = memoryStream.ToArray();
		//		}
		//		if(imgByte.Length >= 2)
		//		{

		//			member.FileData = imgByte;
		//		}


		//		_context.Members.Add(member);
		//		_context.SaveChanges();
		//return Content($"{filePath}");
		//	}

		//讀取城市名稱
		public IActionResult City()
		{
			var cities = _context.Addresses.Select(c => c.City).Distinct();
			//var cities = _context.Addresses.Select(c => new
			//{
			//    c.City
			//}).Distinct().OrderBy(c => c.City);
			return Json(cities);
		}
		//根據城市名稱讀取鄉鎮區
		public IActionResult Site(string city)
		{
			var sites = _context.Addresses.Where(s => s.City == city).Select(s => s.SiteId).Distinct();

			return Json(sites);
		}
		//根據鄉鎮區讀取路名
		public IActionResult Road(string siteRoad)
		{
			var roads = _context.Addresses.Where(s => s.SiteId == siteRoad).Select(s => s.Road).Distinct();

			return Json(roads);
		}
		public IActionResult city2()
		{

			return View();
		}


		public IActionResult findAccount(string targetAccountString) {
			return Content(_context.Members.FirstOrDefault(_ => _.Name == targetAccountString) != null ? "true" : "false" );

        }

	}
}
