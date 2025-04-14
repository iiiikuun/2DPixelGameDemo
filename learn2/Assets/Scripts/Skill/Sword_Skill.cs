using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordType swordType= SwordType.Regular;

    [Header("Bounce info")]
    [SerializeField] private int bounceAmount;

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Spin info")]
    [SerializeField] private float maxTravelDistance=7f;
    [SerializeField] private float spinDuration=2f;
    [SerializeField] private float hitCooldown = 0.5f;

    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float launchForce;
    [SerializeField] private float swordGravity;
    private float defaultSwordGravaty;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float swordSpeed;


    private Vector2 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float timeBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        defaultSwordGravaty = swordGravity;

        GenerateDots();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce, AimDirection().normalized.y * launchForce);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (swordType == SwordType.Pierce)
                swordGravity = pierceGravity;
            else
                swordGravity = defaultSwordGravaty;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * timeBetweenDots);
            }
        }
    }

    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        if (swordType == SwordType.Bounce)
            newSwordScript.SetupBounce(true,bounceAmount);
        else if (swordType == SwordType.Pierce)
        {
            newSwordScript.SetupPierce(true,pierceAmount);
            swordGravity = pierceGravity;
        }
        else if(swordType == SwordType.Spin)
            newSwordScript.SetupSpin(true, maxTravelDistance,spinDuration,hitCooldown);

        newSwordScript.SetupSword(finalDir, swordGravity, player,freezeTimeDuration,swordSpeed);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }

    #region Aim

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(AimDirection().normalized.x * launchForce, AimDirection().normalized.y * launchForce) * t + 0.5f * (Physics2D.gravity * swordGravity) * t * t;

        return position;
    }

    #endregion
}
