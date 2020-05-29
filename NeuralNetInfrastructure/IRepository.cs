using NeuralNetInfrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetInfrastructure
{
    public interface IRepository
    {
        Task<Price> GetLastPrice(int companyId);

        Task AddRangeOfPrices(List<Price> prices);

        Task<List<Company>> GetCompanies();

        Task<NeuralNet> GetNeuralNet(int neuralNetId);

        Task UpdateNeuralNet(NeuralNet neuralNet);

        Task AddNeuralNet(NeuralNet neuralNet);

        Task RemoveNeuralNet(NeuralNet neuralNet);
    }
}
