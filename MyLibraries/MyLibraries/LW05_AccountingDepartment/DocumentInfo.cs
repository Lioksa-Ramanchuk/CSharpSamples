namespace OOP.LW04_Documents
{
    public enum DocumentType : byte
    {
        None,
        PaymentReceipt,
        PackingList,
        Cheque,
    }

    public struct DocumentInfo
    {
        public DocumentInfo(Document doc, string? description = null)
        {
            Doc = doc;
            Description = description;
        }

        public Document? Doc { get; set; } = null;
        public string? Description { get; set; } = null;
        public DocumentType Type
        {
            get => Doc switch
            {
                PaymentReceipt => DocumentType.PaymentReceipt,
                PackingList => DocumentType.PackingList,
                Cheque => DocumentType.Cheque,
                _ => DocumentType.None,
            };
        }
    }
}