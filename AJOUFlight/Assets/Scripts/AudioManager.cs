using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<AudioManager>();
            return instance;
        }
    }

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip playerHitClip;
    [SerializeField]
    private AudioClip enemyHitClip;
    [SerializeField]
    private AudioClip playerShootClip;
    [SerializeField]
    private AudioClip enemyShootClip;
    [SerializeField]
    private AudioClip playerDeathClip;
    [SerializeField]
    private AudioClip enemyDeathClip;
    [SerializeField]
    private AudioClip winClip;


    public AudioSource AudioSource
    {
        get { return audioSource; }
        set { if (value != null) audioSource = value; else { Debug.Log("fail to load the audioSource."); } }
    }

    public AudioClip PlayerHitClip
    {
        get { return playerHitClip; }
        set { if (value != null) playerHitClip = value; else { Debug.Log("failed to load the playerHitClip.");} }
    }

    public AudioClip EnemyHitClip
    {
        get { return enemyHitClip; }
        set { if (value != null) enemyHitClip = value; else { Debug.Log("failed to load the enemyHitClip."); } }
    }

    public AudioClip PlayerShootClip
    {
        get { return playerShootClip; }
        set { if (value != null) playerShootClip = value; else { Debug.Log("failed to load the playerShootClip."); } }
    }

    public AudioClip EnemyShootClip
    {
        get { return enemyShootClip; }
        set { if (value != null) enemyShootClip = value; else { Debug.Log("failed to load the enemyShootClip."); } }
    }

    public AudioClip PlayerDeathClip
    {
        get { return playerDeathClip; }
        set { if (value != null) playerDeathClip = value; else { Debug.Log("failed to load the playerDeathClip."); } }
    }

    public AudioClip EnemyDeathClip
    {
        get { return enemyDeathClip; }
        set { if (value != null) enemyDeathClip = value; else { Debug.Log("failed to load the enemyDeathClip."); } }
    }

    public AudioClip WinClip
    {
        get { return winClip; }
        set { if (value != null) winClip = value; else { Debug.Log("failed to load the winClip."); } }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayPlayerHitClip()
    {
        audioSource.clip = PlayerHitClip;
        audioSource.Play();
    }

    public void PlayEnemyHitClip()
    {
        audioSource.clip = EnemyHitClip;
        audioSource.Play();
    }

    public void PlayPlayerShootClip()
    {
        audioSource.clip = PlayerShootClip;
        audioSource.Play();
    }

    public void PlayEnemyShootClip()
    {
        audioSource.clip = EnemyShootClip;
        audioSource.Play();
    }

    public void PlayPlayerDeathClip()
    {
        audioSource.clip = PlayerDeathClip;
        audioSource.Play();
    }

    public void PlayEnemyDeathClip()
    {
        audioSource.clip = EnemyDeathClip;
        audioSource.Play();
    }

    public void PlayWinClip()
    {
        audioSource.clip = WinClip;
        audioSource.Play();
    }
}