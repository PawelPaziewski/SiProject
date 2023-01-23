namespace NeuralProject.Model
{
    /// <summary>
    //7. Attribute Information:
    //1. sepal length in cm
    //2. sepal width in cm
    //3. petal length in cm
    //4. petal width in cm
    //5. class:
    //-- Iris Setosa
    //    -- Iris Versicolour
    //-- Iris Virginica
    // source: http://archive.ics.uci.edu/ml/datasets/Iris
    /// </summary>
    public class Iris
    {
        public double variance { get; set; }
        public double skewness { get; set; }
        public double curtosis { get; set; }
        public double entropy { get; set; }
        public double ClassCode { get; set; }
    }
}