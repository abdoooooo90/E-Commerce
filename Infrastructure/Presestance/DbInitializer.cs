using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
	public class DbInitializer : IDbInitializer
	{
		private readonly StoreContext _storeContext;

		public DbInitializer(StoreContext storeContext)
        {
			_storeContext = storeContext;
		}
		
		public async Task InitializerAsync()
		{
			try
			{
				//Create DataBase If It Dosn't Exist & Applying Any Pending Migrations
				if (_storeContext.Database.GetPendingMigrations().Any())
					await _storeContext.Database.MigrateAsync();
				//Appling Data Seeding
				#region ProductType
				if (!_storeContext.ProductTypes.Any())
				{
					//Read Types From File As String
					var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\types.json");
					//Transform Into C# Objects
					var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
					//Add To DB & Save Changes
					if (types is not null && types.Any())
					{
						await _storeContext.ProductTypes.AddRangeAsync(types);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
				#region ProductBrand
				if (!_storeContext.ProductBrands.Any())
				{
					//Read Types From File As String
					var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\brands.json");
					//Transform Into C# Objects
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
					//Add To DB & Save Changes
					if (brands is not null && brands.Any())
					{
						await _storeContext.ProductBrands.AddRangeAsync(brands);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
				#region Product
				if (!_storeContext.Products.Any())
				{
					//Read Types From File As String
					var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\products.json");
					//Transform Into C# Objects
					var products = JsonSerializer.Deserialize<List<Product>>(productData);
					//Add To DB & Save Changes
					if (products is not null && products.Any())
					{
						await _storeContext.Products.AddRangeAsync(products);
						await _storeContext.SaveChangesAsync();
					}

				}
				#endregion
			}catch (Exception)
			{
				throw;
			}
		}

	}
}
