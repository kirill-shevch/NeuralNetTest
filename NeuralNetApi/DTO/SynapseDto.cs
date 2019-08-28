namespace NeuralNetApi.DTO
{
    public class SynapseDto
    {
        /// <summary>
        /// Id  синапса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id входного нейрона
        /// </summary>
        public int NeuronIdInput { get; set; }

        /// <summary>
        /// Id выходного нейрона
        /// </summary>
        public int NeuronIdOutput { get; set; }

        /// <summary>
        /// Id нейросети
        /// </summary>
        public int NeuralWebId { get; set; }

        /// <summary>
        /// Вес синапса
        /// </summary>
        public double Weight { get; set; }
    }
}
