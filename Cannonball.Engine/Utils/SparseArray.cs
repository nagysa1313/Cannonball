using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cannonball.Engine.Utils
{
    public class SparseArray<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> dictionary;
        private TValue defaultValue;

        public SparseArray()
        {
            this.defaultValue = default(TValue);
            dictionary = new Dictionary<TKey, TValue>();
        }
        public SparseArray(TValue defaultValue)
        {
            this.defaultValue = defaultValue;
            dictionary = new Dictionary<TKey, TValue>();
        }

        public TValue this[TKey index]
        {
            get
            {
                TValue retVal = defaultValue;
                dictionary.TryGetValue(index, out retVal);
                return retVal;
            }
            set 
            {
                if (dictionary.ContainsKey(index))
                {
                    if (value.Equals(defaultValue))
                        dictionary.Remove(index);
                    else dictionary[index] = value;
                }
                else
                {
                    dictionary.Add(index, value);
                }
            }
        }

        public bool IndexExists(TKey index)
        {
            return !this[index].Equals(defaultValue);
        }

        #region IEnumerable<KeyValuePair<TKey, TValue>> members
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
        #endregion
    }

    public class SparseArray<TValue> : SparseArray<int, TValue>
    {

    }
}