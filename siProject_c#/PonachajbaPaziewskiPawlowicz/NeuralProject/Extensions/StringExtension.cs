using System;
using NeuralProject.Model;
using System.Collections.Generic;

namespace NeuralProject.Extensions
{
    public static class StringExtension
    {
        private static Dictionary<string, double> _irisType;

        public static Iris ToIris(this string stringIris)
        {
            if ( ! string.IsNullOrEmpty(stringIris))
            {
                string[] irisArr = stringIris.Replace(',',';').Replace('.',',').Split(';');
                if (irisArr.Length != 5)
                {
                    return null;
                }

                Iris iris = new Iris()
                {
                    variance = double.Parse(irisArr[0]),
                    skewness = double.Parse(irisArr[1]),
                    curtosis = double.Parse(irisArr[2]),
                    entropy = double.Parse(irisArr[3]),
                    ClassCode = double.Parse(irisArr[4]),
                };

                return iris;
            }

            return null;
        }

        public static double ToClassValue(this string stringIris)
        {
            if( _irisType.ContainsKey(stringIris ) )
            {
                return _irisType[stringIris];
            }
            throw new Exception($"Nieprawidłowy typ irysa w pliku {stringIris}");
        }
    }
}