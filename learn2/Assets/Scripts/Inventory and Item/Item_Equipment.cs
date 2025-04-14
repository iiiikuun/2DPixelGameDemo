using UnityEngine;

[System.Serializable]
public class Item_Equipment : Item
{
    public ItemData_Equipment data;

    public UI_Equipment ui_Equipment;

    #region Stats
    [Header("Major stats")]
    public Stat strength = new Stat(StatType.力量); // 力量:1攻击，1%暴击伤害
    public Stat agility = new Stat(StatType.敏捷); // 敏捷:1%暴击率，0.5%闪避概率
    public Stat intelligence = new Stat(StatType.智力); // 智力:2法强，5法力
    public Stat vitality = new Stat(StatType.体质); // 活力:5生命，1护甲

    [Header("Offensive stats")]
    public Stat damage = new Stat(StatType.攻击力);
    public Stat attackSpeed = new Stat(StatType.攻速);
    public Stat magicPower = new Stat(StatType.法强);
    public Stat critChance = new Stat(StatType.暴击率);
    public Stat critPower = new Stat(StatType.暴击伤害);
    public Stat damageIncrease = new Stat(StatType.增伤);
    public Stat armorPiercing = new Stat(StatType.穿甲);
    public Stat magicPiercing = new Stat(StatType.法穿);

    [Header("Defensive stats")]
    public Stat maxHealth = new Stat(StatType.生命值);
    public Stat armor = new Stat(StatType.护甲);
    public Stat magicResistance = new Stat(StatType.魔抗);
    public Stat evasion = new Stat(StatType.闪避率);
    public Stat damageDecrease = new Stat(StatType.减伤);
    public Stat resilience = new Stat(StatType.韧性);

    [Header("其他属性")]
    public Stat maxMana = new Stat(StatType.法力值);
    public Stat moveSpeed = new Stat(StatType.移速);
    #endregion

    public Stat[] allStats;

    public Item_Equipment(ItemData_Equipment _data)
    {
        data = _data;

        strength.SetBaseValue(Random.Range(data.strength.min, data.strength.max + 1));
        agility.SetBaseValue(Random.Range(data.agility.min, data.agility.max + 1));
        intelligence.SetBaseValue(Random.Range(data.intelligence.min, data.intelligence.max + 1));
        vitality.SetBaseValue(Random.Range(data.vitality.min, data.vitality.max + 1));

        damage.SetBaseValue(Random.Range(data.damage.min, data.damage.max + 1));
        attackSpeed.SetBaseValue(Random.Range(data.attackSpeed.min, data.attackSpeed.max + 1));
        magicPower.SetBaseValue(Random.Range(data.magicPower.min, data.magicPower.max + 1));
        critChance.SetBaseValue(Random.Range(data.critChance.min, data.critChance.max + 1));
        critPower.SetBaseValue(Random.Range(data.critPower.min, data.critPower.max + 1));
        damageIncrease.SetBaseValue(Random.Range(data.damageIncrease.min, data.damageIncrease.max + 1));
        armorPiercing.SetBaseValue(Random.Range(data.armorPiercing.min, data.armorPiercing.max + 1));
        magicPiercing.SetBaseValue(Random.Range(data.magicPiercing.min, data.magicPiercing.max + 1));

        maxHealth.SetBaseValue(Random.Range(data.maxHealth.min, data.maxHealth.max + 1));
        armor.SetBaseValue(Random.Range(data.armor.min, data.armor.max + 1));
        magicResistance.SetBaseValue(Random.Range(data.magicResistance.min, data.magicResistance.max + 1));
        evasion.SetBaseValue(Random.Range(data.evasion.min, data.evasion.max + 1));
        damageDecrease.SetBaseValue(Random.Range(data.damageDecrease.min, data.damageDecrease.max + 1));
        resilience.SetBaseValue(Random.Range(data.resilience.min, data.resilience.max + 1));

        maxMana.SetBaseValue(Random.Range(data.maxMana.min, data.maxMana.max + 1));
        moveSpeed.SetBaseValue(Random.Range(data.moveSpeed.min, data.moveSpeed.max + 1));

        allStats = new Stat[] {
        strength, agility, intelligence, vitality,
        damage, attackSpeed, magicPower, critChance, critPower, damageIncrease, armorPiercing, magicPiercing,
        maxHealth, armor, magicResistance, evasion, damageDecrease, resilience,
        maxMana, moveSpeed
        };

        SetUpItemBonus();
    }

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        for (int i = 0; i < allStats.Length; i++)
        {
            if (allStats[i].GetValue() != 0)
                playerStats.AddValue(allStats[i].statType, allStats[i].GetValue());
        }
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        for (int i = 0; i < allStats.Length; i++)
        {
            if (allStats[i].GetValue() != 0)
                playerStats.RemoveValue(allStats[i].statType, allStats[i].GetValue());
        }
    }

    public override void SetUpItemBonus()
    {
        sb.Length = 0;

        for (int i = 0; i < allStats.Length; i++)
        {
            if (allStats[i].GetValue() != 0)
                AddItemBouns(allStats[i]);
        }
    }

    private void AddItemBouns(Stat stat)
    {
        if (sb.Length > 0)
            sb.AppendLine();

        if (stat.statType is StatType.暴击率 or
                   StatType.暴击伤害 or
                   StatType.闪避率 or
                   StatType.攻速 or
                   StatType.增伤 or
                   StatType.减伤 or
                   StatType.韧性)
        {
            if (stat.GetValue() > 0)
                sb.Append("+" + ((float)stat.GetValue() / 10).ToString() + "%" + stat.statType.ToString());
            else
                sb.Append(((float)stat.GetValue() / 10).ToString() + "%" + stat.statType.ToString());
        }
        else
        {
            if (stat.GetValue() > 0)
                sb.Append("+" + stat.GetValue() + stat.statType.ToString());
            else
                sb.Append(stat.GetValue() + stat.statType.ToString());

        }
    }

    public void DeleteUI() => ui_Equipment.DeleteUI();
}
