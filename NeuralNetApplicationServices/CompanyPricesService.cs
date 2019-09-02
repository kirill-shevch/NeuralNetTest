using AutoMapper;
using NeuralNetApi;
using NeuralNetApi.DTO.Prices;
using NeuralNetInfrastructure;
using NeuralNetInfrastructure.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeuralNetApplicationServices
{
    public class CompanyPricesService : ICompanyPricesService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        private string historicalPriceURL = $"https://financialmodelingprep.com/api/v3/historical-price-full";

        public void Update()
        {
            var companies = _applicationContext.Companies;

            foreach (var company in companies)
            {
                var lastPrice = _applicationContext.Prices
                    .Where(p => p.CompanyId == company.Id)
                    .OrderBy(p => p.Date)
                    .FirstOrDefault();

                if (lastPrice != null)
                {
                    var lastPriceUpdateDate = lastPrice.Date;
                    var lastPrices = GetPricesByCompanyFromDate(company, lastPriceUpdateDate);
                    _applicationContext.AddRange(lastPrices);
                }
                else
                {
                    var allPrices = GetPricesByCompany(company);
                    _applicationContext.AddRange(allPrices);
                }
            }
        }

        private List<Price> GetPricesByCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentException("Company can't be null.", nameof(company));
            }

            if (string.IsNullOrWhiteSpace(company.Signature))
            {
                throw new ArgumentException("Company's signature can't be null, empty or white space.", nameof(company.Signature));
            }

            var prices = new List<Price>();

            var url = historicalPriceURL + company.Signature;

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))

                {
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                    var httpResponse = Task.Run(() => httpClient.SendAsync(request)).Result;

                    if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"{url} is not responding.");
                    }

                    var responseBody = httpResponse.Content.ReadAsStringAsync().Result;

                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(responseBody);

                    foreach(var historical in rootObject.Historical)
                    {
                        var price = _mapper.Map<Price>(historical);
                        price.CompanyId = company.Id;
                        price.IsPredicted = false;

                        prices.Add(price);
                    }
                }
            }

            return prices;
        }

        private List<Price> GetPricesByCompanyFromDate(Company company, DateTime lastUpdated)
        {
            if (company == null)
            {
                throw new ArgumentException("Company can't be null.", nameof(company));
            }

            if (string.IsNullOrWhiteSpace(company.Signature))
            {
                throw new ArgumentException("Company's signature can't be null, empty or white space.", nameof(company.Signature));
            }

            var dateFormat = "yyyy-dd-MM";

            var currentDateString = DateTime.Now.ToString(dateFormat);

            var lastUpdatedDateString = lastUpdated.ToString(dateFormat);

            var prices = new List<Price>();

            var url = $"{historicalPriceURL}{company.Signature}?from={lastUpdatedDateString}&to={currentDateString}";

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))

                {
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                    var httpResponse = Task.Run(() => httpClient.SendAsync(request)).Result;

                    if (httpResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception($"{url} is not responding.");
                    }

                    var responseBody = httpResponse.Content.ReadAsStringAsync().Result;

                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(responseBody);

                    foreach (var historical in rootObject.Historical)
                    {
                        var price = _mapper.Map<Price>(historical);
                        price.CompanyId = company.Id;
                        price.IsPredicted = false;

                        prices.Add(price);
                    }
                }
            }

            return prices;
        }
    }
}
