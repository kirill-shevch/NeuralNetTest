namespace NeuralNetTest
{
    public abstract class AbstractInstance
    {
        public NeuralNetwork NeuralNetwork { get; set; }

        public abstract void CreateInstance();

        public void SaveInstance(string directory)
        {
            NeuralNetwork.SaveToFile(directory);
        }

        public void LoadInstance(string directory)
        {
            NeuralNetwork = new NeuralNetwork(directory);
        }
    }
}
