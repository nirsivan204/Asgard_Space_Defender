using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMGR : MonoBehaviour
{
    [Serializable]
    public class SoundTypeAndRef
    {
        public SoundTypes SoundType;
        public AudioClip AudioClipRef;
    }

    [Serializable]
    public class SoundSourceAndRef
    {
        public AudioSourceTypes SourceType;
        public AudioSource AudioSourceRef;
    }


    [SerializeField]
    private List<SoundTypeAndRef> SoundTypeAndRefList = new List<SoundTypeAndRef>();

    [SerializeField]
    private List<SoundSourceAndRef> SoundSourceAndRefList = new List<SoundSourceAndRef>(); // should be a dictionary for better run time


    public enum SoundTypes
    {
        None = 0,
        //ships sound effects

        CannonShoot = 1,
        ShipHurt = 2,

        //other sound effects
        Boom = 101,

        // UI_Sounds

        // Gameplay Sounds
        Win = 301,
        Lose = 302,
        Launch = 303,
        // Music
        BG_Music = 400,
        //other
    }

    public enum AudioSourceTypes
    {
        None,
        ShipEffects,
        SFX,
        UI,
        Gameplay,
        Music,
    }

    public void Play_Sound(SoundTypes soundType, bool isLoop = false)
    {
        AudioClip clip = GetAudioClip(soundType);

        AudioSource source = GetAudioSource(soundType);

        source.loop = isLoop;

        Play_Sound(clip, source);

    }

    public void Stop_Sound(SoundTypes soundType)
    {
        AudioSource source = GetAudioSource(soundType);

        Stop_Sound(source);

    }

    private void Play_Sound(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }

    private void Stop_Sound(AudioSource source)
    {
        source.Stop();
    }


    private AudioSource GetAudioSource(SoundTypes soundType)
    {
        switch (soundType)
        {
            case SoundTypes.None:
                break;
            case SoundTypes.Boom:
                return GetAudioSourceByType(AudioSourceTypes.SFX);
            case SoundTypes.CannonShoot:
            case SoundTypes.ShipHurt:
                return GetAudioSourceByType(AudioSourceTypes.ShipEffects);
            case SoundTypes.BG_Music:
                return GetAudioSourceByType(AudioSourceTypes.Music);
            case SoundTypes.Win:
            case SoundTypes.Lose:
            case SoundTypes.Launch:
            default:
                return GetAudioSourceByType(AudioSourceTypes.Gameplay);
        }

        return null;
    }

    private AudioSource GetAudioSourceByType(AudioSourceTypes audioSourceType)
    {
        for (int i = 0; i < SoundSourceAndRefList.Count; i++)
        {
            if (SoundSourceAndRefList[i].SourceType == audioSourceType)
            {
                return SoundSourceAndRefList[i].AudioSourceRef;
            }
        }

        return null;
    }

    private AudioClip GetAudioClip(SoundTypes soundType)
    {
        for (int i = 0; i < SoundTypeAndRefList.Count; i++)
        {
            if (SoundTypeAndRefList[i].SoundType == soundType)
                return SoundTypeAndRefList[i].AudioClipRef;
        }

        return null;
    }

    public void AddPitch(SoundTypes soundType, float addedPitch)
    {
        AudioSource source = GetAudioSource(soundType);
        source.pitch += addedPitch;
    }
}
