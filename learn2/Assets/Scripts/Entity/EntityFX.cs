using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected SpriteRenderer sr => GetComponentInChildren<SpriteRenderer>();

    [Header("隐身")]
    [SerializeField] public GameObject stats_UI;

    [Header("打击特效")]
    [SerializeField] protected Material hitMat;
    protected Material originalMat;
    [SerializeField] protected GameObject hitFX;

    protected virtual void Awake()
    {
        originalMat = sr.material;
    }

    protected virtual void Update()
    {
        
    }

    #region 闪烁
    protected virtual IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(0.2f);

        sr.material = originalMat;
    }

    protected virtual void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else sr.color = Color.red;
    }

    protected virtual void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    #endregion
    #region 隐身
    public void MakeTransprent(bool _transparent)
    {
        if (_transparent)
        {
            sr.color = Color.clear;
            stats_UI.SetActive(false);
        }
        else
        {
            sr.color = Color.white;
            stats_UI.SetActive(true);
        }
    }

    public IEnumerator FadeOut(float fadeDuration)
    {
        float startAlpha = sr.color.a;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);

            Color newColor = sr.color;
            newColor.a = newAlpha;
            sr.color = newColor;

            yield return null;
        }
    }

    #endregion
    #region 特效
    public void CreateHitFX(Transform target)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);

        GameObject newHitfx = Instantiate(hitFX, target.position + new Vector3(xPosition, yPosition, 0), Quaternion.identity);

        newHitfx.transform.Rotate(new Vector3(0, 0, zRotation));

        Destroy(newHitfx,0.5f);
    }

    public virtual void CreateAfterImage()
    {
        
    }

    public virtual void ScreenShake(Vector2 shakePower)
    {

    }
    #endregion
}


