namespace CarFuel.DataAccess.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Models;
	internal sealed class Configuration : DbMigrationsConfiguration<CarFuel.DataAccess.Contexts.CarFuelDb>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			ContextKey = "CarFuel.DataAccess.Contexts.CarFuelDb";
		}

		protected override void Seed(Contexts.CarFuelDb context)
		{
			var zeroId = new Guid();
			context.Users.AddOrUpdate(
				u => u.UserId,
				new User { UserId = zeroId, DisplayName = "Default user" }
				);
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}
	}
}
