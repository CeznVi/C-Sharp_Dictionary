using System.Text;

namespace Dictionary
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Dictionary";
            Console.OutputEncoding = Encoding.Unicode;

            Word word = new();
            word.BWord = "dog";
            word.AddTranslete("собака");
            word.AddTranslete("пес");
            word.AddTranslete("собак");

            Word word2 = new("cat", "кіт");

            Console.WriteLine(word);
            word.RemoveTranslete("пес");
            Console.WriteLine(word2);

            //Console.WriteLine("\n Тестування Гет транслейт");
            //foreach (string item in word.GetTranslete())
            //    Console.Write(item + ' ');






        }
    }
}