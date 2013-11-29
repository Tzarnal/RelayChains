using System;
using System.Collections.Generic;
using System.Linq;

namespace RelayChains
{    
    class ChainKey
    {
        
        private string[] _entries;

        public string this[int index]    // Indexer declaration
        {
            get { return _entries[index]; }
        }

        public string FirstWord
        {
            get { return _entries[0]; }
        }

        public ChainKey(int size)
        {
            _entries = new string[size];
        }

        public ChainKey(IEnumerable<string> content)
        {
            SetContent(content);
        }

        public void SetContent(IEnumerable<string> content)        
        {
            _entries = content.ToArray();            
        }
        
        new public string ToString()
        {
            return String.Join("", _entries);
        }
    }
}
