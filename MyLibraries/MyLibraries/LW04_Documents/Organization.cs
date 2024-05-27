using Newtonsoft.Json;

namespace OOP.LW04_Documents
{
    public class Organization
    {
        public Organization(Organization org)
        {
            Name = new(org.Name);
            Address = org.Address is null ? null : new(org.Address);
            Description = org.Description is null ? null : new(org.Description);
        }

        [JsonConstructor]
        public Organization(string name, string? address = null, string? description = null)
        {
            Name = name;
            Address = address;
            Description = description;
        }

        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }

        public override string ToString()
        {
            return $"Название организации: {Name}\n" +
                   $"Адрес:                {Address ?? "-"}\n" +
                   $"Описание:             {Description ?? "-"}\n";
        }
    }
}