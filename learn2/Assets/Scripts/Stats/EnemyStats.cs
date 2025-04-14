using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    protected Enemy enemy => GetComponent<Enemy>();
    protected ItemDrop myDropSystem => GetComponent<ItemDrop>();

    public List<DropProbility> dropList = new List<DropProbility>();

    protected override void Start()
    {
        base.Start();
    }

    protected override void Die()
    {
        if (isDead)
            return;
        else
            isDead = true;

        base.Die();

        enemy.Die();

        if (dropList.Count != 0)
            CheckDropProbability();
    }

    protected virtual void CheckDropProbability()
    {
        for (int i = 0; i < dropList.Count; i++)
        {
            int probability = dropList[i].GetDropProbility(level);

            if (probability > Random.Range(0,1000))
                myDropSystem.DropItem(dropList[i].GetItemData());
        }
    }
}
