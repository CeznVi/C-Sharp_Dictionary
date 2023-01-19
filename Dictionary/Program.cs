using System.Text;

namespace Dictionary
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Dictionary";
            Console.OutputEncoding = Encoding.Unicode;

            //Word word = new("dog");
            //word.AddTranslete("собака");
            //word.AddTranslete("пес");
            //word.AddTranslete("собак");

            //Word word2 = new("cat", "кіт");

            //Console.WriteLine(word);
            //word.RemoveTranslete("пес");
            //Console.WriteLine(word2);
            //Console.WriteLine("\n Тестування Гет транслейт");
            //foreach (string item in word.GetTranslete())
            //    Console.Write(item + ' ');

            Vocabulary dic = new();
            //dic.AddLanguage("English - Ukrainian");
            //dic.AddLanguage("English - Russian");
            //dic.AddLanguage("Ukrainian - English");
            //dic.AddLanguage("Russian - English");

            //foreach (string item in dic.GetLanguageList())
            //    Console.WriteLine(item);

            //dic.AddWord("English - Ukrainian", word);


            //Word word3 = new("test");
            //word3.AddTranslete("с123");
            //word3.AddTranslete("п123");
            //word3.AddTranslete("со132");

            //dic.AddWord("English - Ukrainian", word3);
            //dic.AddWord("English - Ukrainian", new Word("Vasya", "Вася"));
            //dic.AddWord("123", word3);

            //dic.ShowAllWord("English - Ukrainian");

            //dic.AddTranslete("English - Ukrainian", "dog", "псюк");




            //dic.ShowWord("English - Ukrainian", "dog");

            //dic.RemoveTranslete("English - Ukrainian", "dog", "псюк");
            //dic.RemoveTranslete("English - Ukrainian", "dog", "собака");
            //dic.RemoveTranslete("English - Ukrainian", "dog", "собак");
            //dic.RemoveTranslete("English - Ukrainian", "dog", "пес");
            //dic.ShowWord("English - Ukrainian", "dog");

            //Console.WriteLine("\n Remove test");
            //dic.RemoveWord("English - Ukrainian", "test");
            //dic.ShowAllWord("English - Ukrainian");
            ////ТЕСТ ТРАЙ КЕТЧ
            //dic.ShowAllWord("error");

            //dic.RemoveWord("English - Ukrainian", "1111");
            //List<string> s = new (dic.GetTransleteWordList("English - Ukrainian", "dog"));
            //foreach(string s2 in s) 
            //{
            //    Console.WriteLine(s2);
            //}


            /////SEREALIZATION
            //dic.SerializeData();
            //dic.ShowAllWord("English - Ukrainian");
            //dic = new();
            //Console.ReadKey();
            //Console.Clear();
            dic.DeserializeData();
            Console.WriteLine("десеарілізованні данні");
            dic.AddWord("English - Ukrainian", new Word("Abadon", "Абадон"));
            dic.ShowAllWord("English - Ukrainian");




            Console.WriteLine("\n\n");
            foreach (string item in dic.GetWordList("English - Ukrainian"))
                Console.WriteLine(item);


        }
    }
}