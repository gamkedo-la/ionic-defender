using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundFxKey { Explosion, LoopingLaser, PulseWave, WaveCleared}
public class SoundFXManager : MonoBehaviour
{
    [Header("OneShot Audio Sources")]
    [SerializeField] private AudioSource mainAudioSource;

    [Header("Loop Audio Sources")]
    [SerializeField] private AudioSource audioLoopOne;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip explosionA;
    [SerializeField] private AudioClip loopingLaser;
    [SerializeField] private AudioClip pulseWave;
    [SerializeField] private AudioClip waveCleared;
    private static SoundFXManager Instance
    {
        get
        {
            if(_instance == null)
            {
                // instead of logging an error, we could instantiate this as a prefab.
                Debug.LogError($"There is no object in the current scene with this script attached to it.");
            }

            return _instance;
        }
    }

    // variable backing up field so that we can print an error message
    private static SoundFXManager _instance; 
    private Dictionary<SoundFxKey, AudioClip[]> soundFxToAudioClipMap = new Dictionary<SoundFxKey, AudioClip[]>();
    private Dictionary<SoundFxKey, AudioSource> soundFxToLoopAudioClipMap = new Dictionary<SoundFxKey, AudioSource>();
    private void Awake()
    {
        _instance = this;
        SetupAudioLoopSources();
        SetupExplosionSounds();
        SetupLoopingLaserSound();
        SetupPulseWaveSounds();
        SetupWaveClearedSound();
    }

    private void SetupAudioLoopSources()
    {
        soundFxToLoopAudioClipMap.Add(SoundFxKey.LoopingLaser, audioLoopOne);
    }

    private void SetupExplosionSounds()
    {
        var audioClipArray = new AudioClip[]
        {
            explosionA,
        };
        soundFxToAudioClipMap.Add(SoundFxKey.Explosion, audioClipArray);
    }

    private void SetupPulseWaveSounds()
    {
        var audioClipArray = new AudioClip[]
        {
            pulseWave,
        };
        soundFxToAudioClipMap.Add(SoundFxKey.PulseWave, audioClipArray);
    }

    private void SetupLoopingLaserSound()
    {
        var audioClipArray = new AudioClip[]
        {
            loopingLaser,
        };

        soundFxToAudioClipMap.Add(SoundFxKey.LoopingLaser, audioClipArray);
    }

    private void SetupWaveClearedSound()
    {
        var audioClipArray = new AudioClip[]
        {
            waveCleared,
        };

        soundFxToAudioClipMap.Add(SoundFxKey.WaveCleared, audioClipArray);
    }

    public static void PlayOneShot(SoundFxKey soundFxKey)
    {  
        if(false == TryGetRandomClip(soundFxKey, out AudioClip clip))
        {
            return;
        }

        Instance.mainAudioSource.PlayOneShot(clip);
    }

    public static void StartLoopSound(SoundFxKey soundFxKey)
    {
        Debug.Log("LoopSound: Start");
        if(false == TryGetRandomClip(soundFxKey, out AudioClip clip))
        {
            return;
        }

        if(false == TryGetLoopAudioSource(soundFxKey, out AudioSource audioSource))
        {
            return;
        }

        if(false == audioSource.isPlaying)
        {
            Debug.Log("IsShooting: Starting looping sound");
            audioSource.clip = clip;
            audioSource.time = 0; // resets playback position to the start
            audioSource.Play();
        }
    }

    public static void StopLoopSound(SoundFxKey soundFxKey)
    {
        Debug.Log("LoopSound: Stop");

        if(false == TryGetLoopAudioSource(soundFxKey, out AudioSource audioSource))
        {
            return;
        }

        if(audioSource.isPlaying)
        {
            Debug.Log("IsShooting: Stopping Looping sound");
            audioSource.Pause();
        }
    }

    private static bool TryGetRandomClip(SoundFxKey soundFxKey, out AudioClip clip)
    {
        clip = null;
        if(false == Instance.soundFxToAudioClipMap.ContainsKey(soundFxKey))
        {
            Debug.LogError($"no audio clip found for sound fx key [{soundFxKey}] on gameobject [{Instance.gameObject.name}].");
            return false;
        }

        var clipArray = Instance.soundFxToAudioClipMap[soundFxKey];
        if(clipArray == null || clipArray.Length <= 0)
        {
            Debug.LogError($"audio clip array is null/empty for sound fx key [{soundFxKey}] on gameobject [{Instance.gameObject.name}].");
            return false;
        }

        clip = clipArray[Random.Range(0, clipArray.Length)];
        return true;
    }

    private static bool TryGetLoopAudioSource(SoundFxKey soundFxKey, out AudioSource audioSource)
    {
        audioSource = null;
        if(false == Instance.soundFxToLoopAudioClipMap.ContainsKey(soundFxKey))
        {
            Debug.LogError($"no audio source found for sound fx key [{soundFxKey}] on gameobject [{Instance.gameObject.name}].");
            return false;
        }

        audioSource = Instance.soundFxToLoopAudioClipMap[soundFxKey];
        return true;
    }

}
