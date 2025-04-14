using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    力量,
    敏捷,
    智力,
    体质,
    攻击力,
    攻速,
    法强,
    暴击率,
    暴击伤害,
    增伤,
    穿甲,
    法穿,
    生命值,
    护甲,
    魔抗,
    闪避率,
    减伤,
    韧性,
    法力值,
    移速
}

public class CharacterStats : MonoBehaviour
{
    #region Stats
    [Header("主属性")]
    public Stat strength = new Stat(StatType.力量); // 力量:1攻击，1%暴击伤害
    public Stat agility = new Stat(StatType.敏捷); // 敏捷:1%暴击率，0.5%闪避概率
    public Stat intelligence = new Stat(StatType.智力); // 智力:2法强，5法力
    public Stat vitality = new Stat(StatType.体质); // 活力:5生命，1护甲

    [Header("攻击属性")]
    public Stat damage = new Stat(StatType.攻击力);
    public Stat attackSpeed = new Stat(StatType.攻速);
    public Stat magicPower = new Stat(StatType.法强);
    public Stat critChance = new Stat(StatType.暴击率);
    public Stat critPower = new Stat(StatType.暴击伤害);
    public Stat damageIncrease = new Stat(StatType.增伤);
    public Stat armorPiercing = new Stat(StatType.穿甲);
    public Stat magicPiercing = new Stat(StatType.法穿);

    [Header("防御属性")]
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

    private EntityFX fx => GetComponent<EntityFX>();

    public int level;
    public int currentHealth;
    public int currentMana;

    protected bool isDead;

    public System.Action onHealthChanged;
    public System.Action onMaxHealthChanged;
    public System.Action onStatChanged;

    protected virtual void Awake()
    {
        currentHealth = GetMaxHealthValue();
        currentMana = GetMaxManaValue();
        critPower.SetBaseValue(1500);
    }

    protected virtual void Start()
    {
        
    }

    #region Damage
    public virtual bool DoDamage(CharacterStats _taraget)
    {
        if (TargetCanAvoidAttack(_taraget))
            return false;

        fx.CreateHitFX(_taraget.transform);

        float totalDamage = GetDamageValue();

        float totalArmor = _taraget.GetArmorValue() - armorPiercing.GetValue();
        totalArmor = Mathf.Clamp(totalArmor, 0, Mathf.Infinity);
        float armorEffect= 100f / (100f + totalArmor);
        totalDamage *= armorEffect;

        totalDamage = totalDamage * (1 + (damageIncrease.GetValue() - _taraget.damageDecrease.GetValue()) * 0.001f);

        if (CanCrit())
            totalDamage =CalculateCriticalDamage((int)totalDamage);

        _taraget.TakeDamage((int)totalDamage);

        return true;
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log("造成伤害"+_damage);

        if (onHealthChanged != null)
            onHealthChanged();

        if (currentHealth <= 0)
            Die();
    }
    #endregion
    #region Calculate Crit And Avoid
    protected virtual bool TargetCanAvoidAttack(CharacterStats _taraget)
    {
        if (Random.Range(0, 1000) < _taraget.GetEvasionValue())
        {
            Debug.Log("闪避成功");
            return true;
        }

        return false;
    }

    protected virtual bool CanCrit()
    {
        if (Random.Range(0, 1000) < GetCritChanceValue())
            return true;

        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float critDamage = _damage * GetCritPowerValue() * 0.001f;
        Debug.Log("暴击");

        return Mathf.FloorToInt(critDamage);
    }
    #endregion 
    #region Get Value
    public virtual int GetValue(StatType statType) => statType switch
    {
        StatType.力量 => strength.GetValue(),
        StatType.敏捷 => agility.GetValue(),
        StatType.智力 => intelligence.GetValue(),
        StatType.体质 => vitality.GetValue(),
        StatType.攻击力 => GetDamageValue(),
        StatType.攻速 => attackSpeed.GetValue(),
        StatType.法强 => GetMagicPowerValue(),
        StatType.暴击率 => GetCritChanceValue(),
        StatType.暴击伤害 => GetCritPowerValue(),
        StatType.增伤 => damageIncrease.GetValue(),
        StatType.穿甲 => armorPiercing.GetValue(),
        StatType.法穿 => magicPiercing.GetValue(),
        StatType.生命值 => GetMaxHealthValue(),
        StatType.护甲 => GetArmorValue(),
        StatType.魔抗 => magicResistance.GetValue(),
        StatType.闪避率 => GetEvasionValue(),
        StatType.减伤 => damageDecrease.GetValue(),
        StatType.韧性 => resilience.GetValue(),
        StatType.法力值 => GetMaxManaValue(),
        StatType.移速 => moveSpeed.GetValue(),
        _ => 0
    };

    public virtual int GetDamageValue() => damage.GetValue() + strength.GetValue();
    public virtual int GetMagicPowerValue() => magicPower.GetValue() + 2 * intelligence.GetValue();
    public virtual int GetCritChanceValue() => critChance.GetValue() + 10 * agility.GetValue();
    public virtual int GetCritPowerValue() => critPower.GetValue() + 10 * strength.GetValue();
    public virtual int GetMaxHealthValue() => maxHealth.GetValue() + 5 * vitality.GetValue();
    public virtual int GetArmorValue() => armor.GetValue() + vitality.GetValue();
    public virtual int GetMaxManaValue() => maxMana.GetValue() + 5 * intelligence.GetValue();
    public virtual int GetEvasionValue() => evasion.GetValue() + 5 * agility.GetValue();
    #endregion
    #region Change Value
    public virtual void AddValue(StatType statType, int value)
    {
        switch (statType)
        {
            case StatType.力量: strength.AddModifier(value); break;
            case StatType.敏捷: agility.AddModifier(value); break;
            case StatType.智力: intelligence.AddModifier(value); break;
            case StatType.体质:
                {
                    vitality.AddModifier(value);

                    if (onMaxHealthChanged != null)
                        onMaxHealthChanged();
                    break;
                }
            case StatType.攻击力: damage.AddModifier(value); break;
            case StatType.攻速: attackSpeed.AddModifier(value); break;
            case StatType.法强: magicPower.AddModifier(value); break;
            case StatType.暴击率: critChance.AddModifier(value); break;
            case StatType.暴击伤害: critPower.AddModifier(value); break;
            case StatType.增伤: damageIncrease.AddModifier(value); break;
            case StatType.穿甲: armorPiercing.AddModifier(value); break;
            case StatType.法穿: magicPiercing.AddModifier(value); break;
            case StatType.生命值:
                {
                    maxHealth.AddModifier(value);

                    if (onMaxHealthChanged != null)
                        onMaxHealthChanged();
                    break;
                }
            case StatType.护甲: armor.AddModifier(value); break;
            case StatType.魔抗: magicResistance.AddModifier(value); break;
            case StatType.闪避率: evasion.AddModifier(value); break;
            case StatType.减伤: damageDecrease.AddModifier(value); break;
            case StatType.韧性: resilience.AddModifier(value); break;
            case StatType.法力值: maxMana.AddModifier(value); break;
            case StatType.移速: moveSpeed.AddModifier(value); break;
            default: break;
        }

        if(onStatChanged != null)
            onStatChanged();
    }

    public virtual void RemoveValue(StatType statType, int value)
    {
        switch (statType)
        {
            case StatType.力量: strength.RemoveModifier(value); break;
            case StatType.敏捷: agility.RemoveModifier(value); break;
            case StatType.智力: intelligence.RemoveModifier(value); break;
            case StatType.体质:
                {
                    vitality.RemoveModifier(value);

                    if (onMaxHealthChanged != null)
                        onMaxHealthChanged();
                    break;
                }
            case StatType.攻击力: damage.RemoveModifier(value); break;
            case StatType.攻速: attackSpeed.RemoveModifier(value); break;
            case StatType.法强: magicPower.RemoveModifier(value); break;
            case StatType.暴击率: critChance.RemoveModifier(value); break;
            case StatType.暴击伤害: critPower.RemoveModifier(value); break;
            case StatType.增伤: damageIncrease.RemoveModifier(value); break;
            case StatType.穿甲: armorPiercing.RemoveModifier(value); break;
            case StatType.法穿: magicPiercing.RemoveModifier(value); break;
            case StatType.生命值:
                {
                    maxHealth.RemoveModifier(value);

                    if (onMaxHealthChanged != null)
                        onMaxHealthChanged();
                    break;
                }
            case StatType.护甲: armor.RemoveModifier(value); break;
            case StatType.魔抗: magicResistance.RemoveModifier(value); break;
            case StatType.闪避率: evasion.RemoveModifier(value); break;
            case StatType.减伤: damageDecrease.RemoveModifier(value); break;
            case StatType.韧性: resilience.RemoveModifier(value); break;
            case StatType.法力值: maxMana.RemoveModifier(value); break;
            case StatType.移速: moveSpeed.RemoveModifier(value); break;
            default:
                break;
        }

        if (onStatChanged != null)
            onStatChanged();
    }
    #endregion

    protected virtual void Die()
    {
        
    }
}
