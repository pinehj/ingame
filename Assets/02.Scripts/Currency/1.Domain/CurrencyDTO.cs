using UnityEngine;

// 계층 간 데이터 전송을 위해 도메인 모델 대신 사용하는 객체
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
