using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFunctionalities
{
    public static void PlayRandomClip(AudioSource source, AudioClip[] clips, float pitchMin = 1f, float pitchMax = 1f)
    {
        if (clips.Length < 2) return;
        source.pitch = Random.Range(pitchMin, pitchMax);

        int newValue = Random.Range(0, clips.Length);


        while (source.clip == clips[newValue])
        {
            newValue = Random.Range(0, clips.Length);
        }


        source.clip = clips[newValue];
        source.Play();
    }
}