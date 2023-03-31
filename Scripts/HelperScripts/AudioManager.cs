using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip sounds1;
    public AudioClip sounds2;
    public AudioClip sounds3;
    public static AudioManager instance;
    private AudioSource mainTheme;

    private void Awake()
    {
        if(instance!=null)
        {
            instance = this;
        }
        mainTheme = gameObject.AddComponent<AudioSource>();
        mainTheme.clip = sounds1;
        mainTheme.loop = true;
        mainTheme.volume = 0.2f;
        mainTheme.Play();
    }
    void Start()
    {

    }

    // Update is called once per frame
    public void fruitPickUp()
    {
        AudioSource fruitTheme = new AudioSource();
        fruitTheme.clip = sounds2;
        fruitTheme.loop = false;
        fruitTheme.volume = 1.0f;
        mainTheme.Pause();
        fruitTheme.Play();
        mainTheme.UnPause();
    }

    public void gameOverTheme()
    {
        AudioSource gameOver = gameObject.AddComponent<AudioSource>();
        gameOver.clip = sounds3;
        gameOver.loop = false;
        gameOver.volume = 1.0f;
        mainTheme.Pause();
        gameOver.Play();
        mainTheme.UnPause();
    }
}
