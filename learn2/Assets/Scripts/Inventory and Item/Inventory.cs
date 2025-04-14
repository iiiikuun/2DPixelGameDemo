using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;

    [SerializeField] private Vector2Int materialGridSize;
    private Transform[,] materialSlots;
    [SerializeField] private Item_Material[,] materialInventory;

    [SerializeField] private Vector2Int equipmentGridSize;
    private Transform[,] equipmentSlots;
    [SerializeField] private Item_Equipment[,] equipmentInventory;

    [SerializeField] private Transform[] equippedSlots = new Transform[6];
    [SerializeField] private Item_Equipment[] wornEquipment = new Item_Equipment[6];

    [Header("仓库UI")]
    [SerializeField] private GameObject UI_ItemSlotPrefab;
    [SerializeField] private GameObject UI_MaterialPrefab;
    [SerializeField] private GameObject UI_EquipmentPrefab;

    [SerializeField] private Transform materialSlotParent;
    [SerializeField] private Transform equipmentSlotParent;

    [Header("加载物品数据")]
    private Dictionary<string, ItemData_Material> MaterialDataCache;
    private Dictionary<string, ItemData_Equipment> EquipmentDataCache;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        Initialization();
    }

    private void Initialization()
    {
        materialInventory = new Item_Material[materialGridSize.x, materialGridSize.y];
        materialSlots = new Transform[materialGridSize.x, materialGridSize.y];
        equipmentInventory = new Item_Equipment[equipmentGridSize.x, equipmentGridSize.y];
        equipmentSlots = new Transform[equipmentGridSize.x, equipmentGridSize.y];

        for (int x = 0; x < materialGridSize.x; x++)
        {
            for (int y = 0; y < materialGridSize.y; y++)
            {
                GameObject item = Instantiate(UI_ItemSlotPrefab);
                item.transform.SetParent(materialSlotParent, false);
                item.GetComponent<UI_ItemSlot>().SetUpSlot(ItemType.材料, new Vector2Int(x, y));
                materialSlots[x, y] = item.transform;
            }
        }

        for (int x = 0; x < equipmentGridSize.x; x++)
        {
            for (int y = 0; y < equipmentGridSize.y; y++)
            {
                GameObject item = Instantiate(UI_ItemSlotPrefab);
                item.transform.SetParent(equipmentSlotParent, false);
                item.GetComponent<UI_ItemSlot>().SetUpSlot(ItemType.装备, new Vector2Int(x, y));
                equipmentSlots[x, y] = item.transform;
            }
        }
    }

    private void Start()
    {

    }

    #region 添加物品
    public void AddMaterial(Item_Material newMaterial)
    {
        for (int x = 0; x < materialGridSize.x; x++)
        {
            for (int y = 0; y < materialGridSize.y; y++)
            {
                if (materialInventory[x, y] != null && materialInventory[x, y].data == newMaterial.data)
                {
                    materialInventory[x, y].AddStack(newMaterial.stackSize);
                    return;
                }
            }
        }

        for (int x = 0; x < materialGridSize.x; x++)
        {
            for (int y = 0; y < materialGridSize.y; y++)
            {
                if (materialInventory[x, y] == null)
                {
                    CreateNewMaterial(newMaterial, x, y);

                    return;
                }
            }
        }
    }

    public void AddEquipment(Item_Equipment newEquipment)
    {
        for (int x = 0; x < equipmentGridSize.x; x++)
        {
            for (int y = 0; y < equipmentGridSize.y; y++)
            {
                if (equipmentInventory[x, y] == null)
                {
                    CreateNewEquipment(newEquipment, x, y);

                    return;
                }
            }
        }
    }

    private void CreateNewMaterial(Item_Material newMaterial, int x, int y)
    {
        GameObject item = Instantiate(UI_MaterialPrefab);
        item.GetComponent<UI_Material>().SetUpItem(newMaterial, new Vector2Int(x, y));
        newMaterial.ui_Material = item.GetComponent<UI_Material>();

        AddMaterialInventory(newMaterial, x, y);
    }

    private void CreateNewEquipment(Item_Equipment newEquipment, int x, int y)
    {
        GameObject item = Instantiate(UI_EquipmentPrefab);
        item.GetComponent<UI_Equipment>().SetUpItem(newEquipment, new Vector2Int(x, y));
        newEquipment.ui_Equipment = item.GetComponent<UI_Equipment>();

        AddEquipmentInventory(newEquipment, x, y);
    }

    private void AddMaterialInventory(Item_Material newMaterial, int x, int y)
    {
        materialInventory[x, y] = newMaterial;
        newMaterial.ui_Material.gameObject.transform.SetParent(materialSlots[x, y], false);
        newMaterial.ui_Material.itemPosition = new Vector2Int(x, y);
    }

    private void AddEquipmentInventory(Item_Equipment newEquipment, int x, int y)
    {
        equipmentInventory[x, y] = newEquipment;
        newEquipment.ui_Equipment.gameObject.transform.SetParent(equipmentSlots[x, y], false);
        newEquipment.ui_Equipment.itemPosition = new Vector2Int(x, y);
    }

    private void Equip(Item_Equipment newEquipment, int equipmentIndex)
    {
        wornEquipment[equipmentIndex] = newEquipment;
        newEquipment.ui_Equipment.gameObject.transform.SetParent(equippedSlots[equipmentIndex], false);
        newEquipment.ui_Equipment.itemPosition = new Vector2Int(-1, -1);

        newEquipment.AddModifiers();
    }
    #endregion
    #region 移除物品
    public void RemoveMaterial(Item_Material material)
    {
        for (int x = 0; x < materialGridSize.x; x++)
        {
            for (int y = 0; y < materialGridSize.y; y++)
            {
                if (materialInventory[x, y] != null && materialInventory[x, y].data == material.data)
                {
                    if (materialInventory[x, y].stackSize > material.stackSize)
                    {
                        materialInventory[x, y].RemoveStack(material.stackSize);

                        return;
                    }
                    else if (materialInventory[x, y].stackSize == material.stackSize)
                    {
                        materialInventory[x, y].DeleteUI();
                        RemoveMaterialInventory(x, y);

                        return;
                    }
                }
            }
        }
    }

    public void RemoveEquipment(Item_Equipment equipment)
    {
        for (int x = 0; x < equipmentGridSize.x; x++)
        {
            for (int y = 0; y < equipmentGridSize.y; y++)
            {
                if (equipmentInventory[x, y] == equipment)
                {
                    equipmentInventory[x, y].DeleteUI();
                    RemoveEquipmentInventory(x, y);

                    return;
                }
            }
        }
    }

    private void RemoveMaterialInventory(int x, int y)
    {
        materialInventory[x, y] = null;
    }

    private void RemoveEquipmentInventory(int x, int y)
    {
        equipmentInventory[x, y] = null;
    }

    private void Unequip(Item_Equipment equipment)
    {
        int index = CheckEquipmentType(equipment);
        wornEquipment[index] = null;

        equipment.RemoveModifiers();
    }
    #endregion
    #region 交换物品位置
    public void ChangeEquipment(Item_Equipment usedEquipment, Item_Equipment newEquipment, Vector2Int position)
    {
        if (usedEquipment != null && newEquipment != null)
        {
            Unequip(usedEquipment);
            AddEquipmentInventory(usedEquipment, position.x, position.y);
            Equip(newEquipment, CheckEquipmentType(newEquipment));
        }
        else if (newEquipment != null)
        {
            RemoveEquipmentInventory(position.x, position.y);
            Equip(newEquipment, CheckEquipmentType(newEquipment));
        }
        else if (usedEquipment != null)
        {
            Unequip(usedEquipment);
            AddEquipmentInventory(usedEquipment, position.x, position.y);
        }
    }

    private int CheckEquipmentType(Item_Equipment newEquipment)
    {
        switch (newEquipment.data.equipmentType)
        {
            case EquipmentType.武器:
                return 0;

            case EquipmentType.头盔:
                return 1;

            case EquipmentType.战甲:
                return 2;

            case EquipmentType.战靴:
                return 3;

            case EquipmentType.饰品:
                return 4;

            case EquipmentType.消耗品:
                return 5;

            default:
                return 6;
        }
    }

    public void ExchangeMaterialPosition(Vector2Int startPosition, Vector2Int endPosition)
    {
        Item_Material usedMaterial = materialInventory[endPosition.x, endPosition.y];
        Item_Material newMaterial = materialInventory[startPosition.x, startPosition.y];

        AddMaterialInventory(newMaterial, endPosition.x, endPosition.y);
        if (usedMaterial != null)
            AddMaterialInventory(usedMaterial, startPosition.x, startPosition.y);
        else
            materialInventory[startPosition.x, startPosition.y] = null;
    }

    public void ExchangeEquipmentPosition(Vector2Int startPosition, Vector2Int endPosition)
    {
        Item_Equipment usedEquipment = equipmentInventory[endPosition.x, endPosition.y];
        Item_Equipment newEquipment = equipmentInventory[startPosition.x, startPosition.y];

        AddEquipmentInventory(newEquipment, endPosition.x, endPosition.y);
        if (usedEquipment != null)
            AddEquipmentInventory(usedEquipment, startPosition.x, startPosition.y);
        else
            equipmentInventory[startPosition.x, startPosition.y] = null;
    }
    #endregion
    #region 检查槽位是否已满
    public bool IsEquipmentInventoryFull()
    {
        for (int i = 0; i < equipmentInventory.GetLength(0); i++)
        {
            for (int j = 0; j < equipmentInventory.GetLength(1); j++)
            {
                if (equipmentInventory[i, j] == null)
                {
                    return false;
                }
            }
        }
        Debug.Log("装备槽已满");
        return true;
    }

    public bool IsMaterialInventoryFull(ItemData_Material data)
    {
        for (int i = 0; i < materialInventory.GetLength(0); i++)
        {
            for (int j = 0; j < materialInventory.GetLength(1); j++)
            {
                if (materialInventory[i, j] == null || data == materialInventory[i, j].data)
                {
                    return false;
                }
            }
        }
        Debug.Log("材料槽已满");
        return true;
    }
    #endregion
    #region 保存数据
    public void LoadData(GameData _data)
    {
        if (MaterialDataCache == null)
            MaterialDataCache = Resources.LoadAll<ItemData_Material>("Item Data/Material").ToDictionary(data => data.itemID, data => data);

        if (EquipmentDataCache == null)
            EquipmentDataCache = Resources.LoadAll<ItemData_Equipment>("Item Data/Equipment").ToDictionary(data => data.itemID, data => data);

        if (_data.materialInventory.Count > 0)
        {
            for (int x = 0; x < materialGridSize.x; x++)
            {
                for (int y = 0; y < materialGridSize.y; y++)
                {
                    MaterialSaveData saveData = _data.materialInventory[y + x * materialGridSize.y];
                    Item_Material material;

                    if (saveData == null)
                        materialInventory[x, y] = null;
                    else
                    {
                        if (MaterialDataCache.TryGetValue(saveData.itemID, out var data))
                        {
                            material = new Item_Material(data, saveData.stackSize);
                            CreateNewMaterial(material, x, y);
                        }
                    }
                }
            }
        }

        if (_data.equipmentInventory.Count > 0)
        {
            for (int x = 0; x < equipmentGridSize.x; x++)
            {
                for (int y = 0; y < equipmentGridSize.y; y++)
                {
                    EquipmentSaveData saveData = _data.equipmentInventory[y + x * equipmentGridSize.y];
                    Item_Equipment equipment;

                    if (saveData == null)
                        equipmentInventory[x, y] = null;
                    else
                    {
                        if (EquipmentDataCache.TryGetValue(saveData.itemID, out var data))
                        {
                            equipment = new Item_Equipment(data);
                            CreateNewEquipment(equipment, x, y);
                        }
                    }
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.materialInventory.Clear();

        for (int x = 0; x < materialGridSize.x; x++)
        {
            for (int y = 0; y < materialGridSize.y; y++)
            {
                if (materialInventory[x, y] == null)
                    _data.materialInventory.Add(new MaterialSaveData());
                else
                    _data.materialInventory.Add(new MaterialSaveData(materialInventory[x, y]));
            }
        }

        _data.equipmentInventory.Clear();

        for (int x = 0; x < equipmentGridSize.x; x++)
        {
            for (int y = 0; y < equipmentGridSize.y; y++)
            {
                if (equipmentInventory[x, y] == null)
                    _data.equipmentInventory.Add(new EquipmentSaveData());
                else
                    _data.equipmentInventory.Add(new EquipmentSaveData(equipmentInventory[x, y]));
            }
        }
    }
    #endregion
}
