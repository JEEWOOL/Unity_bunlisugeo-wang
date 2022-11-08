using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtnManager : MonoBehaviour
{
    public bool isSoundOn;
    AudioSource audioSource;
    
    public Image image;
    public Sprite playSprite;
    public Sprite muteSprite;
    

    private void Awake()
    {
        isSoundOn = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        image.sprite = playSprite;
    }

    public void MusicToggle()
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            audioSource.Pause();
            image.sprite = muteSprite;
        }
        else
        {
            isSoundOn = true;
            audioSource.Play();
            image.sprite = playSprite;
        }
    }
}
