namespace HouseOfMine.Domain.ValueObjects;

public sealed record NationalCode
{
    public string Value { get; }

    private NationalCode(string value) => Value = value;

    public static bool IsValid(string? code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != 10) return false;
        if (!code.All(char.IsAsciiDigit)) return false;
        if (code.Distinct().Count() == 1) return false; // مثل 1111111111

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (code[i] - '0') * (10 - i);

        int remainder = sum % 11;
        int check = code[9] - '0';
        return remainder < 2 ? check == remainder : check == 11 - remainder;
    }

    public static NationalCode Create(string code)
    {
        if (!IsValid(code))
            throw new ArgumentException("کد ملی نامعتبر است.", nameof(code));
        return new NationalCode(code);
    }

    public override string ToString() => Value;
}