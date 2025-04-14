using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();
    private Player player;

    private bool isReturning;
    private bool canRotate = true;

    private float swordSpeed;
    private float freezeTimeDuration;

    [Header("Pierce info")]
    private int pierceAmount;
    private bool isPiercing;

    [Header("Bounce info")]
    private bool isBouncing;
    private int bounceAmount;
    private List<Transform> enemyTarget;
    private int targetIndex;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool isSpinning;
    private bool wasStopped;
    private float hitTimer;
    private float hitCooldown;

    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player, float _freezeTimeDuraTion, float _swordSpeed)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        player = _player;
        freezeTimeDuration = _freezeTimeDuraTion;
        swordSpeed = _swordSpeed;

        if (!isPiercing)
            anim.SetBool("Rotation", true);
    }

    public void SetupBounce(bool _isBouncing,int _bounceAmount)
    {
        isBouncing= _isBouncing;
        bounceAmount= _bounceAmount;

        enemyTarget=new List<Transform>();
    }

    public void SetupPierce(bool _isPiercing,int _pierceAmount)
    {
        isPiercing= _isPiercing;
        pierceAmount = _pierceAmount;
    }

    public void SetupSpin(bool _isSpinning,float _maxTravelDistance,float _spinDuration,float _hitCooldown)
    {
        isSpinning= _isSpinning;
        spinDuration= _spinDuration;
        maxTravelDistance = _maxTravelDistance;
        hitCooldown= _hitCooldown;
    }

    public void ReturnSword()
    {
        anim.SetBool("Rotation", false);
        cd.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturning = true;

        isSpinning = false;
    }

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, swordSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchTheSword();
        }

        if(Vector2.Distance(transform.position,player.transform.position) >50)
            Destroy(gameObject);

        BounceLogic();
        SpinLogic();
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, swordSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            {
                SwordSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());

                targetIndex++;
                bounceAmount--;

                if (bounceAmount <= 0 || enemyTarget.Count==1)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }
    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;

                if (spinTimer < 0)
                {
                    isSpinning = false;
                    isReturning = true;
                }

                hitTimer -= Time.deltaTime;

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.8f);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            SwordSkillDamage(hit.GetComponent<Enemy>());
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
        cd.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBouncing)
        {
            SetupTargetsForBounce(collision);
            StuckInto(collision);
            return;
        }

        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }

        if (collision.GetComponent<Enemy>() != null)
            SwordSkillDamage(collision.GetComponent<Enemy>());

        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        StuckInto(collision);
    }

    private void SwordSkillDamage(Enemy enemy)
    {
        enemy.Damage(PlayerManager.instance.player.stats.DoDamage(enemy.stats),0,null,Vector2.zero);
    }

    private void SetupTargetsForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

            Array.Sort(colliders, (a, b) =>Vector2.Distance(a.transform.position, transform.position).CompareTo(Vector2.Distance(b.transform.position, transform.position)));

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                    enemyTarget.Add(hit.transform);
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        canRotate = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        cd.enabled = false;
        rb.isKinematic = true;

        if (isBouncing && enemyTarget.Count > 0)
            return;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
