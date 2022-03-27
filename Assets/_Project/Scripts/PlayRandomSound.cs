using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSound : MonoBehaviour
{

    public bool playOnEnable;
    public List<AudioClip> clips;

    public float playRandomSound()
    {
        int i = Random.Range(0, clips.Count);
        GetComponent<AudioSource>().clip = clips[i];
        GetComponent<AudioSource>().Play();
        return clips[i].length;
    }

    private void OnEnable()
    {
        if (playOnEnable) playRandomSound();
    }

}
