using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NeuralNetInfrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetInfrastructure
{
    public class Repository : IRepository
    {
        private readonly IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Price> GetLastPrice(int companyId)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
                return await dbContext.Prices
                    .OrderBy(p => p.Date)
                    .FirstOrDefaultAsync(p => p.CompanyId == companyId);
        }

        public async Task AddRangeOfPrices(List<Price> prices)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
                await dbContext.AddRangeAsync(prices);
        }

        public async Task<List<Company>> GetCompanies()
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
                return await dbContext.Companies.ToListAsync();
        }

        public async Task<NeuralNet> GetNeuralNet(int neuralNetId)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
                return await dbContext.NeuralNets.SingleAsync(n => n.Id == neuralNetId);
        }

        public async Task UpdateNeuralNet(NeuralNet neuralNet)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                dbContext.NeuralNets.Update(neuralNet);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddNeuralNet(NeuralNet neuralNet)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                await dbContext.NeuralNets.AddAsync(neuralNet);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveNeuralNet(NeuralNet neuralNet)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                dbContext.NeuralNets.Remove(neuralNet);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
