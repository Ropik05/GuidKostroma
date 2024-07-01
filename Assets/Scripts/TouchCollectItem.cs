using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCollectItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("Touch"))
        {
            Destroy(this.gameObject);
            DollControl.GameMode++;
        }
    }
}
