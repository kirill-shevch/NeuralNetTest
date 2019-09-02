using Microsoft.Extensions.Hosting;

namespace NeuralNetApi.Private
{
    public interface ICompanyPricesHostedService : IHostedService
    {
        void Update();
    }
}
