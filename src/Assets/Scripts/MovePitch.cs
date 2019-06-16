using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MovePitch : MonoBehaviour
{
    AudioSource ThisAudioSource;
    private GameObject player;
    private Vector3 playerPos;
    private Vector3 soundPos;
    AudioSource audioSource;
    public int startDist = 10;
    private float dist;
    public float startingPitch = 1f;
    public int maxPitch = 1;
    public int minPitch = 0;
    public bool inverse = false;
    private float pitchValue;
    private float perc;
    void Start()
    {
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();

        audioSource.pitch = startingPitch;
    }

    void Update()
    {
        playerPos = player.transform.position;
        dist = Vector3.Distance(playerPos, transform.position);
        if(dist < startDist) {
            perc = Mathf.InverseLerp(0, startDist, dist);
            if(inverse) {
                pitchValue = Mathf.Lerp(maxPitch, minPitch, perc);
                audioSource.pitch = pitchValue;
            } else {
                pitchValue = Mathf.Lerp(minPitch, maxPitch, perc);
                audioSource.pitch = pitchValue;
            }
        }
    }
}
