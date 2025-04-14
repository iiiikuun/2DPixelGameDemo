using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Crystal mirage")]
    [SerializeField] private bool cloneInsteadOfCrystal;

    [Header("Explode crystal")]
    [SerializeField] private bool canExplode;

    [Header("Moving crystal")]
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed;

    [Header("Multi stacking crystal")]
    [SerializeField] private bool canMultiStacks;
    [SerializeField] private int amountOfStacks;
    [SerializeField] private List<GameObject> crystalLeft=new List<GameObject>();
    [SerializeField] private float useTimeWindow;
    private float useTimer;

    protected override void Start()
    {
        base.Start();

        RefillCrystal();

        useTimer = Mathf.Infinity;
    }

    protected override void Update()
    {
        base.Update();

        useTimer-= Time.deltaTime;

        if (useTimer < 0)
        {
            ResetAbility();
        }
    }

    public override void UseSkill()
    {
        if (canMultiStacks)
        {
            UseMultiCrystal();
            return;
        }

        if(currentCrystal == null)
        {
            CreateCrystal();
        }
        else
        {
            base.UseSkill();

            Vector2 playPos= player.transform.position;
            player.transform.position=currentCrystal.transform.position;
            currentCrystal.transform.position=playPos;

            if (cloneInsteadOfCrystal)
            {
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
                currentCrystal.GetComponent<Crystal_Skill_Controller>().FinishCrystal();

        }
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);

        Crystal_Skill_Controller currentCristalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCristalScript.SetupCrystal(crystalDuration, canExplode, canMove, moveSpeed, FindClosestEnemy(currentCrystal.transform));
    }

    private void UseMultiCrystal()
    {
        if(crystalLeft.Count > 0)
        {
            GameObject crystalToSpawn = crystalLeft[crystalLeft.Count-1];
            GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

            crystalLeft.Remove(crystalToSpawn);

            newCrystal.GetComponent<Crystal_Skill_Controller>().SetupCrystal(crystalDuration, canExplode, canMove, moveSpeed, FindClosestEnemy(newCrystal.transform));                        
        
            if(crystalLeft.Count <= 0)
            {
                ResetAbility();
            }
            else
            {
                useTimer = useTimeWindow;
            }
        }
    }

    private void ResetAbility()
    {
        base.UseSkill();
        RefillCrystal();
        useTimer = Mathf.Infinity;
    }

    private void RefillCrystal()
    {
        while(crystalLeft.Count < amountOfStacks)
        {
            crystalLeft.Add(crystalPrefab);
        }
    }

}