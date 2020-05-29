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
                    .FirstOrDefaultAsync(p => p.CompanyId == companyId).ConfigureAwait(false);
        }

        public async Task AddRangeOfPrices(List<Price> prices)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                await dbContext.AddRangeAsync(prices).ConfigureAwait(false);

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<List<Company>> GetCompanies()
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
                return await dbContext.Companies.ToListAsync().ConfigureAwait(false);
        }

        public async Task<NeuralNet> GetNeuralNet(int neuralNetId)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
                return await dbContext.NeuralNets
                    .Include(n => n.Neurons)
                    .Include(n => n.Synapses)
                    .SingleOrDefaultAsync(n => n.Id == neuralNetId)
                    .ConfigureAwait(false);
        }

        public async Task UpdateNeuralNet(NeuralNet neuralNet)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                var neuralNetEntity = await dbContext.FindAsync<NeuralNet>(neuralNet.Id).ConfigureAwait(false);
                neuralNetEntity.ErrorMSE = neuralNet.ErrorMSE;
                neuralNetEntity.MSEcounter = neuralNet.MSEcounter;
                dbContext.Entry(neuralNetEntity).State = EntityState.Modified;
                foreach (var synapse in neuralNet.Synapses)
                {
                    var synapseEntity = await dbContext.FindAsync<Synapse>(synapse.Id).ConfigureAwait(false);
                    synapseEntity.Weight = synapse.Weight;
                    dbContext.Entry(synapseEntity).State = EntityState.Modified;
                }

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task AddNeuralNet(NeuralNet neuralNet)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                await dbContext.NeuralNets.AddAsync(neuralNet).ConfigureAwait(false);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task RemoveNeuralNet(NeuralNet neuralNet)
        {
            using (var dbContext = new ApplicationContext(_configuration["connectionString"]))
            {
                dbContext.NeuralNets.Remove(neuralNet);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
