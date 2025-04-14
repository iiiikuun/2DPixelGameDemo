using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropProbility
{
    [SerializeField] private int defaultProbability;

    [SerializeField] private ItemData data;

    [SerializeField] private int IncreaseRate;

    public int GetDropProbility(int level)
    {
        return defaultProbability + (level - 1) * IncreaseRate;
    }

    public ItemData GetItemData()
    {
        return data;
    }
}
