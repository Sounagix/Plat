using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SOUNDS
{
    JUMP, DAMAGE, DIE, DEFENSIVE, MOV
}

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip jumpClip, damageClip, dieSound, defensiveClip, movClip;

    private SOUNDS currentSound;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool GetStatus(SOUNDS sOUNDS)
    {
        return currentSound == sOUNDS && audioSource.isPlaying;
    }


    public void PlaySound(SOUNDS sOUNDS)
    {
        if (sOUNDS == currentSound && audioSource.isPlaying) return;
        currentSound = sOUNDS;
        StopSound();
        switch (sOUNDS)
        {
            case SOUNDS.JUMP:
                audioSource.PlayOneShot(jumpClip);
                break;
            case SOUNDS.DAMAGE:
                audioSource.PlayOneShot(damageClip);
                break;
            case SOUNDS.DIE:
                audioSource.PlayOneShot(dieSound);
                break;
            case SOUNDS.DEFENSIVE:
                audioSource.loop = true;
                audioSource.clip = defensiveClip;
                audioSource.Play();
                break;
            case SOUNDS.MOV:
                audioSource.loop = true;
                audioSource.clip = movClip;
                audioSource.Play();
                break;
            default:
                break;
        }
    }

    public void StopSound()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}
