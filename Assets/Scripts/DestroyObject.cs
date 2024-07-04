using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyObject : MonoBehaviour
    {
        public UnityEngine.AudioSource m_MyAudioSource;

        public GameObject CollectItem;
        // Start is called before the first frame update
        void Start()
        {
            m_MyAudioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

            if (!m_MyAudioSource.isPlaying)
            {
                Quest.QuestR = true;
                //Instantiate(CollectItem,this.gameObject.transform.position + new Vector3(0,0,0.2f), Quest.VideoPoss.rotation);
                DoDestroy();
            }
           // this.gameObject.transform.LookAt(Quest.VideoPoss.position);

        }

        public void DoDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}
