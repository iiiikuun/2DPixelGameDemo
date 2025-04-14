using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISaveManager
{
    private UI_Menu ui=>GetComponentInParent<UI_Menu>();
    private Image skillImage=>GetComponent<Image>();

    [SerializeField] private string skillName;
    [TextArea] 
    [SerializeField] private string skillDescription;
    [SerializeField] private int skillPoint;

    public bool unlocked;

    [SerializeField] private UI_SkillSlot[] shouldBeUnlocked; 
    [SerializeField] private UI_SkillSlot[] shouldBeLocked;


    private void OnValidate()
    {
        gameObject.name = "Skill - " + skillName;
    }

    private void Awake()
    {
        skillImage.color = new Color(1, 1, 1, 0.3f);
    }
    private void Start()
    {
        if (unlocked)
            skillImage.color = Color.white;
    }

    public void UnlockedSkillSlot()
    {
        if (unlocked)
            return;

        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (!shouldBeUnlocked[i].unlocked)
                return;
        }

        for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked)
                return;
        }

        if(!PlayerManager.instance.HaveEnoughSkillPoint(skillPoint))
            return;

        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(skillName, skillDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
            _data.skillTree.Add(skillName, unlocked);
    }
}
