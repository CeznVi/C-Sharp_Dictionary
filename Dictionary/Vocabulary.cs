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
        public SerializableDictionary<string, List<Word>> vc = new();

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
                    throw new FileNotFoundException($"Файл: {fileName} не створений. Робота додатка не можлива!");

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
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /*---------------------------Методи----------------------------*/
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        /*---Додають данні---*/
        /// <summary>
        /// Додати Словник (Мова - мова перекладу)
        /// </summary>
        /// <param name="language">Мова - мова перекладу</param>
        public void AddLanguage(string lang)
        {
            try
            {
                if (lang != null)
                    vc.Add(lang, new List<Word>());
            }
            catch(System.ArgumentException)
            {
                Console.WriteLine($"Такий словник: \"{lang}\" вже існує");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Додати у відповідник словник слово і слово переклад
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
        /// Додати переклад до словника[слово]+переклад
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
                    throw new ArgumentNullException($"Нова назва словника не повинна бути порожньою");
                if (vc[lang].Find(w => w.BWord == word) == null)
                    throw new ArgumentException();

                //vc[lang].[word] = edition;

            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника \"{lang}\" не існує, редагування не можливе");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
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



        ///TO DO
        /*---Редагують данні---(словник, слово, переклад)*/

        /*---Шукають данні(слово у словнику)---*/

        /*---Експорт слова з перекладом у файл.тхт---*/

        /*---Сутність меню + створити класи та наслідників---*/
    }
}
