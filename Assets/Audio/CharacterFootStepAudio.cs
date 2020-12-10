using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFootStepAudio : MonoBehaviour
{
    [SerializeField]
    AudioClip[] footStepSounds;

    [SerializeField]
    AudioSource footstepSource;



    public void PlayFootStep()
    {
        footstepSource.clip = footStepSounds[0];
        footstepSource.Play(); 
    }
}
