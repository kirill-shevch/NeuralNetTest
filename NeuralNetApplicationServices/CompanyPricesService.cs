using AutoMapper;
using Microsoft.Extensions.Configuration;
using NeuralNetApi;
using NeuralNetApi.DTO.Prices;
using NeuralNetInfrastructure;
using NeuralNetInfrastructure.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeuralNetApplicationServices
{
    public class CompanyPricesService : ICompanyPricesService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly string _historicalPriceURL;

        public CompanyPricesService(IRepository repository, IConfiguration configuraton)
        {
            _repository = repository;
            _historicalPriceURL = configuraton["historicalPriceUrl"];
        }

        public async Task Update()
        {
            //var companies = await _repository.GetCompanies();

            //foreach (var company in companies)
            //{
            //    var lastPrice = await _repository.GetLastPrice(company.Id);

            //    if (lastPrice != null)
            //    {
            //        var lastPriceUpdateDate = lastPrice.Date;
            //        var lastPrices = await GetPricesByCompanyFromDate(company, lastPriceUpdateDate);
            //        await _repository.AddRangeOfPrices(lastPrices);
            //    }
            //    else
            //    {
            //        var allPrices = await GetPricesByCompany(company);
            //        await _repository.AddRangeOfPrices(allPrices);
            //    }
            //}
        }

        private async Task<List<Price>> GetPricesByCompany(Company company)
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

            var url = _historicalPriceURL + company.Signature;

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))

                {
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                    var httpResponse = await httpClient.SendAsync(request);

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

        private async Task<List<Price>> GetPricesByCompanyFromDate(Company company, DateTime lastUpdated)
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

            var url = $"{_historicalPriceURL}{company.Signature}?from={lastUpdatedDateString}&to={currentDateString}";

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))

                {
                    request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");

                    var httpResponse = await httpClient.SendAsync(request);

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
