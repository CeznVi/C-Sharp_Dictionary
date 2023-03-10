using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dictionary
{
    /// <summary>
    /// Контейнер для зберігання слова та перекладів до нього
    /// </summary>
    [Serializable]
    public class Word : IComparable<Word>
    {
        /*------------Властивості(змінні)--------------*/
        /// <summary>
        /// Властивість для зберігання слова
        /// </summary>
        public string BWord = "null";
        /// <summary>
        /// Список для зберігання перекладу слова
        /// </summary>
        public List<string> TWord = new();
        /*------------Конструктори--------------------*/
        /// <summary>
        /// Конструктор без параметрів
        /// </summary>
        public Word() { }
        /// <summary>
        /// Конструктор з одним параметром
        /// </summary>
        /// <param name="bWord">Слово оригінал</param>
        public Word(string bWord)
        {
            BWord = bWord;
        }
        /// <summary>
        /// Конструктор з двома параметрами
        /// </summary>
        /// <param name="bWord">Слово оригінал</param>
        /// <param name="tWord">Список перекладів</param>
        public Word(string bWord, List<string> tWord)
        {
            BWord = bWord;
            TWord = tWord;
        }
        /// <summary>
        /// Конструктор з двома параметрами
        /// </summary>
        /// <param name="bWord">Слово оригінал</param>
        /// <param name="translete">Слово переклад</param>
        public Word(string bWord, string translete)
        {
            BWord = bWord;
            TWord.Add(translete);
        }
        /*------------------Методи--------------------*/
        /// <summary>
        /// Додати переклад до базового слова
        /// </summary>
        /// <param name="word">Варіант перекладу</param>
        public void AddTranslete(string word) { TWord.Add(word); }
        /// <summary>
        /// Видалити переклад базавого за слова
        /// </summary>
        /// <param name="word">Варіант перекладу</param>
        public void RemoveTranslete(string word) 
        { 
            if(TWord.Count > 1)
                TWord.Remove(word); 
        }
        /// <summary>
        /// Геттер списку переладів
        /// </summary>
        /// <returns>Повртає список варіантів перекладу</returns>
        public List<string> GetTranslete() { return TWord; }
        /// <summary>
        /// Перевантаженя оператора ТуСтрінг
        /// </summary>
        /// <returns>Повертає строку зі словом та варіантами
        /// перекладу</returns>
        public override string ToString()
        {
            StringBuilder sb = new();

            foreach (string item in TWord)
            {
                if (item != TWord.Last())
                    sb.Append(item + ", ");
                else
                    sb.Append(item);
            }

            return BWord + " -- " + sb;
        }
        /// <summary>
        /// Метод для переміщення по данним ВОРД у контейнері
        /// </summary>
        /// <returns>Повертає елемент Word із контейнера</returns>
        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < TWord.Count; i++)
            {
                yield return TWord.ElementAt(i);
            }
        }
        /// <summary>
        /// Порівнює об'єкти типу Word
        /// </summary>
        /// <param name="w">Об'єкт типу Word</param>
        /// <returns>Повертає результат CompareTo</returns>
        public int CompareTo(Word w)
        {
            return (BWord.ToLower()).CompareTo(w.BWord.ToLower());
        }
    }
}
