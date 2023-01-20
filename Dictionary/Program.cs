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

            foreach (var i in dic.GetWordList("English - Ukrainian"))
                Console.WriteLine(i);

            //dic.ShowAllWord("English - Ukrainian");
            dic.EditWord("English - Ukrainian", "test", "test99");

            Console.WriteLine("\n\tРедагування слова!!!!!!!!!!");
            foreach (var i in dic.GetWordList("English - Ukrainian"))
                Console.WriteLine(i);



        }
    }
}