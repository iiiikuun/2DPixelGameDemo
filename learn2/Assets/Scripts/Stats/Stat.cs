using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private int baseValue;
    [SerializeField] private List<int> modifiers = new List<int>();

    public StatType statType;

    public Stat(StatType _statType,int _baseValue)
    {
        statType = _statType;
        baseValue = _baseValue;
    }

    public Stat(StatType _statType)
    {
        statType = _statType;
    }

    public int GetValue()
    {
        int finalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void AddModifier(int _modifier)
    {
        if (_modifier != 0)
            modifiers.Add(_modifier);
    }

    public void RemoveModifier(int _modifier)
    {
        if (_modifier != 0)
            modifiers.Remove(_modifier);
    }

    public void SetBaseValue(int _baseValue)
    {
        baseValue = _baseValue;
    }
}
