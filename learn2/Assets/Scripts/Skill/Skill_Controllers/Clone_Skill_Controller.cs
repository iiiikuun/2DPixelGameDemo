using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    private Animator anim => GetComponent<Animator>();

    [SerializeField] private float colorLoosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;
    private Transform closestEnemy;

    private bool canDuplicateClone;
    private int chanceToDuplicate;
    private int facingDir = 1;

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
    public void SetupClone(Transform _newTransform,float _cloneDuration,bool _canAttack,Vector3 _offset,Transform _closestEnemy,bool _canDuplicateClone,int _chanceToDuplicate)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 4));

        transform.position = _newTransform.position+_offset;
        cloneTimer = _cloneDuration;
        closestEnemy = _closestEnemy;
        canDuplicateClone = _canDuplicateClone;
        chanceToDuplicate = _chanceToDuplicate;

        FaceClosestTarger();
    }

    private void AnimationTrigger()
    {
        cloneTimer = 0;
    }

    private void AttackTrigger()
    {
        Collider2D[] collidors = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in collidors)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                hit.GetComponent<Enemy>().Damage(PlayerManager.instance.player.stats.DoDamage(enemy.stats),0, transform, new Vector2(3, 3));

                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(0.5f * facingDir, 0));
                }
            }
        }
    }

    private void FaceClosestTarger()
    {
        if(closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0,180,0);
            }
        }
    }
}
