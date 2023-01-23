using SharpLearning.Containers.Matrices;
using SharpLearning.Neural.Layers;
using SharpLearning.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLearning.Neural.Learners;
using SharpLearning.Neural.Loss;
using System.IO;
using NeuralProject.Extensions;
using SharpLearning.Neural.Models;
using NeuralProject.Model;
using NeuralProject.Repository;

namespace NeuralProject.SystemRun
{
    public class SystemRun
    {
        public static void Run()
        {
            //XorExample();

/*            Exercise01();
*/
            Exercise02();

        }

        private static void Exercise02()
        {
            List<Iris> irises = DataSource.GetListOfIrises("./data_banknote_authentication.txt");
            irises.Shuffle<Iris>();

            int amoutOfData = irises.Count;    
            int amoutOfAttributes = 4;  
            int numberOfClasses = 2;

            //zbiór uczący:
            F64Matrix observations = new F64Matrix(amoutOfData, amoutOfAttributes);
            double[] targets = new double[amoutOfData];

            int row = 0;
            foreach (Iris iris in irises)
            {
                observations[row, 0] = iris.variance;
                observations[row, 1] = iris.skewness;
                observations[row, 2] = iris.curtosis;
                observations[row, 3] = iris.entropy;
                targets[row] = iris.ClassCode;
                row++;
            }
            //zbiór testujący
            List<Iris> testingListOfIrises = irises.DrawElements(0.20);
            F64Matrix testingObservations = new F64Matrix(testingListOfIrises.Count, amoutOfAttributes);
            double[] testingTargets = new double[testingListOfIrises.Count];

            row = 0;
            foreach (Iris iris in testingListOfIrises)
            {
                testingObservations[row, 0] = iris.variance;
                testingObservations[row, 1] = iris.skewness;
                testingObservations[row, 2] = iris.curtosis;
                testingObservations[row, 3] = iris.entropy;
                testingTargets[row] = iris.ClassCode;
                row++;
            }

            //dodatkowe dane:
            F64Matrix runData = new F64Matrix(2, amoutOfAttributes);
            row = 0;
           // 3.6216,8.6661,-2.8073,-0.44699,0 klasa 0
            runData[row, 0] = 3.6216;
            runData[row, 1] = 8.6661;
            runData[row, 2] = -2.8073;
            runData[row, 3] = -0.44699;
            row++;

            //-1.9983,-6.6072,4.8254,-0.41984,1
            runData[row, 0] = -1.9983;
            runData[row, 1] = -6.6072;
            runData[row, 2] = 4.8254;
            runData[row, 3] = -0.41984;

            row = 0;
            // 3.6216,8.6661,-2.8073,-0.44699,0 klasa 0
            runData[row, 0] = 3.6216;
            runData[row, 1] = 8.6661;
            runData[row, 2] = -2.8073;
            runData[row, 3] = -0.44699;
            row++;

            //-1.9983,-6.6072,4.8254,-0.41984,1
            runData[row, 0] = -1.9983;
            runData[row, 1] = -6.6072;
            runData[row, 2] = 4.8254;
            runData[row, 3] = -0.41984;
            //-1.9983,-6.6072,4.8254,-0.41984,1
            /*
                        3.6216,8.6661,-2.8073,-0.44699,0
            4.5459,8.1674,-2.4586,-1.4621,0
            - 1.9983,-6.6072,4.8254,-0.41984,1
            0.15423,0.11794,-1.6823,0.59524,1*/

            //topologia sieci:
            NeuralNet neuralNet = new NeuralNet();
            ILayer inputLayer = new InputLayer(amoutOfAttributes); // warstwa wejściowa
            neuralNet.Add(inputLayer);
            /*
                        ILayer layer2 = new DenseLayer(8); // warstwa ukryta
                        neuralNet.Add(layer2);*/
            ILayer layer2 = new DenseLayer(6); // warstwa ukryta
            neuralNet.Add(layer2);
            ILayer layer3 = new DenseLayer(5); // warstwa ukryta
            neuralNet.Add(layer3);
            ILayer layer4 = new DenseLayer(4); // warstwa ukryta
            neuralNet.Add(layer4);


            //warstwę wyjściową która klasyfikuje dane jako -1 lub 1
            ILayer outputLayer = new SoftMaxLayer(numberOfClasses); // warstwa wyjściowa
            neuralNet.Add(outputLayer);

            //definiowanie procesu uczenia:
            int netIterations = 1800;
            ILoss logLoss = new LogLoss();
            ClassificationNeuralNetLearner learner =
                new ClassificationNeuralNetLearner(
                    optimizerMethod: SharpLearning.Neural.Optimizers.OptimizerMethod.Adam,
                    net: neuralNet,
                    loss: logLoss,
                    iterations: netIterations
                );

            //proces uczenia:
            var model = learner.Learn(observations, targets,
                testingObservations, testingTargets);


            //wynik działania sieci:
            var testingPredictions = model.Predict(observations);

            //wynik dla danych innych:
             var runPredictions = model.Predict(runData);
            double x = 0;

            Console.WriteLine("Uruchomienie dla zbioru testującego");

            for (int i = 0; i < testingObservations.RowCount; i++)
            {
                if (testingPredictions[i] == testingTargets[i]) { x++; }
                Console.WriteLine(
                    $"({testingObservations[i, 0]}, {testingObservations[i, 1]})->{testingPredictions[i]} (oczekiwano {testingTargets[i]})");
            }
            Console.WriteLine(x / testingObservations.RowCount * 100 + "% skutecznosc zbioru testujacego ");
            Console.WriteLine("Uruchomienie dla danych zewnętrznych:");
            for (int i = 0; i < runData.RowCount; i++)
            {
                Console.WriteLine(
                    $"({runData[i, 0]}, {runData[i, 1]})->{runPredictions[i]}");
            }
            //zapis stanu sieci po nauczeniu:
            string xmlConfigurationContentFile = null;
            StringBuilder str = new StringBuilder();
            using (TextWriter tw = new StringWriter(str))
            {
                model.Save(() => { return tw; });
                xmlConfigurationContentFile = str.ToString();
                try
                {
                    File.WriteAllText("trainedANNconfig.xml", xmlConfigurationContentFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wystąpił problem z pisaniem do pliku {ex}");
                }
            }
        }
        private static void XorExample()
        {
            int amoutOfData = 4;        //ilość danych w zbiorze uczącym
            int amoutOfAttributes = 2;  // bo XoR przyjmuje dwa argumenty
            int numberOfClasses = 2;    // bo XoR zwraca 0 lub 1

            F64Matrix observations = new F64Matrix(amoutOfData, amoutOfAttributes);
            double[] targets = new double[amoutOfData];

            int row = 0;
            observations[row, 0] = 0;
            observations[row, 1] = 0;
            targets[row] = 0;

            row = 1;
            observations[row, 0] = 0;
            observations[row, 1] = 1;
            targets[row] = 1;

            row = 2;
            observations[row, 0] = 1;
            observations[row, 1] = 0;
            targets[row] = 1;

            row = 3;
            observations[row, 0] = 1;
            observations[row, 1] = 1;
            targets[row] = 0;

            //dane to celów uruchomienia sieci
            int amountOfTestingData = 5;
            F64Matrix testingObservations = new F64Matrix(amountOfTestingData, amoutOfAttributes);
            // docelowo klasa 0:
            row = 0;
            testingObservations[row, 0] = 0;
            testingObservations[row, 1] = 0;

            // docelowo klasa 1:
            row = 1;
            testingObservations[row, 0] = 0;
            testingObservations[row, 1] = 1;

            // docelowo klasa 1:
            row = 2;
            testingObservations[row, 0] = 1;
            testingObservations[row, 1] = 0;

            // docelowo klasa 0:
            row = 3;
            testingObservations[row, 0] = 1;
            testingObservations[row, 1] = 1;

            // docelowo klasa 1 ?:
            row = 4;
            testingObservations[row, 0] = 0.2;
            testingObservations[row, 1] = 1.1;

            //tolopogia sieci:
            NeuralNet neuralNet = new NeuralNet();
            ILayer inputLayer = new InputLayer(amoutOfAttributes); // warstwa wejściowa
            neuralNet.Add(inputLayer);

            ILayer layer1 = new DenseLayer(3); // warstwa ukryta
            neuralNet.Add(layer1);

            //warstwę wyjściową która klasyfikuje dane jako 1 lub 0
            ILayer outputLayer = new SoftMaxLayer(numberOfClasses); // warstwa wyjściowa
            neuralNet.Add(outputLayer);

            //definiowanie procesu uczenia:
            int netIterations = 1000;
            ILoss logLoss = new LogLoss();
            ClassificationNeuralNetLearner learner =
                new ClassificationNeuralNetLearner(
                    net: neuralNet,
                    loss: logLoss,
                    iterations: netIterations
                );

            //proces uczenia:
            var model = learner.Learn(observations, targets, 
                observations, targets);


            //wynik działania sieci:
            var predictions = model.Predict(testingObservations);

            for (int i = 0; i < testingObservations.RowCount; i++)
            {
                Console.WriteLine(
                    $"({testingObservations[i, 0]},{ testingObservations[i, 1]})->{ predictions[i]}");
            }

            //zapis stanu sieci po nauczeniu:
            string xmlConfigurationContentFile = null;
            StringBuilder str = new StringBuilder();
            using (TextWriter tw = new StringWriter(str))
            {
                model.Save(() => { return tw; });
                xmlConfigurationContentFile = str.ToString();
                try
                {
                    File.WriteAllText("trainedANNconfig.xml", xmlConfigurationContentFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wystąpił problem z pisaniem do pliku {ex}");
                }
            }

            //odczyt konfiguracji nauczonej sieci z pliku xml:
            ClassificationNeuralNetModel model2;
            string contentOfALoadedFile;
            try
            {
                contentOfALoadedFile = File.ReadAllText("trainedANNconfig.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił probelm z czytaniem pliku {ex}");
                return;
            }
            using (TextReader tr = new StringReader(contentOfALoadedFile))
            {
                model2 = ClassificationNeuralNetModel.Load(() => { return tr; });
            }

            // Uruchomienie wczytanej z pliku sieci dla tych samych danych 
            Console.WriteLine("Uruchomienie sieci wczytanej z pliku:");
            var predictions2 = model2.Predict(testingObservations);

            for (int i = 0; i < testingObservations.RowCount; i++)
            {
                Console.WriteLine($"({testingObservations[i, 0]}, {testingObservations[i, 1]})->{predictions2[i]}");
            }

            Console.WriteLine();
        }

        private static void Exercise01()
        {
            int amoutOfData = 200;
            int amoutOfAttributes = 2;
            int numberOfClasses = 2;

            //wygenerowanie danych:
            List<PointE> listOfPoints = DataSource.GetListOfSymetricPointsForExercise01(amoutOfData);


            //zbiór uczący:
            F64Matrix observations = new F64Matrix(amoutOfData, amoutOfAttributes);
            double[] targets = new double[amoutOfData];

            int row = 0;
            foreach (PointE point in listOfPoints)
            {
                observations[row,0] = point.X;
                observations[row,1] = point.Y;
                targets[row] = point.Value;
                row++;
            }

            //zbiór testujący
            List<PointE> testingListOfPoints = listOfPoints.DrawElements(0.2);
            F64Matrix testingObservations = new F64Matrix(testingListOfPoints.Count, amoutOfAttributes);
            double[] testingTargets = new double[testingListOfPoints.Count];

            row = 0;
            foreach (PointE point in testingListOfPoints)
            {
                testingObservations[row, 0] = point.X;
                testingObservations[row, 1] = point.Y;
                testingTargets[row] = point.Value;
                row++;
            }

            //dodatkowe dane:
            F64Matrix runData = new F64Matrix(2, amoutOfAttributes);
            row = 0;
            //klasa -1
            runData[row, 0] = -12;
            runData[row, 1] = -9;
            row++;
            //klasa 1
            runData[row, 0] = 8;
            runData[row, 1] = 13;


            //topologia sieci:
            NeuralNet neuralNet = new NeuralNet();
            ILayer inputLayer = new InputLayer(amoutOfAttributes); // warstwa wejściowa
            neuralNet.Add(inputLayer);

            ILayer layer1 = new DenseLayer(3); // warstwa ukryta
            neuralNet.Add(layer1);

            //warstwę wyjściową która klasyfikuje dane jako -1 lub 1
            ILayer outputLayer = new SoftMaxLayer(numberOfClasses); // warstwa wyjściowa
            neuralNet.Add(outputLayer);

            //definiowanie procesu uczenia:
            int netIterations = 500;
            ILoss logLoss = new LogLoss();
            ClassificationNeuralNetLearner learner =
                new ClassificationNeuralNetLearner(
                    net: neuralNet,
                    loss: logLoss,
                    iterations: netIterations
                   // batchSize: 2
                );

            //proces uczenia:
            var model = learner.Learn(observations, targets,
                testingObservations, testingTargets);


            //wynik działania sieci:
            var testingPredictions = model.Predict(testingObservations);

            //wynik dla danych innych:
            var runPredictions = model.Predict(runData);

            Console.WriteLine("Uruchomienie dla zbioru testującego");
            for (int i = 0; i < testingObservations.RowCount; i++)
            {
                Console.WriteLine(
                    $"({testingObservations[i, 0]}, {testingObservations[i, 1]})->{testingPredictions[i]} (oczekiwano {testingTargets[i]})");
            }

            Console.WriteLine("Uruchomienie dla danych zewnętrznych");
            for (int i = 0; i < runData.RowCount; i++)
            {
                Console.WriteLine(
                    $"({runData[i, 0]}, {runData[i, 1]})->{runPredictions[i]}");
            }

        }
}
}
