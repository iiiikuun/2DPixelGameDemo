using UnityEngine;

public class Enemy_SkeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] collidors = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in collidors)
        {
            if (hit.GetComponent<Player>() != null)
            {
                Player player = hit.GetComponent<Player>();

                if (player.isCounterAttacking)
                {
                    if (player.facingDir != enemy.facingDir)
                    {
                        enemy.BeStunned(player.skill.parry.stunDuration, player.transform, player.skill.parry.stunDirection);

                        player.SuccessfulCounterAttack();
                        player.skill.parry.CloneOnCounterSucceed(enemy.transform);
                        player.skill.parry.TwiceCloneOnCounterSucceed(enemy.transform);
                        player.skill.parry.DecreaseTargetArmor(enemy);

                        return;
                    }
                }

                player.Damage(enemy.stats.DoDamage(player.stats), 0, null, Vector2.zero);
            }
        }
    }
}
