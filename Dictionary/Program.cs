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

            //Word word2 = new("cat", "кіт");

            //Console.WriteLine(word);
            //word.RemoveTranslete("пес");
            //Console.WriteLine(word2);
            //Console.WriteLine("\n Тестування Гет транслейт");
            //foreach (string item in word.GetTranslete())
            //    Console.Write(item + ' ');

            Vocabulary dic = new();
            dic.AddLanguage("English - Ukrainian");
            dic.AddLanguage("English - Russian");
            dic.AddLanguage("Ukrainian - English");
            dic.AddLanguage("Russian - English");

            //foreach (string item in dic.GetLanguageList())
            //    Console.WriteLine(item);

            dic.AddWord("English - Ukrainian", word);


            Word word3 = new();
            word3.BWord = "test";
            word3.AddTranslete("с123");
            word3.AddTranslete("п123");
            word3.AddTranslete("со132");

            dic.AddWord("English - Ukrainian", word3);
            dic.AddWord("English - Ukrainian", new Word("Vasya","Вася"));


            //dic.ShowAllWord("English - Ukrainian");
            dic.AddTranslete("English - Ukrainian", "dog","псюк");
            dic.ShowWord("English - Ukrainian", "dog");

            dic.RemoveTranslete("English - Ukrainian", "dog", "псюк");
            dic.RemoveTranslete("English - Ukrainian", "dog", "собака");
            dic.RemoveTranslete("English - Ukrainian", "dog", "собак");
            dic.RemoveTranslete("English - Ukrainian", "dog", "пес");
            dic.ShowWord("English - Ukrainian", "dog");

            Console.WriteLine("\n Remove test");
            dic.RemoveWord("English - Ukrainian", "test");
            dic.ShowAllWord("English - Ukrainian");
            ////ТЕСТ ТРАЙ КЕТЧ
            //dic.ShowAllWord("error");

        }
    }
}