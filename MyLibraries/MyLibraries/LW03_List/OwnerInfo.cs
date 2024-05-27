#pragma warning disable S2328 // "GetHashCode" should not reference mutable fields

using Newtonsoft.Json;
using System;

namespace OOP.LW03_List
{
    [JsonObject(MemberSerialization.Fields)]
    public class OwnerInfo
    {
        uint _id = 0;
        string? _name = null;
        string? _organization = null;

        public OwnerInfo() { }
        public OwnerInfo(string? name, string? organization)
        {
            _name = name;
            _organization = organization;
            _id = (uint)GetHashCode();
        }

        public uint Id { get => _id; }
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                _id = (uint)GetHashCode();
            }
        }
        public string? Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                _id = (uint)GetHashCode();
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is OwnerInfo oi && _id == oi._id;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _organization);
        }
        public override string ToString()
        {
            return $"ID: {_id}, создатель: {_name ?? "-"}, организация: {_organization ?? "-"}";
        }
    }
}