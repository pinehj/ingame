using UnityEngine;

// ���� �� ������ ������ ���� ������ �� ��� ����ϴ� ��ü
public class CurrencyDTO : MonoBehaviour
{
    public readonly ECurrencyType Type;
    public readonly int Value;

    public CurrencyDTO(Currency currency)
    {
        Type = currency.Type;
        Value = currency.Value;
    }
    public CurrencyDTO(ECurrencyType type, int value)
    {
        Type = type;
        Value = value;
    }
    public bool HasEnough(int value)
    {
        return Value >= value;
    }
}
