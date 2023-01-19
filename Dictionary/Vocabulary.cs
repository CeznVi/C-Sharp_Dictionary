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
        /// <see cref="https://www.cyberforum.ru/csharp-net/thread1429593.html"/>
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









        /// <summary>
        /// Додати Словник (Мова - мова перекладу)
        /// </summary>
        /// <param name="language">Мова - мова перекладу</param>
        public void AddLanguage(string language) 
        { 
            vc.Add(language, new List<Word>());
        }


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
        /// Повернути список переводів слова
        /// </summary>
        /// <param name="lang">Словник(мова - мова переклад)</param>
        /// <param name="word">Слово</param>
        /// <returns></returns>
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
            }
            
            return WordList;
        }
        ///ADD try CATCH TKEY == неіснує

        /// <summary>
        /// Додати у відповідник словник слово і слово переклад
        /// </summary>
        /// <param name="lang">Словник(мова - мова переклад)</param>
        /// <param name="w">Контейнер слова</param>
        public void AddWord(string lang, Word w) 
        {
            if(vc.ContainsKey(lang))
                if(vc[lang].Contains(w) == false)
                    vc[lang].Add(w);
        }
        /// <summary>
        /// Показати всі слова у вибраному словнику
        /// </summary>
        /// <param name="lang">Мова - мова переклад</param>
        public void ShowAllWord(string lang) 
        {
            foreach (Word item in vc[lang])
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// Показати переклад слова у відповідному словнику
        /// </summary>
        /// <param name="lang">Словник</param>
        /// <param name="word">Слово</param>
        public void ShowWord(string lang, string word) 
        {
            foreach (Word item in vc[lang])
            {
               if(item.BWord == word)
                    Console.WriteLine(item);
            }
        }

        public void AddTranslete(string lang, string word, string trans)
        {
            foreach (Word item in vc[lang])
            {
                if (item.BWord == word)
                    item.AddTranslete(trans);
            }
        }
        public void RemoveTranslete(string lang, string word, string trans)
        {
            foreach (Word item in vc[lang])
            {
                if (item.BWord == word)
                    item.RemoveTranslete(trans);
            }
        }
        
        public void RemoveWord(string lang, string word)
        {
            try
            {
                if(vc[lang].Find(w => w.BWord == word) == null)
                    Console.WriteLine($"Слова {word} не існує");
                else
                _ = vc[lang].Remove(vc[lang].Find(w => w.BWord == word));
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Слова {word} не існує");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Словника {lang} не існує");
            }
        }
    }
}
