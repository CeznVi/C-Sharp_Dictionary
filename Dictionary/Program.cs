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

            //dic.DeserializeData();


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

            while (true)
            {
                dic.CreateLanguage();

                //Console.ReadKey();
                //Console.Clear();

                Console.WriteLine("Для завершення натисніть ЕСК");
                ConsoleKeyInfo cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Escape) { Console.WriteLine("_"); break; }

            }
                foreach (string s in dic.GetLanguageList())
                Console.WriteLine(s);
        }
    }
}