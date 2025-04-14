using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player => GetComponent<Player>();

    protected override void Awake()
    {
        base.Awake();
    }

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

        player.Die();
    }

}
