﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarFuel.Services;
using Microsoft.AspNet.Identity;

namespace CarFuel.Web.Controllers
{
	public abstract class AppControllerBase : Controller
	{

		protected readonly IUserService _userService;

		public AppControllerBase(IUserService userService)
		{
			_userService = userService;

		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				var userId = new Guid(User.Identity.GetUserId());
				_userService.CurrentUser = _userService.Find(userId);
			}
		}
	}
}