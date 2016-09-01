using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarFuel.DataAccess.Contexts;
using CarFuel.Models;
using CarFuel.Services;
using CarFuel.Services.Bases;
using CarFuel.Web.Models;
using Microsoft.AspNet.Identity;

namespace CarFuel.Web.Controllers
{
	public class CarsController : AppControllerBase
	{
		private readonly ICarService _carService;
		private readonly IUserService _userService;

		public CarsController(ICarService carService, IUserService userService)
			: base(userService)
		{
			_carService = carService;
			_userService = userService;
		}

		// GET: Car
		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var id = new Guid(User.Identity.GetUserId());
				var cars = _carService.AllCarsForUser(id);

				ViewBag.AppUser = _userService.CurrentUser;

				return View("IndexForMember", cars);
			}
			else
			{
				return View("IndexForAnonymous");
			}
		}

		// GET: Car/Details/5
		public ActionResult Details(Guid id)
		{
			var cars = from c in _carService.All()
					   where c.Id == id
					   select c;
			return View(cars.FirstOrDefault());
		}

		// GET: Car/Add
		public ActionResult Add()
		{
			var data = new Car();
			return View(data);
		}

		// POST: Car/Create
		[HttpPost]
		public ActionResult Add(Car item)
		{
			ModelState.Remove(nameof(item.Owner));

			if (ModelState.IsValid)
			{
				// TODO: assign car owner to current user.
				User u = _userService.Find(new Guid(User.Identity.GetUserId()));
				item.Owner = u;

				_carService.Add(item);
				_carService.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(item);
		}

		// GET: Car/Add
		public ActionResult AddFillUp(Guid id)
		{
			var q = from c in _carService.All()
					where c.Id == id
					select c.Name;

			var name = q.SingleOrDefault();
			ViewBag.CarName = name;

			return View();
		}

		// POST: Car/Create
		[HttpPost]
		public ActionResult AddFillUp(Guid id, [Bind(Exclude = "Id")] FillUp data)
		{
			ModelState.Remove("Id");

			if (ModelState.IsValid)
			{
				var c = _carService.Find(id);
				c.AddFillUp(data.Odometer, data.Liters);
				_carService.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(data);
		}

		// GET: Car/Edit/5
		public ActionResult Edit(Guid id)
		{
			var ett = _carService.Find(id);
			if (ett == null)
				return HttpNotFound();
			return View(ett);
		}

		// POST: Car/Edit/5
		[HttpPost]
		public ActionResult Edit(Guid id, Car data)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: Add update logic here
					var ett = _carService.Find(id);
					if (ett == null)
						return HttpNotFound();

					ett.Name = data.Name;
					_carService.SaveChanges();

					return RedirectToAction("Index");
				}
				else
				{
					return View(data);
				}
			}
			catch
			{
				return View(data);
			}
		}

		// GET: Car/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Car/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
