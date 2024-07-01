using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayingGif : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D[] frames;
    Renderer im = null;
    void Start()
    {
        im = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (im == null)
        {
            int i = (int)(Time.time * 24 % frames.Length);
            im.material.mainTexture = frames[i++];
        }
    }
}
