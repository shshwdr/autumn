using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    AudioSource[] audiosources;
    float bgmVolume = 0.9f;
    float changeTime = 5f;
    public void stopAll()
    {
        for(int i = 0;i<2;i++)
        {
            var audio = audiosources[i];
            DOTween.To(() => audio.volume, x => audio.volume = x, 0, changeTime);

            //audio.Stop();
        }
    }
    public void startMusic(int i)
    {
        var audio = audiosources[i];
        audio.DOKill();
        //audio.time = 0;
        DOTween.To(() => audio.volume, x => audio.volume = x, bgmVolume, changeTime);
    }
    private void Awake()
    {
        audiosources = GetComponents<AudioSource>();
        
        startMorning();
    }

    public void startMorning()
    {
        stopAll();
        startMusic(0);
    }
    public void startEvening()
    {
        stopAll();
        startMusic(1);
    }
    public void playSound(AudioClip clip)
    {
        audiosources[2].PlayOneShot(clip);
    }
    public void playSFXRandom(AudioClip[] clips)
    {
        audiosources[2].PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
