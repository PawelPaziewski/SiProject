using NeuralProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using NeuralProject.Extensions;

namespace NeuralProject.Repository
{
    public static class DataSource
    {
        private static Random _random = new Random();
        public static List<PointE> GetListOfSymetricPointsForExercise01(int n)
        {
            List<PointE> points= new List<PointE>();
            int width = 10;
            PointE pointM = new PointE()
            {
                X= -10,
                Y= -10,
                Value = -1
            };            
            PointE pointP = new PointE()
            {
                X= 10,
                Y= 10,
                Value = 1
            };

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    points.Add(new PointE
                    {
                        X = pointP.X + _random.Next(width+1) - width/2,
                        Y = pointP.Y + _random.Next(width + 1) - width / 2,
                        Value = pointP.Value
                    });
                }
                else
                {
                    points.Add(new PointE
                    {
                        X = pointM.X + _random.Next(width + 1) - width / 2,
                        Y = pointM.Y + _random.Next(width + 1) - width / 2,
                        Value = pointM.Value
                    });
                }
            }


            return points;
        }

        public static List<Iris> GetListOfIrises(string filePath)
        {
            if( !File.Exists(filePath) )
            {
                throw new FileLoadException(  $"Brak pliku {filePath}");
            }
            List<Iris> list = new List<Iris>();
            using (TextReader tr = new StreamReader(filePath))
            {
                string line;
                Iris iris;
                while ((line = tr.ReadLine()) != null)
                {
                    iris = line.ToIris();
                    if( iris != null  )
                        list.Add( iris );
                }
            }
            return list;
        }
    }
}