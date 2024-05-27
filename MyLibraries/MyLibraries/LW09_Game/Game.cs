using System.Collections;
using System.Collections.Generic;

namespace OOP.LW09_Game
{
    public class Game : IEnumerable<string>
    {
        readonly string[] _players;

        public Game(string name, params string[] players)
        {
            Name = name;
            _players = players;
        }
        public IEnumerator GetEnumerator() => ((IEnumerable<string>)_players).GetEnumerator();
        IEnumerator<string> IEnumerable<string>.GetEnumerator() => (IEnumerator<string>)GetEnumerator();

        public string Name { get; init; }

        public override string ToString() => $"<{Name}> Игроки: {string.Join(", ", _players)}";
    }
}