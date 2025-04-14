using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{
    private CinemachineImpulseSource screenShake => GetComponent<CinemachineImpulseSource>();

    [Header("幻影特效")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLooseTime;
    [SerializeField] private float afterImageCooldown;
    private float afterImageCooldownTime;

    protected override void Update()
    {
        base.Update();

        afterImageCooldownTime -= Time.deltaTime;
    }

    #region 特效
    public override void CreateAfterImage()
    {
        if (afterImageCooldownTime < 0)
        {
            afterImageCooldownTime = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLooseTime, sr.sprite);
        }
    }

    public override void ScreenShake(Vector2 shakePower)
    {
        screenShake.m_DefaultVelocity = shakePower;
        screenShake.GenerateImpulse();
    }
    #endregion
}
