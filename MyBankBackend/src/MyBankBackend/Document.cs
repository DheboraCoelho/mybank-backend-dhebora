public sealed record Document : IEquatable<Document>
{
    public string Value { get; }
    public DocumentType Type { get; }

    private Document(string value, DocumentType type)
    {
        Value = value;
        Type = type;
    }

    public static Document Create(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException("Invalid document value.");
        }

        var type = value.Length == 11 ? DocumentType.CPF : DocumentType.CNPJ;
        return new Document(value, type);
    }

    public static bool IsValid(string value)
    {
        return value.Length == 11 ? ValidateCpf(value) : ValidateCnpj(value);
    }

    private static bool ValidateCpf(string cpf)
    {
        // Add CPF validation logic here  
        return true;
    }

    private static bool ValidateCnpj(string cnpj)
    {
        // Add CNPJ validation logic here  
        return true;
    }

    public string Formatted()
    {
        // Add formatting logic here  
        return Value;
    }

    public override string ToString() => Value;
}
