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



            dic.ShowAllWord("English - Ukrainian");
            dic.ExportData("English - Ukrainian", "test");



        }
    }
}