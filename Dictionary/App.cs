using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class App
    {
        /// <summary>
        /// Словник
        /// </summary>
        private Vocabulary dic = new();
        /// <summary>
        /// Рядок у якому зберігається поточний словник
        /// </summary>
        private string lang = "";
        /// <summary>
        /// Рядок у якому зберігається поточне слово
        /// </summary>
        private string word = "";

        /// <summary>
        /// Головне меню додатка
        /// </summary>
        public void Run()
        {
            dic.DeserializeData();

            List<string> listmenu = new()
            {
                "Переклад слова",
                "Переклад слів",
                "Робота із словником",
                "Вибір словника",
                "Менеджер словників",
                "Вихід"
            };

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                WhereAreYou(); 
                
                int a = ConsoleMenu.SelectVertical(HPosition.Center,
                                    VPosition.Top,
                                    HorizontalAlignment.Center,
                                    listmenu);

                switch (a)
                {
                    case 0:
                        {
                            if(lang == string.Empty) { lang = dic.SelectLanguage(); }
                            word = dic.TransleteWord(lang);

                            if(word != string.Empty) 
                            {
                                ExportData();
                            }
                            word = "";
                            break;
                        }
                    case 1:
                        if (lang == string.Empty) { lang = dic.SelectLanguage(); }
                        dic.Translete(lang);
                        break;
                    case 2:
                        ManageWord();
                        break;
                    case 3:
                           lang = dic.SelectLanguage();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
           
            Console.Clear();
            dic.SerializeData();
        }
        /// <summary>
        /// Метод експорту слова у файл
        /// </summary>
        private void ExportData()
        {
            List<string> listmenu = new()
            {
                "Експортувати данні у файл",
                "Вихід",
            };

            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                int a = ConsoleMenu.SelectVertical(HPosition.Center,
                                    VPosition.Top,
                                    HorizontalAlignment.Center,
                                    listmenu);

                switch (a)
                {
                    case 0:
                        {
                            dic.ExportData(lang,word);
                            return;
                        }
                    case 1:
                        exit = true;
                        break;
                    default:
                        exit = true;
                        break;
                }
            }
        }
       
        private void ManageWord()
        {
            if (lang == string.Empty) { lang = dic.SelectLanguage(); }

            List<string> listmenu = new()
            {
                "Показати всі слова",
                "   Додати слово   ",
                "   Змінити слово  ",
                "   Видалити слово ",
                "  Додати переклад ",
                " Змінити переклад ",
                " Видалити переклад",
                "Вийти у головне меню"
            };

            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                WhereAreYou();

                int a = ConsoleMenu.SelectVertical(HPosition.Center,
                                    VPosition.Top,
                                    HorizontalAlignment.Center,
                                    listmenu);

                switch (a)
                {
                    case 0:
                        {
                            Console.Clear();
                            dic.ShowAllWord(lang);
                            PressAnyKey();
                            break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            AddW();
                            PressAnyKey();
                            break;
                        }
                    case 2:

                        break;
                    case 3:
                        lang = dic.SelectLanguage();
                        break;


                    case 7:
                        exit = true;
                        return;
                    default:
                        exit = true;
                        return;
                }
            }

            Console.Clear();
            dic.SerializeData();
        }

        private void PressAnyKey()
        {
            Console.WriteLine("Для продовження натисніть будь яку клавішу");
            Console.ReadKey();
        }
        private void WhereAreYou()
        {
            if (lang != string.Empty)
            {
                Console.SetCursorPosition(30, 20);
                Console.WriteLine($"---Ви знаходитесь у словнику: {lang} ---");
                Console.SetCursorPosition(0, 0);
            }
        }
       
        /// ТУ ДУ
        private void AddW()
        {
            string word = "";
            while (true)
            {
                Console.WriteLine("Введіть слово");
                word = Console.ReadLine();

                if(word != string.Empty) { break; }
                else { Console.WriteLine("Невірне значення"); }
            }

            List<string> translete = new();

            //while (true)
            //{

            //}
        }
    }
}
