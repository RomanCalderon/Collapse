using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie : MonoBehaviour
{
    [SerializeField]
    Texture[] frames;

    [SerializeField]
    int framesPerSecond = 30;
    
    void Update()
    {
        int index = (int)(Time.time * framesPerSecond);

        if(index < frames.Length - 1)
            GetComponent<Renderer>().material.mainTexture = frames[index];
        else
            GetComponent<Renderer>().material.mainTexture = frames[frames.Length-1];
    }

}
