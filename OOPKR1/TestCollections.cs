using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPKR1
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    public class TestCollections<TKey, TValue>
    {
        private List<TKey> _keyList;
        private List<TValue> _valueList;
        private Dictionary<TKey, TValue> _keyDictionary;
        private Dictionary<string, TValue> _stringDictionary;
        private GenerateElement<TKey, TValue> _element;

        public TestCollections(int length)
        {
            _keyList = new List<TKey>(length);
            _valueList = new List<TValue>(length);
            _keyDictionary = new Dictionary<TKey, TValue>(length);
            _stringDictionary = new Dictionary<string, TValue>(length);
        }

        /*public TestCollections(int length, GenerateElement<TKey, TValue> generateElement)
        {

        }

        public KeyValuePair<TKey, TValue> GenerateCollection(int length)
        {

        }*/
    }
}
