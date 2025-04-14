using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] collidors;

        AudioManager.instance.PlaySFX(0);

        if (player.comboCounter == 0)
            collidors = Physics2D.OverlapCircleAll(player.attackCheck[0].position, player.attackCheckRadius[0]);
        else if (player.comboCounter == 1)
            collidors = Physics2D.OverlapCircleAll(player.attackCheck[1].position, player.attackCheckRadius[1]);
        else
            collidors = Physics2D.OverlapCircleAll(player.attackCheck[2].position, player.attackCheckRadius[2]);

        foreach (var hit in collidors)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                if (player.comboCounter == 2)
                    hit.GetComponent<Enemy>().Damage(player.stats.DoDamage(enemy.stats), player.heavyAttackFreezeDuration, transform, player.enemyMovement[player.comboCounter]);
                else
                    hit.GetComponent<Enemy>().Damage(player.stats.DoDamage(enemy.stats), 0,transform, player.enemyMovement[player.comboCounter]);
            }
        }

        player.skill.dash.CancelCritState();
    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
