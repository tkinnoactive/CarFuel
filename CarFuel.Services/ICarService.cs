﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.Models;
using CarFuel.Services.Bases;

namespace CarFuel.Services
{
	public interface ICarService : IService<Car>
	{
		//IQueryable<Car> AllCarsForUser(Guid memberId);
	}
}
