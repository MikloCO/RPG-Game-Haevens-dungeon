using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonLocomotionSound : MonoBehaviour
{

    public AudioSource LocomotionSource;
    public AudioSource AttackSource;

    public AudioClip[] FootStepSFX;
    public AudioClip[] AttackSFX;
    public AudioClip[] CharacterSounds;

    public Vector2 PitchRange = new Vector2(0.95f, 1.05f);

    public void Step()
    {
        AudioFunctionalities.PlayRandomClip(LocomotionSource, FootStepSFX, PitchRange.x, PitchRange.y);
    }

    public void Hit()
    {
        AudioFunctionalities.PlayRandomClip(AttackSource, AttackSFX, PitchRange.x, PitchRange.y);
    }

    public void Death()
    {
        AttackSource.PlayOneShot(CharacterSounds[0]);
        
        //   AudioFunctionalities.PlayRandomClip(AttackSource, CharacterSounds, PitchRange.x, PitchRange.y);
    }
}

