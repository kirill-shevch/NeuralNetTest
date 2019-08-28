namespace NeuralNetApi
{
    public class SynapseCreatingRequest
    {
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
        public int NeuralNetId { get; set; }

        /// <summary>
        /// Вес синапса
        /// </summary>
        public double Weight { get; set; }
    }
}
