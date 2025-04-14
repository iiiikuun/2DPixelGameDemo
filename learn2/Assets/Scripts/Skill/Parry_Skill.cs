using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("格挡数据")]
    public float counterAttackDuration;
    private float defaultCounterAttackDuration = 0.1f;
    public float stunDuration;
    private float defaultStunDuration = 2;
    public Vector2 stunDirection = new Vector2(10, 10);

    #region 技能树解锁
    [Header("格挡")]
    [SerializeField] private bool 格挡;
    [SerializeField] private UI_SkillSlot 格挡按钮;
    [Header("延长格挡时间")]
    [SerializeField] private bool 延时格挡;
    [SerializeField] private float 延时格挡时间 = 0.15f;
    [SerializeField] private UI_SkillSlot 延时格挡按钮;
    [Header("再次延长格挡时间")]
    [SerializeField] private bool 超级延时;
    [SerializeField] private float 超级延时时间 = 0.25f;
    [SerializeField] private UI_SkillSlot 超级延时按钮;
    [Header("格挡成功时召唤分身")]
    [SerializeField] private bool 影卫追击;
    [SerializeField] private UI_SkillSlot 影卫追击按钮;
    [Header("格挡成功时额外召唤一具分身")]
    [SerializeField] private bool 影卫双生法;
    [SerializeField] private UI_SkillSlot 影卫双生法按钮;
    [Header("增加敌人眩晕时间")]
    [SerializeField] private bool 强力反击;
    [SerializeField] private float 强力反击时间 = 1;
    [SerializeField] private UI_SkillSlot 强力反击按钮;
    [Header("格挡成功时降低目标50护甲，持续6秒，可叠加")]
    [SerializeField] private bool 弱点揭示;
    [SerializeField] private UI_SkillSlot 弱点揭示按钮;
    [Header("格挡成功后进入1.5秒无敌状态")]
    [SerializeField] private bool 磐石霸体诀;
    [SerializeField] private UI_SkillSlot 磐石霸体诀按钮;

    private void 解锁格挡()
    {
        if (!格挡 && 格挡按钮.unlocked)
            格挡 = true;
    }

    private void 解锁延时格挡()
    {
        if (!延时格挡 && 延时格挡按钮.unlocked)
        {
            IncreaseCounterAttackDuration(延时格挡时间);
            延时格挡 = true;
        }
    }

    private void 解锁超级延时()
    {
        if (!超级延时 && 超级延时按钮.unlocked)
        {
            IncreaseCounterAttackDuration(超级延时时间);
            超级延时 = true;
        }
    }

    private void 解锁影卫追击()
    {
        if (!影卫追击 && 影卫追击按钮.unlocked)
            影卫追击 = true;
    }

    private void 解锁影卫双生法()
    {
        if (!影卫双生法 && 影卫双生法按钮.unlocked)
            影卫双生法 = true;
    }

    private void 解锁强力反击()
    {
        if (!强力反击 && 强力反击按钮.unlocked)
        {
            IncreaseStunDuration();
            强力反击 = true;
        }
    }

    private void 解锁弱点揭示()
    {
        if (!弱点揭示 && 弱点揭示按钮.unlocked)
            弱点揭示 = true;
    }

    private void 解锁磐石霸体决()
    {
        if (!磐石霸体诀 && 磐石霸体诀按钮.unlocked)
            磐石霸体诀 = true;
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        counterAttackDuration = defaultCounterAttackDuration;
        stunDuration = defaultStunDuration;
    }

    protected override void Start()
    {
        base.Start();

        格挡按钮.GetComponent<Button>().onClick.AddListener(() => 解锁格挡());
        延时格挡按钮.GetComponent<Button>().onClick.AddListener(() => 解锁延时格挡());
        超级延时按钮.GetComponent<Button>().onClick.AddListener(() => 解锁超级延时());
        影卫追击按钮.GetComponent<Button>().onClick.AddListener(() => 解锁影卫追击());
        影卫双生法按钮.GetComponent<Button>().onClick.AddListener(() => 解锁影卫双生法());
        强力反击按钮.GetComponent<Button>().onClick.AddListener(() => 解锁强力反击());
        弱点揭示按钮.GetComponent<Button>().onClick.AddListener(() => 解锁弱点揭示());
        磐石霸体诀按钮.GetComponent<Button>().onClick.AddListener(() => 解锁磐石霸体决());

        解锁格挡();
        解锁延时格挡();
        解锁超级延时();
        解锁影卫追击();
        解锁影卫双生法();
        解锁强力反击();
        解锁弱点揭示();
        解锁磐石霸体决();
    }

    public override bool CanUseSkill()
    {
        if (!格挡)
            return false;

        return base.CanUseSkill();
    }

    #region 技能效果应用
    public override void UseSkill()
    {
        base.UseSkill();
    }

    private void IncreaseCounterAttackDuration(float _counterAttackDuration)
    {
        counterAttackDuration += _counterAttackDuration;
    }

    public void CloneOnCounterSucceed(Transform _enemyTransform)
    {
        if (影卫追击)
            StartCoroutine(SkillManager.instance.clone.CreateCloneWithDelay(_enemyTransform, new Vector3(-1 * player.facingDir, 0), 0.5f));
    }

    public void TwiceCloneOnCounterSucceed(Transform _enemyTransform)
    {
        if (影卫双生法)
            StartCoroutine(SkillManager.instance.clone.CreateCloneWithDelay(_enemyTransform, new Vector3(player.facingDir, 0), 1));
    }

    private void IncreaseStunDuration()
    {
        stunDuration += 强力反击时间;
    }

    public void DecreaseTargetArmor(Enemy enemy)
    {
        if (弱点揭示)
        {
            enemy.stats.RemoveValue(StatType.护甲, 50);
            StartCoroutine(IncreaseTargetArmorDelay(enemy));
        }
    }

    private IEnumerator IncreaseTargetArmorDelay(Enemy enemy)
    {
        yield return new WaitForSeconds(6);
        enemy.stats.AddValue(StatType.护甲, 50);
    }

    public void EnterParryState()
    {
        if (磐石霸体诀)
        {
            StartCoroutine(player.IInvincibleFor(1.5f)); 
        }
    }
    #endregion
}