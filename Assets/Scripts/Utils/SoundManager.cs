using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource _audioSource;

    public Audio BackgroundMusicIntro;
    public Audio BackgroundMusicLoop;
    public List<Audio> Asteroids;

    #region Init
    private void Awake()
    {
        if (instance == null || instance != this)
            instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayAudio(BackgroundMusicIntro);
        Invoke("PlayBackgroundLoop", BackgroundMusicIntro.Clip.length);
    }
    #endregion

    public void Stop()
    {
        _audioSource.Stop();
    }

    private void PlayBackgroundLoop()
    {
        PlayAudio(BackgroundMusicLoop);
    }

    private void PlayAudio(Audio audio)
    {
        _audioSource.volume = audio.Volume;
        _audioSource.loop = audio.Loop;

        if (_audioSource.loop)
        {
            _audioSource.clip = audio.Clip;
            _audioSource.Play();
        }
        else
            _audioSource.PlayOneShot(audio.Clip);
    }
}
