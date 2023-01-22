using System.Text;

namespace Dictionary
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Dictionary";
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Vocabulary dic = new();

            dic.DeserializeData();
            dic.AddWord("Ukrainian - English", new Word("тест", "test"));
            dic.AddTranslete("Ukrainian - English", "тест", "123ф");
            dic.AddTranslete("Ukrainian - English", "тест", "1231");
            dic.AddTranslete("Ukrainian - English", "тест", "1233");
            dic.AddTranslete("Ukrainian - English", "тест", "1235");
            dic.ShowAllWord("Ukrainian - English");
            //dic.EditTranslete("Ukrainian - English", "тест", dic.SelectTranclete("Ukrainian - English", "тест"), "ХХХРРРЕЕЕНННЬ");
            //Console.WriteLine("\n\nafter ");
            //dic.ShowWord("Ukrainian - English", "тест");
            Console.ReadKey();
            Console.Clear();
            dic.Translete("Ukrainian - English");
            
            
            
            //dic.AddLanguage(lang);
            //dic.ShowAllWord(lang);



            ///Додавання слів
            //string lang = "Ukrainian - English";
            //while (true)
            //{

            //    Console.Clear();
            //    Console.WriteLine("Введіть слово");
            //    string word = Console.ReadLine();
            //    Console.WriteLine("Введіть переклад слова");
            //    string trans = Console.ReadLine();
            //    dic.AddWord(lang, new Word(word, trans));

            //    Console.WriteLine("Для завершення натисніть ЕСК");
            //    ConsoleKeyInfo cki = Console.ReadKey();
            //    if (cki.Key == ConsoleKey.Escape) { break; }

            //}
            //dic.SerializeData();


            //while (true)
            //{
            //    string lang = dic.SelectLanguage();
            //    string word = dic.SelectWord(lang);
            //    Console.WriteLine($"{word}, {lang}");
            //    Console.ReadKey();
            //}

            //while (true)
            //{
            //    dic.CreateLanguage();

            //    //Console.ReadKey();
            //    //Console.Clear();

            //    Console.WriteLine("Для завершення натисніть ЕСК");
            //    ConsoleKeyInfo cki = Console.ReadKey();
            //    if (cki.Key == ConsoleKey.Escape) { Console.WriteLine("_"); break; }

            //}
            //    foreach (string s in dic.GetLanguageList())
            //    Console.WriteLine(s);
        }
    }
}