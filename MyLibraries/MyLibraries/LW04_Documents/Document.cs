using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace OOP.LW04_Documents
{
    [Serializable]
    [DataContract(Namespace = "")]
    public abstract partial class Document : BaseClone, ISignable
    {
        [DataMember(Name = "Date")]
        readonly Date _date = new(1, 1, 0);

        public void Sign()
        {
            IsSigned = true;
        }

        public Date Date => _date;
        [DataMember]
        [JsonProperty]
        public bool IsOriginal { get; protected set; } = true;

        [DataMember]
        [JsonProperty]
        public bool IsSigned { get; protected set; } = false;
        [DataMember]
        public uint Number { get; set; } = 0;

        public virtual string GetTypeName()
        {
            return "Документ";
        }

        public override string ToString()
        {
            return $"=== {GetTypeName()} ===\n" +
                   $"Дата:            {Date}\n" +
                   $"Номер документа: {Number}\n" +
                   $"Оригинал:        {(IsOriginal ? "Да" : "Нет")}\n" +
                   $"Подпись:         {(IsSigned ? "Есть" : "Нет")}\n";
        }
    }
}