using System.Collections.Generic;

namespace NeuralNetApi.DTO.Prices
{
    public class RootObject
    {
        public string Symbol { get; set; }
        public List<Historical> Historical { get; set; }
    }
}