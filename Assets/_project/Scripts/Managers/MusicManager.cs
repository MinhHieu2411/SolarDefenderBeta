using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] AudioMixerSnapshot _patrolMusicSnapshot, _combatMusicSnapshot;
    [SerializeField] AudioClip[] _patrolMusic, _combatMusic;
    [SerializeField] AudioSource _patrolAudioSource, _combatAudioSource;
    int _patrolMusicIndex, _combatMusicIndex;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void PlayCombatMusic()
    {
        _combatAudioSource.Stop();
        _combatAudioSource.clip = _combatMusic[_combatMusicIndex];
        _combatAudioSource.Play();
        _combatMusicSnapshot.TransitionTo(1f);
        _combatMusicIndex = (_combatMusicIndex + 1) % _combatMusic.Length;
    }

    public void PlayPatrolMusic()
    {
        _patrolAudioSource.Stop();
        _patrolAudioSource.clip = _patrolMusic[_patrolMusicIndex];
        _patrolAudioSource.Play();
        _patrolMusicSnapshot.TransitionTo(1f);
        _patrolMusicIndex = (_patrolMusicIndex + 1) % _patrolMusic.Length;
    }
}
