using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    private Vector3 curPos;
    private Vector3 lastPos;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
        if (curPos == lastPos)
        {
            m_MyAudioSource.volume = 0;
        } else
        {
            m_MyAudioSource.volume = 1;
        }
        lastPos = curPos;
    }
}
