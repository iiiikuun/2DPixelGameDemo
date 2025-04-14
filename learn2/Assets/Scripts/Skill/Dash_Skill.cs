using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("冲刺数据")]
    public float dashSpeed;
    private float defaultDashSpeed = 25;
    public float dashDuration = 0.2f;

    #region 技能树解锁
    [SerializeField] private bool 冲刺;
    [SerializeField] private UI_SkillSlot 冲刺按钮;
   
    [SerializeField] private bool 加速冲刺;
    [SerializeField] private float 加速冲刺速度 = 0.2f;
    [SerializeField] private UI_SkillSlot 加速冲刺按钮;

    [SerializeField] private bool 快速休整;
    [SerializeField] private float 快速休整冷却 = -0.2f;
    [SerializeField] private UI_SkillSlot 快速休整按钮;

    [SerializeField] private bool 快速休整pro;
    [SerializeField] private float 快速休整pro冷却 = -0.2f;
    [SerializeField] private UI_SkillSlot 快速休整pro按钮;
    
    [SerializeField] private bool 影卫断后;
    [SerializeField] private int 影卫断后蓝耗 = 5;
    [SerializeField] private UI_SkillSlot 影卫断后按钮;
  
    [SerializeField] private bool 影卫开路;
    [SerializeField] private int 影卫开路蓝耗 = 5;
    [SerializeField] private UI_SkillSlot 影卫开路按钮;
    
    [SerializeField] private bool 疾风幻影闪;
    [SerializeField] private int 疾风幻影闪蓝耗 = 10;
    [SerializeField] private UI_SkillSlot 疾风幻影闪按钮;
    
    [SerializeField] private bool 瞬息无隙步;
    [SerializeField] private float 瞬息无隙步冷却 = 0.3f;
    [SerializeField] private UI_SkillSlot 瞬息无隙步按钮;
    [SerializeField] private bool 瞬息无隙步状态;

    [SerializeField] private bool 双刃破空袭;
    [SerializeField] private bool 双刃破空袭状态;
    [SerializeField] private UI_SkillSlot 双刃破空袭按钮;

    [SerializeField] private bool 破空强袭;
    [SerializeField] private bool 破空强袭状态;
    [SerializeField] private int 破空强袭蓝耗 = 10;
    [SerializeField] private UI_SkillSlot 破空强袭按钮;



    private void 解锁冲刺()
    {
        if (!冲刺&&冲刺按钮.unlocked)
            冲刺 = true;
    }

    private void 解锁加速冲刺()
    {
        if (!加速冲刺 && 加速冲刺按钮.unlocked)
        {
            ChangeDashSpeed(加速冲刺速度);
            加速冲刺 = true;
        }
    }

    private void 解锁快速休整()
    {
        if (!快速休整 && 快速休整按钮.unlocked)
        {
            ChangeCooldown(快速休整冷却);
            快速休整 = true;
        }
    }

    private void 解锁快速休整pro()
    {
        if (!快速休整pro && 快速休整pro按钮.unlocked)
        {
            ChangeCooldown(快速休整pro冷却);
            快速休整pro = true;
        }
    }

    private void 解锁影卫断后()
    {
        if (!影卫断后 && 影卫断后按钮.unlocked)
        {
            ChangeMana(影卫断后蓝耗);
            影卫断后 = true;
        }
            
    }

    private void 解锁影卫开路()
    {
        if (!影卫开路 && 影卫开路按钮.unlocked)
        {
            ChangeMana(影卫开路蓝耗);
            影卫开路 = true;
        }
    }

    private void 解锁疾风幻影闪()
    {
        if (!疾风幻影闪 && 疾风幻影闪按钮.unlocked)
        {
            ChangeMana(疾风幻影闪蓝耗);
            疾风幻影闪 = true;
        }
    }

    private void 解锁瞬息无隙步()
    {
        if (!瞬息无隙步 && 瞬息无隙步按钮.unlocked)
        {
            ChangeCooldown(瞬息无隙步冷却);
            瞬息无隙步 = true;
        }
    }

    private void 解锁双刃破空袭()
    {
        if (!双刃破空袭 && 双刃破空袭按钮.unlocked)
            双刃破空袭 = true;
    }

    private void 解锁破空强袭()
    {
        if (!破空强袭 && 破空强袭按钮.unlocked)
        {
            ChangeMana(破空强袭蓝耗);
            破空强袭 = true;
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        dashSpeed = defaultDashSpeed;
    }

    protected override void Start()
    {
        base.Start();

        冲刺按钮.GetComponent<Button>().onClick.AddListener(() => 解锁冲刺());
        加速冲刺按钮.GetComponent<Button>().onClick.AddListener(() => 解锁加速冲刺());
        快速休整按钮.GetComponent<Button>().onClick.AddListener(() => 解锁快速休整());
        快速休整pro按钮.GetComponent<Button>().onClick.AddListener(() => 解锁快速休整pro());
        影卫断后按钮.GetComponent<Button>().onClick.AddListener(() => 解锁影卫断后());
        影卫开路按钮.GetComponent<Button>().onClick.AddListener(() => 解锁影卫开路());
        疾风幻影闪按钮.GetComponent<Button>().onClick.AddListener(() => 解锁疾风幻影闪());
        瞬息无隙步按钮.GetComponent<Button>().onClick.AddListener(() => 解锁瞬息无隙步());
        双刃破空袭按钮.GetComponent<Button>().onClick.AddListener(() => 解锁双刃破空袭());
        破空强袭按钮.GetComponent<Button>().onClick.AddListener(() => 解锁破空强袭());

        解锁冲刺();
        解锁加速冲刺();
        解锁快速休整();
        解锁快速休整pro();
        解锁影卫断后();
        解锁影卫开路();
        解锁疾风幻影闪();
        解锁瞬息无隙步();
        解锁双刃破空袭();
        解锁破空强袭();
    }

    public override bool CanUseSkill()
    {
        if(!冲刺)
            return false;

        return base.CanUseSkill();
    }

    #region 技能效果应用
    public override void UseSkill()
    {
        DoubleDash();//多段技能需要先设置段数，再释放技能

        base.UseSkill();
    }

    private void ChangeDashSpeed(float _speed)
    {
        dashSpeed = dashSpeed + defaultDashSpeed * _speed;
    }

    public void CloneOnDashStart()
    {
        if (影卫断后)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

    public void CloneOnDashOver()
    {
        if (影卫开路)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

    public void CloneWhenCollideEnemy()
    {
        if (疾风幻影闪)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

    public void invincibleDuringDash()
    {
        if (瞬息无隙步)
        {
            if (!瞬息无隙步状态)
            {
                player.IInvincible(true);
                瞬息无隙步状态 = true;
            }
            else
            {
                player.IInvincible(false);
                瞬息无隙步状态 = false;
            }
        }
    }

    public void DoubleDash()
    {
        if (双刃破空袭)
        {
            if (sectionCounter <= 0)
            {
                sectionCounter = 2;
                sectionTimer = sectionTime;
                player.stats.currentMana -= mana;
            }
        }
    }

    public void CritOnDashOver()
    {
        if (破空强袭)
        {
            if (!破空强袭状态)
            {
                player.stats.AddValue(StatType.暴击率, 1000);
                破空强袭状态 = true;
            }
        }
    }

    public void CancelCritState()
    {
        if (破空强袭)
        {
            if (破空强袭状态)
            {
                player.stats.RemoveValue(StatType.暴击率, 1000);
                破空强袭状态 = false;
            }
        }
    }
    #endregion
}
