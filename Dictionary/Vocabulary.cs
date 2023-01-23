using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Dictionary
{
    /// <summary>
    /// Клас контейнер словника
    /// </summary>
    public class Vocabulary
    {
        /*--------------------Властивості(змінні)----------------------*/
        /// <summary>
        /// Словник (вдосконалений Dictionary для підтримки сериалізації)
        /// </summary>
        private SerializableDictionary<string, List<Word>> vc = new();
        /// <summary>
        /// Контейнер в якому зберігаються базові назви мов для словників
        /// </summary>
        private readonly List<string> languagesList = new()
        {
            "Українська",
            "Російська",
            "Англійська",
            "Французька",
            "Німецька",
        };

        /*---------------------------Методи----------------------------*/
        /*---Переклад---*/
        /// <summary>
        /// Отримання від користувача слово, друк перекладу
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <returns>пошукове слово</returns>
        public string TransleteWord(string lang)
        {
            Console.Clear();
            Console.WriteLine("Введіть слово для пошуку перекладу");
            string word = Console.ReadLine();
            ShowWord(lang, word);
            Console.ReadKey();
            if (isExistWordInVoc(lang, word) == true) return word;
            else return string.Empty;
        }
        /// <summary>
        /// Тестова функція для перекладу реченнь. Нажаль є багато нюансів і
        /// потрібно більше часу і знань, щоб реалізувати повністю функціонал
        /// </summary>
        /// <param name="lang">Словник</param>
        public void Translete(string lang)
        {
            Console.Clear();
            Console.WriteLine("Переклад багьох слів (речень) працює у тестовому режимі. Відмінок вводимих слів відіграє роль," +
                "також існує проблема із слово-сполученнями");
            Console.WriteLine("Введіть що потрібно перекласти");
            
            string input = Console.ReadLine();
            char[] separators = new char[] { '.', ',', ':', ';', '?', '!', '\n' };

            string[] arrInput =  input.Split(' ', StringSplitOptions.TrimEntries);

            string output = "";
            for (int i = 0; i < arrInput.Length; i++)
            {
                if ((vc[lang].Find(w => w.BWord.ToLower() == arrInput[i].ToLower().TrimEnd(separators))) != null)
                {
                    Word word = vc[lang].Find(w => w.BWord.ToLower() == arrInput[i].ToLower().TrimEnd(separators));
                    output += word.TWord[0];

                    if (separators.Contains(arrInput[i].ElementAt(arrInput[i].Length -1)))
                    {
                        output += arrInput[i].ElementAt(arrInput[i].Length -1) + " ";
                    }
                    else
                        output += " ";

                }
                else
                    output += arrInput[i] + " ";
            }

            Console.WriteLine($"Оригінал:\n{input}");
            Console.WriteLine($"Переклад:\n{output}");
            Console.WriteLine("Для продовження натисніть будь яку клавішу");
            Console.ReadKey();
        }

        /*---Створюють об'єкти---*/
        /// <summary>
        /// Створює словник
        /// </summary>
        public void CreateLanguage()
        {
            
            try 
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Виберіть базову мову словника");
                    string first = SelectFromList(languagesList, languagesList.Count);
                    Console.WriteLine("Виберіть мову перекладу");
                    string second = SelectFromList(languagesList, languagesList.Count);

                    if (first != second)
                    {
                        AddLanguage($"{first} - {second}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Помилка. Не вірна назва словника.");
                        Console.ReadKey();
                    }
                }
           
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        /*---Додають данні---*/
        /// <summary>
        /// Додати Словник (Мова - мова перекладу) до колекції
        /// </summary>
        /// <param name="language">Мова - мова перекладу</param>
        public void AddLanguage(string lang)
        {
            try
            {
                if (lang != null)
                    vc.Add(lang, new List<Word>());
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine($"Такий словник: \"{lang}\" вже існує");
                Console.WriteLine("Спробуйте ще раз");
                Console.ReadKey();
                CreateLanguage();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Додати слово(як об'єкт) до колекції
        /// </summary>
        /// <param name="lang">Словник(мова - мова переклад)</param>
        /// <param name="wordClass">Контейнер слова</param>
        public void AddWord(string lang, Word wordClass)
        {
            try
            {
                if (vc[lang].Find(w => w.BWord == wordClass.BWord) != null)
                    throw new ArgumentException($"Слово \"{wordClass.BWord}\" вже є у словнику");
                else
                {
                    vc[lang].Add(wordClass);
                    vc[lang].Sort();
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника {lang} не існує");
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Додати переклад(до певного слова) до колекції
        /// </summary>
        /// <param name="lang">Словник(мова - мова переклад)</param>
        /// <param name="word">Слово</param>
        /// <param name="trans">Переклад</param>
        public void AddTranslete(string lang, string word, string trans)
        {
            try
            {
                if (vc[lang].Find(w => w.BWord == word).TWord.Find(w => w.Equals(trans)) != null)
                    throw new ArgumentException(vc[lang].Find(w => w.BWord == word).BWord);
                else
                    (vc[lang].Find(w => w.BWord == word)).AddTranslete(trans);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника {lang} не існує");
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Переклад \"{trans}\" вже є у слові {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*---Повертають списки---*/
        /// <summary>
        /// Повернути список словників (Мова - мова перекладу)
        /// </summary>
        /// <returns>Сисок "Мова - мова перекладу"</returns>
        public List<string> GetLanguageList()
        {
            List<string> languageList = new();

            foreach (var item in vc)
                languageList.Add(item.Key);

            return languageList;
        }
        /// <summary>
        /// Повернути список слів
        /// </summary>
        /// <param name="lang">Словник(мова - мова переклад)</param>
        /// <returns>Список слів</returns>
        public List<string> GetWordList(string lang)
        {
            List<string> WordList = new();
            try
            {
                foreach (Word item in vc[lang])
                {
                    WordList.Add(item.BWord);
                }
                WordList.Sort();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника {lang} не існує");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return WordList;
        }
        /// <summary>
        /// Повернути список переводів слова
        /// </summary>
        /// <param name="lang">Словник(мова - мова переклад)</param>
        /// <param name="word">Слово</param>
        /// <returns>Список переводів слова</returns>
        public List<string> GetTransleteWordList(string lang, string word)
        {

            List<string> WordList = new();

            if (vc[lang].Find(w => w.BWord == word) == null)
                Console.WriteLine($"Слова {word} не існує");
            else
            {
                try
                {
                    Word temp = vc[lang].Find(w => w.BWord == word);

                    if (temp != null)
                    {
                        foreach (string item in temp)
                        {
                            WordList.Add(item);
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Слова {word} не існує");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Словника {lang} не існує");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return WordList;
        }

        /*---Повертають вибрану строку---*/
        /// <summary>
        /// Повертає назву словника через його вибір зі списку
        /// </summary>
        /// <returns>Повертає назву словника</returns>
        public string SelectLanguage()
        {
            Console.Clear();
            List<string> LanguageList = GetLanguageList();

            if (LanguageList.Count == 0)
            {
                CreateLanguage();
                return "";
            }
            else
            {
                Console.WriteLine("Виберіть словник із списку нижче");
                for (int i = 0; i < LanguageList.Count; i++)
                {
                    Console.WriteLine($"{i} - {LanguageList[i]}");
                }
                try
                {
                    int selec = 0;
                    while (true)
                    {
                        selec = int.Parse(Console.ReadLine());

                        if (selec < LanguageList.Count && selec >= 0)
                            break;
                        else
                            Console.WriteLine("Не вірний вибор");
                    }
                    Console.WriteLine($"Ви обрали: {LanguageList[selec]}");
                    Console.WriteLine("Натисніть будь яку клавішу щоб продовжити");
                    Console.ReadKey();
                    return LanguageList[selec];
                }
                catch(System.FormatException)
                {
                    Console.WriteLine("Введені данні не мають бути порожніми");
                    return SelectLanguage();
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine("Введені данні не мають бути порожніми");
                    return SelectLanguage();
                }

            }
        }
        /// <summary>
        /// Повертає слово через його вибор із списку
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <returns>Повертає слово</returns>
        public string SelectWord(string lang)
        {
            Console.Clear();
            try
            {
                List<string> WordList = GetWordList(lang);

                if (WordList.Count == 0) 
                { 
                    return ""; 
                }
                else
                {
                    return SelectFromList(WordList);
                }

            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника {lang} не існує");
                Console.WriteLine("Натисніть будь яку клавішу щоб продовжити");
                Console.ReadKey();
                return "";
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Натисніть будь яку клавішу щоб продовжити");
                Console.ReadKey();
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Натисніть будь яку клавішу щоб продовжити");
                Console.ReadKey();
                return "";
            }
        }
        /// <summary>
        /// Повертає переклад через вибір його із списку
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        /// <returns>Повертає вибраний переклад</returns>
        public string SelectTranclete(string lang, string word)
        {
            try
            {
                List<string> temp = GetTransleteWordList(lang, word);
                return SelectFromList(temp, temp.Count);
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                return "ERORR!";
            }

            
        }

        /*---Друкують данні---*/
        ///ADD try CATCH TKEY == неіснує
        /// <summary>
        /// Показати всі слова у вибраному словнику
        /// </summary>
        /// <param name="lang">Мова - мова переклад</param>
        public void ShowAllWord(string lang) 
        {
            
            try
            {
                if (vc[lang].Count == 0)
                    throw new Exception($"У словнику {lang} ще немає слів");

                if (lang != null)
                    foreach (Word item in vc[lang])
                        Console.WriteLine(item);
            }
            catch (KeyNotFoundException) 
            { 
                Console.WriteLine($"Словника \"{lang}\" не існує, вивід не можливий"); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Показати переклад слова у відповідному словнику
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        public void ShowWord(string lang, string word) 
        {
            try
            {
                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException($"Слова \"{word}\" немає у словнику");

                Console.WriteLine("Слово\t Переклад");
                Console.WriteLine(vc[lang].Find(w => w.BWord == word));
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, вивід не можливий");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*---Видаляють данні---*/
        /// <summary>
        /// Видалити словник
        /// </summary>
        /// <param name="lang">Словник</param>
        public void RemoveLanguage(string lang)
        {
            try
            {
                if (vc[lang] == null) throw new KeyNotFoundException();
                
                vc.Remove(lang);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, видалення не можливе");
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine($"Такий словник: \"{lang}\" не існує");
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Видалити слово
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        public void RemoveWord(string lang, string word)
        {
            try
            {
                if (vc[lang] == null) throw new KeyNotFoundException();
                
                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException();

                _ = vc[lang].Remove(vc[lang].Find(w => w.BWord == word));
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, видалення не можливе");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Слова {word} не існує");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Слова \"{word}\" немає у словнику");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Видалити переклад у слові
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        /// <param name="trans">Переклад</param>
        public void RemoveTranslete(string lang, string word, string trans)
        {
            try
            {
                if (vc[lang] == null) throw new KeyNotFoundException();

                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException($"Слова \"{word}\" немає у словнику");
                if (vc[lang].Find(w => w.BWord == word).TWord.
                             Find(t => t == trans) == null)
                    throw new ArgumentException($"Переклад \"{trans}\" не міститься у слові \"{word}\"");

                vc[lang].Find( w => w.BWord == word).RemoveTranslete(trans);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, видалення не можливе");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Слова {word} не існує");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*---Редагують данні---*/
        /// <summary>
        /// Змінити назву словника
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="edition">Нова назва словника</param>
        public void EditLanguage(string lang, string edition)
        {
            try
            {
                if (vc[lang] == null) throw new KeyNotFoundException();
                if (edition == string.Empty) throw new ArgumentNullException();

                var temp = vc[lang];
                vc.Remove(lang);
                AddLanguage(edition);
                vc[edition] = temp;
                
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, редагування не можливе");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Нова назва словника не повинна бути порожньою");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Змінити словo
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        /// <param name="edition">Нове ім'я слова</param>
        public void EditWord(string lang, string word, string edition)
        {
            try
            {
                if (vc[lang] == null) throw new KeyNotFoundException();
                if (edition == string.Empty) 
                    throw new ArgumentNullException();
                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException();

                vc[lang].Find( w=> w.BWord == word).BWord = edition;

            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, редагування не можливе");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Нова назва словника не повинна бути порожньою");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Слова \"{word}\" немає у словнику");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Змінити переклад
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        /// <param name="trans">Переклад</param>
        /// <param name="edition">Нове ім'я перекладу</param>
        public void EditTranslete(string lang, string word, string trans, string edition)
        {
            try
            {
                if (vc[lang] == null) throw new KeyNotFoundException();
                if (edition == string.Empty)
                    throw new ArgumentNullException();
                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException($"Слова \"{word}\" немає у словнику");

                if (vc[lang].Find(w => w.BWord == word).TWord.Find(t => t == trans) == null)
                    throw new ArgumentException($"Переклад \"{trans}\" не міститься у слові \"{word}\"");

                RemoveTranslete(lang, word, trans);
                AddTranslete(lang, word, edition);

            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, редагування не можливе");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Нова назва словника не повинна бути порожньою");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*---Експорт слова з перекладом у файл.тхт---*/
        /// <summary>
        /// Шлях до директорії де зберігати експортованні данні
        /// </summary>
        private readonly string dirPathExp = "../../../../ExpotData";
        /// <summary>
        /// Експортувати слово і варіанти його перекладу до файлу
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        public void ExportData(string lang, string word)
        {
            Console.Clear();
            try
            {
                if (!Directory.Exists(dirPathExp))
                {
                    Directory.CreateDirectory(dirPathExp);
                }

                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException($"Слова \"{word}\" немає у словнику");

                string fName = '/' + lang + '_' + word + ".txt";

                using (FileStream fs = new FileStream(dirPathExp + fName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine($"Словник: {lang.Replace(" ", "")}\n");
                        sw.WriteLine("Слово\t Переклад");
                        sw.WriteLine(vc[lang].Find(w => w.BWord == word));
                    }
                }

                DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());
                string path = d.Parent.ToString();
                path = path.Remove(path.Length - 20) + @$"ExpotData\{lang}_{word}.txt";

                Console.WriteLine("Данні успішно експортовані у файл який знаходиться за адресою:");
                Console.WriteLine(path);
                Console.WriteLine("Для прожовження натисніть будь яку клавішу");
                Console.ReadKey();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, вивід не можливий");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*---Допоміжні методи (уникнення копіпасту коду)---*/
        /// <summary>
        /// Дає змогу вибирати строку зі списка
        /// </summary>
        /// <param name="list">Список із чого вибирати</param>
        /// <param name="forpage">Кількість результатів на сторінку</param>
        /// <returns></returns>
        private string SelectFromList(List<string> list, int forpage = 10)
        {
            try
            {
                Console.WriteLine("Виберіть із списку нижче");

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"{i} - {list[i]}");

                    if (i % forpage == 0 && i != 0)
                    {
                        Console.WriteLine("Для перегляду наступного списку натисніть будь яку клавішу");
                        Console.WriteLine("Для завершення перегляду натисніть клавішу ESC");
                        
                        ConsoleKeyInfo keyEsc = Console.ReadKey();
                        if (keyEsc.Key == ConsoleKey.Escape)
                        {
                           Console.WriteLine("_");
                           Console.WriteLine("Введіть номер");
                           break;
                        }
                        Console.Clear();
                        Console.WriteLine("Виберіть із списку нижче");
                    }

                    if (i + 1 == list.Count)
                    {
                        if (list.Count == forpage) break;
                        
                        Console.WriteLine("Кінець списку");
                        Console.WriteLine("Для завершення перегляду натисніть клавішу ESC");
                        Console.WriteLine("Для повторного перегляду натисніть будь яку іншу клавішу");

                        ConsoleKeyInfo keyEsc = Console.ReadKey();
                        if (keyEsc.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("_");
                            Console.WriteLine("Введіть номер");
                            break;
                        }
                        i = 0;

                        Console.Clear();
                        Console.WriteLine("Виберіть із списку нижче");
                    }
                }

                int selec = 0;
                while (true)
                {
                    selec = Int32.Parse(Console.ReadLine());
                    if (selec < list.Count && selec >= 0)
                        break;
                    else
                        Console.WriteLine("Не вірний вибор");
                }
                Console.WriteLine($"Ви обрали: {list[selec]}");
                Console.WriteLine("Натисніть будь яку клавішу щоб продовжити");
                Console.ReadKey();
                Console.Clear();
                return list[selec];
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
                return SelectFromList(list, forpage);
            }
        }
        /// <summary>
        /// Перевіряє наявності слова word у словнику
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Перевіряєме слово</param>
        /// <returns>Повертає true якщо так слово є</returns>
        private bool isExistWordInVoc(string lang, string word)
        {
            try
            {
                if (vc[lang].Find(w => w.BWord == word) != null) return true;
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return false;
            }
        }
        /*-----------------------Серіалізація--------------------------*/
        /// <summary>
        /// Шлях до директорії де зберігати серіалізовані данні
        /// </summary>
        private readonly string dirPath = "../../../../SaveFile";
        /// <summary>
        /// Назва файлу для збереження серіалізованих данних
        /// </summary>
        private readonly string fileName = "/SaveData.xml";
        /// <summary>
        /// Збереження(серіалізація) данних у ХМЛ файл за допомогою XmlSerializer
        /// </summary>
        public void SerializeData()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                XmlSerializer serializer = new(typeof(SerializableDictionary<string, List<Word>>));

                using (Stream stream = File.Create(dirPath + fileName))
                {
                    serializer.Serialize(stream, vc);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Завантаження серіалізованих данних з ХМЛ файлу
        /// </summary>
        public void DeserializeData()
        {
            try
            {
                if (!File.Exists(dirPath + fileName))
                    throw new FileNotFoundException($"Файл: {fileName} не створений.");

                XmlSerializer serializer = new(typeof(SerializableDictionary<string, List<Word>>));
                using (Stream stream = File.OpenRead(dirPath + fileName))
                {
                    vc = (SerializableDictionary<string, List<Word>>)serializer.Deserialize(stream);
                }

            }
            catch (FileNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.WriteLine("Збережених данних не знайдено. Зараз буде ствоненно перший словник");
                Console.WriteLine("Для продовження роботи натисніть будь яку клавішу");
                Console.ReadKey();
                CreateLanguage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
