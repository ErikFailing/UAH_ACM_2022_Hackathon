using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSound : MonoBehaviour
{
    public List<AudioClip> clips;

    public void playRandomSound()
    {
        int i = Random.Range(0, clips.Count);
        GetComponent<AudioSource>().clip = clips[i];
        GetComponent<AudioSource>().Play();
    }

}
