using System;

namespace NeuralProject.Terminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SystemRun.SystemRun.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
