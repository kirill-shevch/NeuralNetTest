using Microsoft.Extensions.Hosting;
using NeuralNetApi;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebService.HostedServices
{
    public class CompanyPricesHostedService : IHostedService
    {
        private Timer _timer;

        private readonly ICompanyPricesService _companyPricesService;

        public CompanyPricesHostedService(ICompanyPricesService companyPricesService)
        {
            _companyPricesService = companyPricesService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private void DoWork(object state)
        {
            _companyPricesService.Update();
        }
    }
}
