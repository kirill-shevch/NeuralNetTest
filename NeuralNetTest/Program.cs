using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Скорость обучения
            const double E = 0.7;

            //Момент
            const double M = 0.3;
            List<Neuron> Neurons = new List<Neuron>();

            //Получаем сет нейронов и синапсов из файла
            #region read from stream set of neurons
                        
            bool neurFlag = true; //true = считываем нейроны, false = считываем синапсы
            using (StreamReader sr = new StreamReader(@"D:\Workspace\DotNet\NeuralNetTest\NeuralNetTest\data.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Equals("neurons"))
                    {
                        neurFlag = true;
                        line = sr.ReadLine();
                    }
                    if (line.Equals("synapses"))
                    {
                        neurFlag = false;
                        line = sr.ReadLine();
                    }
                    var parts = line.Split(';');
                    if (parts.Length > 1)
                    {
                        if (neurFlag)
                        {
                            var n = new Neuron();
                            n.id = Convert.ToInt32(parts[0]);
                            n.data_in = Convert.ToDouble(parts[2]);
                            n.data_out = Convert.ToDouble(parts[3]);
                            switch (parts[1])
                            {
                                case "input":
                                    {
                                        n.Input = true;
                                        break;
                                    }
                                case "hidden":
                                    {
                                        n.Hidden = true;
                                        break;
                                    }
                                case "output":
                                    {
                                        n.Output = true;
                                        break;
                                    }
                                default:
                                    break;
                            }
                            Neurons.Add(n);
                        }
                        else
                        {
                            var s = new Synapse();
                            s.id_input = Convert.ToInt32(parts[0]);
                            s.id_output = Convert.ToInt32(parts[1]);
                            s.weight = Convert.ToDouble(parts[2]);
                            Neurons[s.id_input].output_Synapse.Add(s);
                            Neurons[s.id_output].input_Synapse.Add(s);
                        }
                    }
                }
            }

            #endregion        
            
            //Правильный ответ
            double answer = (byte)Neurons[0].data_out ^ (byte)Neurons[1].data_out;

            print(Neurons);
            
            #region Forward Pass
            //Передача вперёд
            
            //Обсчитываем скрытый слой
            var hidden_neur = Neurons.Where(n => n.Hidden).ToList();
            for (int i = 0; i < hidden_neur.Count; i++)
            {
                foreach (var item in hidden_neur[i].input_Synapse)
                {
                    Neurons[hidden_neur[i].id].data_in += Neurons[item.id_input].data_out * item.weight;
                    Neurons[hidden_neur[i].id].data_out = sigmoid(Neurons[hidden_neur[i].id].data_in);
                }
            }
            
            //Обсчитываем слой вывода

            var out_neur = Neurons.Where(n => n.Output).ToList();
            for (int i = 0; i < out_neur.Count; i++)
            {
                foreach (var item in out_neur[i].input_Synapse)
                {
                    Neurons[out_neur[i].id].data_in += Neurons[item.id_input].data_out * item.weight;
                    Neurons[out_neur[i].id].data_out = sigmoid(Neurons[out_neur[i].id].data_in);
                }
            }

            print(Neurons);
            
            //Считаем значение средней квадратичной ошибки
            double ErrorMSE = 0;
            double MSEcounter = 0;
            for (int i = 0; i < out_neur.Count; i++)
            {
                MSEcounter++;
                ErrorMSE += Math.Pow((answer - out_neur[i].data_out), 2); 
            }
            
            ErrorMSE = ErrorMSE / MSEcounter;
            
            Console.WriteLine("Answer is {0}", answer);
            Console.WriteLine("Mean Squared Error: {0}", ErrorMSE);
            Console.ReadLine();

            #endregion
            
            #region Delta deviation
        
            out_neur = Neurons.Where(n => n.Output).ToList();

            //Считаем дельта-отклонение для слоя вывода
            for (int i = 0; i < out_neur.Count; i++)
            {
                Neurons[out_neur[i].id].delta_deviation = delta_output(answer, out_neur[i].data_out, out_neur[i].data_out);
            }
                      
            //Считаем дельта-отклонение для скрытого слоя и изменение веса синапсов
            hidden_neur = Neurons.Where(n => n.Hidden).ToList();
            for (int i = 0; i < hidden_neur.Count; i++)
            {
                //сумма произведения всех исходящих весов и дельта-отклонения нейрона, с которым связан синапс
                double sum = 0;

                //градиент для градиентного спуска
                double grad = 0;

                for (int j = 0; j < hidden_neur[i].output_Synapse.Count; j++)
                {
                    sum += Neurons[hidden_neur[i].output_Synapse[j].id_output].delta_deviation * hidden_neur[i].output_Synapse[j].weight;
                    grad = Neurons[hidden_neur[i].output_Synapse[j].id_output].delta_deviation * hidden_neur[i].data_out;
                    hidden_neur[i].output_Synapse[j].delta_weight = E * grad + M * hidden_neur[i].output_Synapse[j].delta_weight;
                    hidden_neur[i].output_Synapse[j].weight += hidden_neur[i].output_Synapse[j].delta_weight;
                }

                Neurons[hidden_neur[i].id].delta_deviation = sigmoidDiff(hidden_neur[i].data_out) * sum;
            }

            //Считаем изменение веса синапсов слоя ввода
            var in_neur = Neurons.Where(n => n.Input).ToList();
            for (int i = 0; i < in_neur.Count; i++)
            {
                //сумма произведения всех исходящих весов и дельта-отклонения нейрона, с которым связан синапс
                double sum = 0;

                //градиент для градиентного спуска
                double grad = 0;

                for (int j = 0; j < in_neur[i].output_Synapse.Count; j++)
                {
                    sum += Neurons[in_neur[i].output_Synapse[j].id_output].delta_deviation * in_neur[i].output_Synapse[j].weight;
                    grad = Neurons[in_neur[i].output_Synapse[j].id_output].delta_deviation * in_neur[i].data_out;
                    in_neur[i].output_Synapse[j].delta_weight = E * grad + M * in_neur[i].output_Synapse[j].delta_weight;
                    in_neur[i].output_Synapse[j].weight += in_neur[i].output_Synapse[j].delta_weight;
                }
            }

            print(Neurons);
            #endregion
        }
        //Функция активации
        static double sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));        //Сигмоид
        }

        //Производная функции активации для метода обратного распространения ошибки (МОР)
        static double sigmoidDiff(double x)
        {
            return x * (1 - x);
        }
        
        //Дельта отклонение для выходных нейронов
        static double delta_output(double answer, double result, double x)
        {
            return (answer - result) * sigmoidDiff(x);
        }

        //Дельта отклонение для скрытых нейронов


        //Вывод
        static void print(List<Neuron> NeuroNet)
        {

            foreach (var item in NeuroNet)
            {
                Console.WriteLine(item);
                Console.WriteLine("input synapse:");
                foreach (var synapse in item.input_Synapse)
                {
                    Console.WriteLine(synapse);
                }
                Console.WriteLine("output synapse:");
                foreach (var synapse in item.output_Synapse)
                {
                    Console.WriteLine(synapse);
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.ReadLine();
        }
    }


    class Neuron
    {
        public Neuron()
        {
            input_Synapse = new List<Synapse>();
            output_Synapse = new List<Synapse>();
        }
        public int id { get; set; }
        //Входное значение
        public double data_in { get; set; }
        //Значение на выход после применения функции активации
        public double data_out { get; set; }
        //Дельта отклонение
        public double delta_deviation { get; set; }
        //Список входящих синапсов
        public List<Synapse> input_Synapse { get; set; }
        //Список исходящих синапсов
        public List<Synapse> output_Synapse { get; set; }

        //Входной слой
        private bool isInput = true;
        //Скрытый слой
        private bool isOutput = false;
        //Выходной слой
        private bool isHidden = false;

        public bool Input
        {
            get { return isInput; }
            set
            {
                isInput = value;
                isOutput = !value;
                isHidden = !value;
            }
        }
        public bool Output
        {
            get { return isOutput; }
            set
            {
                isInput = !value;
                isOutput = value;
                isHidden = !value;
            }
        }
        public bool Hidden
        {
            get { return isHidden; }
            set
            {
                isInput = !value;
                isOutput = !value;
                isHidden = value;
            }
        }
        public override bool Equals(object obj)
        {
            return ((Neuron)obj).id == this.id;
        }
        public override string ToString()
        {
            if (isInput)
            {
                return id.ToString() + " input " + data_in.ToString() + " " + data_out.ToString() + " " + delta_deviation;
            }
            if (isOutput)
            {
                return id.ToString() + " output " + data_in.ToString() + " " + data_out.ToString() + " " + delta_deviation;
            }
            if (isHidden)
            {
                return id.ToString() + " hidden " + data_in.ToString() + " " + data_out.ToString() + " " + delta_deviation;
            }
            return id.ToString() + " " + data_in.ToString() + " " + data_out.ToString() + " " + delta_deviation;
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }

    class Synapse
    {
        public Synapse()
        {

        }
        public int id_input { get; set; }
        public int id_output { get; set; }
        public double weight { get; set; }
        public double delta_weight { get; set; }
        public override bool Equals(object obj)
        {
            return ((Synapse)obj).id_input == this.id_input && ((Synapse)obj).id_output == this.id_output;
        }
        public override string ToString()
        {
            return id_input.ToString() + " " + id_output.ToString() + " " + weight.ToString();
        }
        public override int GetHashCode()
        {
            return (this.id_input.ToString() + this.id_output.ToString()).GetHashCode();
        }
    }

}

