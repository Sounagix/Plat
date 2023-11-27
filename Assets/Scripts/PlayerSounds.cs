using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SOUNDS
{
    JUMP,
}

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpClip;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(SOUNDS sOUNDS)
    {
        switch (sOUNDS)
        {
            case SOUNDS.JUMP:
                audioSource.clip = jumpClip;
                audioSource.Play();
                break;
        }
    }
}
