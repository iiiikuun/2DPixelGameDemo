using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency;
    public int skillPoint;
    public int level;
    public int experience;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public bool HaveEnoughMoney(int price)
    {
        if (price > currency)
            return false;

        currency -= price;
        return true;
    }

    public bool HaveEnoughSkillPoint(int _skillPoint)
    {
        if (_skillPoint > skillPoint)
            return false;

        skillPoint -= _skillPoint;
        return true;
    }

    public void LoadData(GameData _data)
    {
        currency = _data.currency;
        skillPoint = _data.skillPoint;
        level = _data.level;
        experience = _data.experience;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = currency;
        _data.skillPoint = skillPoint;
        _data.level = level;
        _data.experience = experience;
    }
}
