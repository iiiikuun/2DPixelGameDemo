using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private Animator anim=>GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float crystalExitTimer;

    private bool canMove;
    private float moveSpeed;
    private Transform closestEnemy;

    private bool canExplode;
    private bool canGrow;
    private float growSpeed = 5;
    private float defaultColliderRadius;
    private float FreezeDuration = 0.5f;

    public void SetupCrystal(float _crystalDuration,bool _canExplode,bool _canMove,float _moveSpeed,Transform _closestEnemy)
    {
        crystalExitTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _moveSpeed;
        closestEnemy = _closestEnemy;

        defaultColliderRadius = cd.radius;
    }

    private void Update()
    {
        crystalExitTimer -= Time.deltaTime;

        if(crystalExitTimer < 0)
        {
            FinishCrystal();
        }

        if (canMove&&closestEnemy!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, moveSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.position, closestEnemy.position) < 1.5f)
                FinishCrystal();
        }

        if(canGrow)
        {
            transform.localScale=Vector2.Lerp(transform.localScale,new Vector2(3,3),growSpeed*Time.deltaTime);
            cd.radius = transform.localScale.x * defaultColliderRadius;
        }
    }

    private void AnnimationExplodeEvent()
    {
        Collider2D[] collidors = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in collidors)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();

                hit.GetComponent<Enemy>().Damage(PlayerManager.instance.player.stats.DoDamage(enemy.stats), FreezeDuration,transform, new Vector2(8, 8));
            }
        }
    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            anim.SetTrigger("Explode");

            canMove = false;
        }
        else
            Destroy(gameObject);
    }
}
