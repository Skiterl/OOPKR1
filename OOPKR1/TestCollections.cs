using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPKR1
{
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    public class TestCollections<TKey, TValue>
    {
        private List<TKey> _keyList;
        private List<TValue> _valueList;
        private Dictionary<TKey, TValue> _keyDictionary;
        private Dictionary<string, TValue> _stringDictionary;
        private GenerateElement<TKey, TValue> _generateElement;

        private TestCollections(int length)
        {
            _keyList = new List<TKey>(length);
            _valueList = new List<TValue>(length);
            _keyDictionary = new Dictionary<TKey, TValue>(length);
            _stringDictionary = new Dictionary<string, TValue>(length);
        }

        public TestCollections(int length, GenerateElement<TKey, TValue> generateElement):this(length)
        {
            _generateElement = generateElement;
            GenerateCollection(length);
        }

        public void GenerateCollection(int length)
        {
            for (int i = 0; i < length; i++)
            {
                var element = _generateElement(i);
                _keyList.Add(element.Key);
                _valueList.Add(element.Value);
                _keyDictionary.Add(element.Key, element.Value);
                _stringDictionary.Add(i.ToString(), element.Value);
            }
        }

        public void MeasureSearchTimes()
        {
            var first = _generateElement(0);
            var middle = _generateElement(_keyList.Count / 2);
            var last = _generateElement(_keyList.Count);
            var noExist = _generateElement(_keyList.Count + 1);

            Console.WriteLine("First element");
            MeasureElementSearchTime(first);
            Console.WriteLine("\nMiddle element");
            MeasureElementSearchTime(middle);
            Console.WriteLine("\nLast element");
            MeasureElementSearchTime(last);
            Console.WriteLine("\nNo exist element");
            MeasureElementSearchTime(noExist);
        }


        private void MeasureElementSearchTime(KeyValuePair<TKey, TValue> element)
        {
            var sw = new Stopwatch();
            Console.WriteLine("KeyList");
            sw.Start();
            _keyList.Contains(element.Key);
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalNanoseconds} ns");

            Console.WriteLine("ValueList");
            sw.Restart();
            _valueList.Contains(element.Value);
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalNanoseconds} ns");

            Console.WriteLine("KeyDictionary (ContainsKey)");
            sw.Restart();
            _keyDictionary.ContainsKey(element.Key);
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalNanoseconds} ns");

            Console.WriteLine("StringDictionary");
            sw.Restart();
            _stringDictionary.ContainsKey(element.Key.ToString());
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalNanoseconds} ns");

            Console.WriteLine("KeyDictionary (ContainsValue)");
            sw.Restart();
            _keyDictionary.ContainsValue(element.Value);
            sw.Stop();
            Console.WriteLine($"{sw.Elapsed.TotalNanoseconds} ns");
        }
    }
}
