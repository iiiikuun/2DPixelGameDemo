using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    PlayerStats playerStats;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statType.ToString();

        if (statNameText != null)
            statNameText.text = statType.ToString();
    }

    private void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        UpdateStatValueUI();

        playerStats.onStatChanged += UpdateStatValueUI;
    }

    private void UpdateStatValueUI()
    {
        if (playerStats != null)
        {
            if (statType is StatType.暴击率 or
                   StatType.暴击伤害 or
                   StatType.闪避率 or
                   StatType.攻速 or
                   StatType.增伤 or
                   StatType.减伤 or
                   StatType.韧性)
                statValueText.text = ((float)playerStats.GetValue(statType) / 10).ToString() + "%";
            else
                statValueText.text = playerStats.GetValue(statType).ToString();
        }
    }
}
