using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BlackBackground : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();

    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
