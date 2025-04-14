using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex < sfx.Length && sfx[sfxIndex] != null)
        {
            sfx[sfxIndex].Play();
        }
    }

    public void StopSFX(int index)
    {
        if (index < sfx.Length && sfx[index] != null)
        {
            sfx[index].Stop();
        }
    }

    public void PlayBGM(int bgmIndex)
    {
        if (bgmIndex < bgm.Length && bgm[bgmIndex] != null)
        {
            StopAllBGM();
            bgm[bgmIndex].Play();
        }
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}