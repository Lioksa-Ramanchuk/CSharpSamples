#pragma warning disable S2328 // "GetHashCode" should not reference mutable fields

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP.LW03_List
{
    using LW07_IInteractive;

    [JsonObject(MemberSerialization.Fields)]
    public class List<T> : IInteractive<T>, IEnumerable<T> where T : IEquatable<T>
    {
        T[] _arr;

        public List(params T[]? arr) : this(arr ?? Array.Empty<T>(), null, null) { }
        List(T[] arr, string? ownerName, string? ownerOrganization)
        {
            _arr = arr;
            Owner.Name = ownerName;
            Owner.Organization = ownerOrganization;
        }

        void IInteractive<T>.Add(T item) => AddLast(item);
        void IInteractive<T>.Delete(T item) => _arr = _arr.Where(i => !i.Equals(item)).ToArray();
        IEnumerable<T> IInteractive<T>.Where(Predicate<T> predicate) => _arr.Where(i => predicate(i));

        IEnumerator IEnumerable.GetEnumerator() => _arr.GetEnumerator();
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_arr).GetEnumerator();

        public OwnerInfo Owner { get; set; } = new OwnerInfo();
        public Date CreationDate { get; } = new Date();

        public T this[int i]
        {
            set
            {
                if (i < 0 || i >= _arr.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(i));
                }

                _arr[i] = value;
            }

            get
            {
                if (i < 0 || i >= _arr.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(i));
                }

                return _arr[i];
            }
        }
        public static List<T> operator +(T item, List<T> list)
        {
            list.AddFirst(item);
            return list;
        }
        public static List<T> operator --(List<T> list)
        {
            list.PopFirst();
            return list;
        }
        public static bool operator ==(List<T> list1, List<T> list2)
        {
            return list1._arr.SequenceEqual(list2._arr);
        }
        public static bool operator !=(List<T> list1, List<T> list2)
        {
            return !list1._arr.SequenceEqual(list2._arr);
        }
        public static List<T> operator *(List<T> list1, List<T> list2)
        {
            List<T> result = new()
            {
                _arr = new T[list1._arr.Length + list2._arr.Length]
            };

            list1._arr.CopyTo(result._arr, 0);
            list2._arr.CopyTo(result._arr, list1._arr.Length);
            result.Owner.Name = $"{list1.Owner.Name}, {list2.Owner.Name}";

            return result;
        }

        public void AddFirst(T item)
        {
            T[] newArr = new T[_arr.Length + 1];
            newArr[0] = item;
            _arr.CopyTo(newArr, 1);
            _arr = newArr;
        }
        public void AddLast(T item)
        {
            T[] newArr = new T[_arr.Length + 1];
            _arr.CopyTo(newArr, 0);
            newArr[^1] = item;
            _arr = newArr;
        }
        public T PopFirst()
        {
            if (_arr.Length <= 0)
            {
                throw new InvalidOperationException();
            }

            T result = _arr[0];

            T[] newArr = new T[_arr.Length - 1];
            Array.Copy(_arr, 1, newArr, 0, newArr.Length);
            _arr = newArr;

            return result;
        }
        public T PopLast()
        {
            if (_arr.Length <= 0)
            {
                throw new InvalidOperationException();
            }

            T result = _arr[^1];

            T[] newArr = new T[_arr.Length - 1];
            Array.Copy(_arr, newArr, newArr.Length);
            _arr = newArr;

            return result;
        }

        public void Serialize(string path)
        {
            StreamWriter? sw = null;
            try
            {
                sw = File.CreateText(path);
                sw.WriteLine(JsonConvert.SerializeObject(this, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented }));
            }
            catch (Exception e)
            {
                Console.WriteLine($"При сериализации коллекции возникла ошибка:\n{e}");
            }
            finally
            {
                sw?.Close();
            }
        }
        public void Deserialize(string path)
        {
            StreamReader? sr = null;
            try
            {
                sr = File.OpenText(path);
                List<T> newList = JsonConvert.DeserializeObject<List<T>>(
                    sr.ReadToEnd(),
                    new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented })!;
                _arr = newList._arr;
            }
            catch (Exception e)
            {
                Console.WriteLine($"При сериализации коллекции возникла ошибка:\n{e}");
            }
            finally
            {
                sr?.Close();
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is List<T> list &&
                   Owner.Equals(list.Owner) &&
                   CreationDate.Equals(list.CreationDate) &&
                   _arr.SequenceEqual(list._arr);
        }
        public override int GetHashCode()
        {
            return _arr.Aggregate(0, (total, nextElem) => HashCode.Combine(total, nextElem));
        }
        public override string ToString()
        {
            return $"[{string.Join(", ", _arr)}]";
        }
    }
}