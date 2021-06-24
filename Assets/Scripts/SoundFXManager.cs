using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundFxKey { Explosion }
public class SoundFXManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource mainAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip explosionA;
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
    private void Awake()
    {
        _instance = this;
        SetupExplosionSounds();
    }

    private void SetupExplosionSounds()
    {
        var explosionsArray = new AudioClip[]
        {
            explosionA,
        };
        soundFxToAudioClipMap.Add(SoundFxKey.Explosion, explosionsArray);
    }

    public static void PlayOneShot(SoundFxKey soundFxKey)
    {  
        if(false == TryGetRandomClip(soundFxKey, out AudioClip clip))
        {
            return;
        }

        Instance.mainAudioSource.PlayOneShot(clip);
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

}
