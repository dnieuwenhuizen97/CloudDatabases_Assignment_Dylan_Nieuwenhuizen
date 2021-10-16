﻿using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerDb
    {
        private readonly DatabaseContext _dbContext;

        public CustomerDb(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FinancialInformation> UpdateFinancialInformation(string customerId, double salary)
        {
            Customer customer = await _dbContext.Customers
                                        .Include(c => c.FinancialInformation)
                                        .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            customer.FinancialInformation = new FinancialInformation(salary);
            _dbContext.SaveChanges();

            return customer.FinancialInformation;
        }

        public async Task<List<Customer>> FindAllCustomers()
        {
            List<Customer> customers = await _dbContext.Customers
                                                .Include(c => c.MortgageOffers)
                                                .Include(c => c.FinancialInformation)
                                                .Where(c => c.FinancialInformation != null)
                                                .ToListAsync();

            return customers;
        }

        public async Task UpdateCustomersMortgageOffers(List<Customer> customers)
        {
            List<Customer> oldCustomers = await FindAllCustomers();

            oldCustomers = customers;
            _dbContext.SaveChanges();
        }
    }
}
