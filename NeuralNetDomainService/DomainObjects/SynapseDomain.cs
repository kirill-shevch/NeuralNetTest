namespace NeuralNetDomainService.DomainObjects
{
    public class SynapseDomain
    {
        /// <summary>
        /// Id  синапса
        /// </summary>
        public int IdSynapse { get; set; }

        /// <summary>
        /// Id входного нейрона
        /// </summary>
        public int IdInput { get; set; }

        /// <summary>
        /// Id выходного нейрона
        /// </summary>
        public int IdOutput { get; set; }

        /// <summary>
        /// Вес синапса
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Изменение веса нейрона
        /// </summary>
        public double DeltaWeight { get; set; }

        /// <summary>
        /// Id нейросети
        /// </summary>
        public int NeuralNetId { get; set; }

        public override bool Equals(object obj)
        {
            var synapse = obj as SynapseDomain;
            return synapse != null &&
                   IdSynapse == synapse.IdSynapse &&
                   IdInput == synapse.IdInput &&
                   IdOutput == synapse.IdOutput &&
                   Weight == synapse.Weight &&
                   DeltaWeight == synapse.DeltaWeight;
        }

        public override int GetHashCode()
        {
            var hashCode = -393186522;
            hashCode = hashCode * -1521134295 + IdSynapse.GetHashCode();
            hashCode = hashCode * -1521134295 + IdInput.GetHashCode();
            hashCode = hashCode * -1521134295 + IdOutput.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + DeltaWeight.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Weight.ToString();
        }

        public SynapseDomain()
        {

        }
    }
}


