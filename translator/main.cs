using System;

namespace Calculator
{
    static class Program
    {
        static void Main(string[] args)
        {
            string input = args[0];
            Translator translator = new Translator();
            string result = translator.Translate(input);
            Console.WriteLine("Translated result is: {0}", result);
        }
    }
}
