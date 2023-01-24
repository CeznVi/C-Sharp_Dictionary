using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    public class App
    {
        /*--------------------Властивості(змінні)----------------------*/
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

        /*--------------------Меню додатка----------------------*/
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
        }
        /// <summary>
        /// Меню експорту слова у файл
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
       
        /// <summary>
        /// Меню роботи із словником
        /// </summary>
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
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            EdtW();
                            break;
                        }
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
            
        }

        /*--------------------Допоміжні методи----------------------*/
        /// <summary>
        /// Друкує "Натисніть будь яку клавішу щоб продовжити"
        /// </summary>
        private void PressAnyKey()
        {
            Console.WriteLine("Для продовження натисніть будь яку клавішу");
            Console.ReadKey();
        }
        /// <summary>
        /// Друкує назву поточного словника
        /// </summary>
        private void WhereAreYou()
        {
            if (lang != string.Empty)
            {
                Console.SetCursorPosition(30, 20);
                Console.WriteLine($"---Ви знаходитесь у словнику: {lang} ---");
                Console.SetCursorPosition(0, 0);
            }
        }
        /// <summary>
        /// Отримує від користувача список перекладів
        /// </summary>
        /// <returns>Список перекладів</returns>
        private List<string> GetTransleteListFromUser()
        {
            List<string> translete = new();

            while (true)
            {
                Console.WriteLine("Введіть слово переклад");
                string trans = Console.ReadLine();

                if (trans != string.Empty)
                {
                    if ((translete.Find(t => t == trans)) == trans)
                        Console.WriteLine("Такий переклад вже існує");
                    else
                    {
                        Console.WriteLine($"Переклад {trans} успішно додано до варіантів перекладу");
                        translete.Add(trans);
                        Console.WriteLine("Для того щоб закінчити з додаванням перекладів натисніть клавішу ESC");
                        Console.WriteLine("Або натисніть будь яку іншу клавішу щоб додати ще варіант перекладу");

                        System.ConsoleKeyInfo k = Console.ReadKey();
                        Console.Clear();

                        if (k.Key == ConsoleKey.Escape)
                            break;
                    }
                }
                else
                    Console.WriteLine("Поле перекладу не має бути порожнім");
            }

            return translete;
        }
        /// <summary>
        /// Отримує від користувача слово
        /// </summary>
        /// <returns>Слово</returns>
        private string GetWordFromUser()
        {
            while (true)
            {
                Console.WriteLine("Введіть слово");
                word = Console.ReadLine();

                if (word != string.Empty && dic.isExistWordInVoc(lang, word) != true)
                    break;
                else
                    Console.WriteLine("Невірне значення");
            }
            return word;
        }

        /*--------------Методи дій ----------------*/
        /// <summary>
        /// Метод додає слово з перекладом
        /// </summary>
        private void AddW()
        {

            string word = GetWordFromUser();

            Console.Clear();
            Console.WriteLine($"Ви створили слово: {word}, тепер попрацюємо з його перекладом");
            PressAnyKey();

            List<string> translete = GetTransleteListFromUser();
            Word w = new(word, translete);
            Console.Clear();

            dic.AddWord(lang, w);
            dic.SerializeData();
            Console.WriteLine("_У словник додано:");
            dic.ShowWord(lang, word);
            PressAnyKey();
        }

        private void EdtW()
        {
            Console.WriteLine("Виберіть слово для його зміни");
            string word = dic.SelectWord(lang);

        }
        

    }
}
