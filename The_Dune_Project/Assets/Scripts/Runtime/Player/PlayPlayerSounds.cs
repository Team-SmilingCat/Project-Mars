using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlayerSounds : MonoBehaviour
{
    [Header("Names of audio sources")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource playerVoiceSource;
    [SerializeField] private AudioSource weaponSoundSource;

    [Tooltip("type of sounds")]
    public enum SoundType{
        VoiceAudioSource,
        WeaponAudioSource
    }

    [SerializeField] private float rayLenght;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask layer;
   

    [Header("sounds of footsteps")]
    [SerializeField] private List<FootStepsGroup> footstepSounds;

    [Header("Sound Effects")]
    [SerializeField] private List<SoundEffect> soundEffects;

    public void PlaySoundEffect(String nameOfAudio)
    {
        foreach(SoundEffect s in soundEffects)
        {
            if (s.name.Equals(nameOfAudio))
            {
                GetAudioSource(s.soundType).PlayOneShot(s.clip);
            }
        }
    }

    private AudioSource GetAudioSource(SoundType type)
    {
        if (type.Equals(SoundType.VoiceAudioSource))
        {
            return playerVoiceSource;
        }
        else if (type.Equals(SoundType.WeaponAudioSource))
        {
            return weaponSoundSource;
        }
        else
        {
            return audioSource;
        }
    }


    private void PlayFootStepSound()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + offset,
            -transform.up,
            out raycastHit,
            rayLenght,
            layer,
            QueryTriggerInteraction.Ignore
            ))
        {
            if (raycastHit.transform.gameObject.layer ==
                LayerMask.NameToLayer("hardsurface"))
            {
                audioSource.PlayOneShot(GetRandomClip(GetGroup()));
            }
        }
    }


    [Obsolete]
    private AudioClip GetRandomClip(AudioClip[] list)
    {
        return list[UnityEngine.Random.RandomRange(0, list.Length)];
    }

    private AudioClip[] GetGroup()
    {

        foreach (FootStepsGroup a in footstepSounds)
        {
            if (a.type.Equals("hardsurface"))
            {
                return a.stepSounds;
            }
            else
            {
                throw new Exception("missing sound group");
            }
        }

        return footstepSounds[0].stepSounds;
    }

    [Serializable]
    public struct FootStepsGroup
    {
        public string type;
        public AudioClip[] stepSounds;
    }

    [Serializable]
    public struct SoundEffect
    {
        public string name;
        public SoundType soundType;
        public AudioClip clip;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + offset, -transform.up * rayLenght);
        Gizmos.color = Color.green;
    }
}