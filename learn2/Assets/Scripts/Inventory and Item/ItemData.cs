using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ItemType
{
    材料,
    装备
}

public enum QualityType
{
    普通,
    精良,
    史诗,
    传说,
    神话
}

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public QualityType qualityType;
    public string itemName;
    public Sprite icon;
    public string itemDescription;
    public string itemID;

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}

