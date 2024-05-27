using System;
using System.Collections.Generic;

namespace OOP.LW07_IInteractive
{
    public interface IInteractive<T> where T : IEquatable<T>
    {
        void Add(T item);
        void Delete(T item);
        IEnumerable<T> Where(Predicate<T> predicate);
    }
}