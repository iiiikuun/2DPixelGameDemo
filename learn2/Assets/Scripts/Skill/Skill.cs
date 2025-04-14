using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected float sectionTime;
    [SerializeField] protected float defaultCooldown;
    [SerializeField] protected int mana;
    [SerializeField] protected int defaultMana;
    [SerializeField] protected float cooldownTimer;
    [SerializeField] protected float sectionTimer;
    [SerializeField] protected int sectionCounter;

    protected Player player;

    protected virtual void Awake()
    {
        cooldown = defaultCooldown;
        mana = defaultMana;
    }

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer-= Time.deltaTime;
        sectionTimer -= Time.deltaTime;
        if (sectionTimer < 0 && sectionCounter > 0)
        {
            sectionCounter = 0;
            cooldownTimer = cooldown;
        }
            
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0 && player.stats.currentMana > mana || sectionCounter > 0)
            return true;
        else
            return false;
    }

    public virtual void UseSkill()
    {
        if (sectionCounter <= 1)
        {
            cooldownTimer = cooldown;

            if (sectionCounter <= 0)
                player.stats.currentMana -= mana;
        }

        sectionCounter--;
    }


    protected virtual void ChangeCooldown(float _cooldown)
    {
        cooldown = cooldown + defaultCooldown * _cooldown;
    }

    protected virtual void ChangeMana(int _mana)
    {
        mana = mana + _mana;
    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] collidors = Physics2D.OverlapCircleAll(_checkTransform.position, 20);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in collidors)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }
        return closestEnemy;
    }
}
