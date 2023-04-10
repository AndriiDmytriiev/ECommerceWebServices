using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {

        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;


        public CustomerProvider(CustomersDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }


        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Luisa Bekker", Address = "4764 Gandy Street, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Alex Shapiro", Address = "31 Meadow View Drive, Florida(FL), 32563" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Sonja Liepae", Address = "1911 Little Street, New Jersey(NJ), 07732" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Dan Kropp", Address = "1910 Melville Street, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 5, Name = "Elsa Leent", Address = "2982 Hazelwood Avenue, Florida(FL), 32563" });
                dbContext.Customers.Add(new Db.Customer() { Id = 6, Name = "Olaf Drinn", Address = "1811 Davis Court, New Jersey(NJ), 07732" });
                dbContext.Customers.Add(new Db.Customer() { Id = 7, Name = "Kane Navus", Address = "3938 Linda Street, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 8, Name = "Itan Bailin", Address = "1050 Veltri Drive, Florida(FL), 32563" });
                dbContext.Customers.Add(new Db.Customer() { Id = 9, Name = "Irma Kenzo", Address = "2830 Clarksburg Park Road, New Jersey(NJ), 07732" });
                dbContext.Customers.Add(new Db.Customer() { Id = 10, Name = "Ram Korri", Address = "675 Meadow Drive, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 11, Name = "Alex Instrem", Address = "1702 Kyle Street, Florida(FL), 32563" });
                dbContext.Customers.Add(new Db.Customer() { Id = 12, Name = "Ply Denver", Address = "4498 Courtright Street, New Jersey(NJ), 07732" });
                dbContext.Customers.Add(new Db.Customer() { Id = 13, Name = "Olga Dabkunaite", Address = "2711 Eden Drive, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 14, Name = "Aaron Wistle", Address = "872 Wines Lane, Florida(FL), 32563" });
                dbContext.Customers.Add(new Db.Customer() { Id = 15, Name = "John Salven", Address = "4181 Luke Lane, New Jersey(NJ), 07732" });
                dbContext.Customers.Add(new Db.Customer() { Id = 16, Name = "Eric Node", Address = "1973 Mandan Road, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 17, Name = "Ana Kraufter", Address = "1674 Custer Street, Florida(FL), 32563" });
                dbContext.Customers.Add(new Db.Customer() { Id = 18, Name = "Enja Dolmer", Address = "1318 Lunetta Street, New Jersey(NJ), 07732" });
                dbContext.Customers.Add(new Db.Customer() { Id = 19, Name = "Anthony Kristoff", Address = "4569 Glendale Avenue, New York, 13676" });
                dbContext.Customers.Add(new Db.Customer() { Id = 20, Name = "Andrew Zeen", Address = "4847 Tator Patch Road, Florida(FL), 32563" });
              
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, Models.Customer Customer, string errorMessage)> GetCustomerAsync(int id)
        {

            try
            {
            
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if(customer != null) {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");

            }
            catch (System.Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                    var customers = await dbContext.Customers.ToListAsync();
                if(customers != null && customers.Any()) {
                  
                  var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }

                return (false, null, "Not customer found");
            }
            catch (System.Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}