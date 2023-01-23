using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace NeuralProject.Extensions
{
    public static class ListExtension
    {
        private static Random _random;

        static ListExtension()
        {
            _random= new Random();
        }
        public static List<T> DrawElements<T>(this List<T> list,double p)
        {
            if (p <= 0 || p > 1)
            {
                throw new ArgumentOutOfRangeException( $"Zły współczynnik p={p}" );
            }

            int countOfElements = Convert.ToInt32(list.Count * p);
            List<T> result = new List<T>();

            for (int i = 0; i < countOfElements; i++)
            {
                result.Add(list[   _random.Next(list.Count)  ]);
            }

            return result;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            int m = n * 2;
            for (int i = 0; i < m; i++)
            {
                int z = _random.Next(n);
                int j = _random.Next(n);
                T value = list[j];
                list[j] = list[z];
                list[z] = value;
            }
        }
    }
}