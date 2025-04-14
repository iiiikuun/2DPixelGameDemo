using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageFX : MonoBehaviour
{
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    private float colorLooseTime;

    public void SetupAfterImage(float _loosingTime, Sprite _spriteImage)
    {
        sr.sprite = _spriteImage;
        colorLooseTime = _loosingTime;
    }

    private void Update()
    {
        float alpha = sr.color.a - Time.deltaTime / colorLooseTime;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (sr.color.a <= 0)
            Destroy(gameObject);
    }
}
