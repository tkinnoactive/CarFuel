using System;
using System.ComponentModel.DataAnnotations;

namespace CarFuel.Models
{
	public class FillUp
	{
		public FillUp()
		{
		}

		public FillUp(int odometer, double liters, bool isFull = true)
		{
			Odometer = odometer;
			Liters = liters;
			IsFull = isFull;
		}

		[Key]
		public int Id { get; set; }

		public bool IsFull { get; set; }

		[Range(0.0, 100.0)]
		public double Liters { get; set; }

		//Navigation Properties
		// makes it "virtual" to enable lazy-loading
		public virtual FillUp NextFileUp { get; set; }

		[Range(0, 999999)]
		public int Odometer { get; set; }

		public double? KmL
		{
			get
			{
				if (NextFileUp == null)
					return null;

				if (NextFileUp.Odometer < Odometer)
					throw new Exception("Invalid next fill up odometer.");

				var totalKm = NextFileUp.Odometer - Odometer;
				return totalKm / NextFileUp.Liters;
			}
		}
	}
}