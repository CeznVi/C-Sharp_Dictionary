using System.Text;

namespace Dictionary
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "Dictionary";
            Console.OutputEncoding = Encoding.Unicode;

            Vocabulary dic = new();

            dic.DeserializeData();


            //dic.ShowWord("English - Ukrainian", "test");

            while (true)
            {
                string lang = dic.SelectLanguage();
                string word = dic.SelectWord(lang);
                Console.WriteLine($"{word}, {lang}");
               Console.ReadKey();
            }

        }
    }
}