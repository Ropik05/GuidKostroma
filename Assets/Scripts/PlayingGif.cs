using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayingGif : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D[] frames;
    private float framesPerSecond = 12f;
    UnityEngine.AudioSource m_MyAudioSource;
    RawImage im = null;
    Renderer rend = null;
    void Start()
    {
        rend = GetComponent<Renderer>();
        im = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float index = Time.time*framesPerSecond;
        index = index % frames.Length;
        if (rend != null)
            rend.material.mainTexture = frames[(int)index];
        /*if (im == null)
        {
            int i = (int)(Time.time * 24 % frames.Length);
            im.material.mainTexture = frames[i++];
        }*/
    }
}
