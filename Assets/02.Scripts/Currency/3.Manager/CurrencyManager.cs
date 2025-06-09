using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyManager : BehaviourSingleton<CurrencyManager>
{
    // 包府磊
    private Dictionary<ECurrencyType, Currency> _currencies;
    private CurrencyRepository _repository;
    public event Action OnDataChanged;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        // 积己
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        if (loadedCurrencies == null)
        {
            for (int i = 0; i < (int)ECurrencyType.Count; i++)
            {
                ECurrencyType type = (ECurrencyType)i;
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }

        }

        else
        {
            foreach (var data in loadedCurrencies)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(currency.Type, currency);
            }
        }


    }

    private List<CurrencyDTO> ToDTOList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }
    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }
    public bool Add(ECurrencyType type, int value)
    {
        try
        {
            _currencies[type].Add(value);
        }
        catch(Exception e) 
        {
            return false;
        }
        Debug.Log($"{type}: {_currencies[type].Value}");

        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();

        return true;
    }
    public bool Subtract(ECurrencyType type, int value)
    {
        try
        {
            _currencies[type].Subtract(value);
        }
        catch(Exception e)
        {
            Debug.Log("ddfd");
            return false;
        }
        Debug.Log($"{type}: {_currencies[type].Value}");
        
        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();

        return true;
    }
}
