using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OOP.LW05_AccountingDepartment
{
    using LW04_Documents;

    public class Archive : ICollection<Document>
    {
        readonly List<Document> _documents;

        [JsonConstructor]
        public Archive(List<Document> documents)
        {
            _documents = new(documents);
        }

        public Archive(params Document[] documents)
        {
            _documents = documents is null ? new() : new(documents);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Document> GetEnumerator() => _documents.GetEnumerator();

        int ICollection<Document>.Count => _documents.Count;
        bool ICollection<Document>.IsReadOnly => ((ICollection<Document>)_documents).IsReadOnly;
        void ICollection<Document>.Add(Document item) => _documents.Add(item);
        bool ICollection<Document>.Contains(Document item) =>_documents.Contains(item);
        void ICollection<Document>.CopyTo(Document[] array, int arrayIndex) => _documents.CopyTo(array, arrayIndex);

        public static void ShowDocuments(params Document[] docs)
        {
            Array.ForEach(docs, Console.WriteLine);
        }
        public void Add(params Document[] docs)
        {
            Array.ForEach(docs, _documents.Add);
        }
        public bool Remove(Document doc)
        {
            return _documents.Remove(doc);
        }
        public void Clear()
        {
            _documents.Clear();
        }
        public void Show()
        {
            if (_documents.Count == 0)
            {
                Console.WriteLine("Архив пуст.");
                return;
            }

            Console.WriteLine("Содержимое архива:\n");

            foreach (Document doc in _documents)
            {
                Console.WriteLine(doc);
            }
        }
    }
}