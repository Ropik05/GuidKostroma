using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyObject : MonoBehaviour
    {
        public UnityEngine.Video.VideoPlayer player;

        public GameObject CollectItem;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (player.frame > 0 && !player.isPlaying)
            {
                DollControl.GameMode++;
                Instantiate(CollectItem, this.gameObject.transform.position + new Vector3(0, 0, 0.1f), DollControl.VideoPoss.rotation);
                DoDestroy();
            }
            this.gameObject.transform.LookAt(DollControl.VideoPoss.position);

        }

        public void DoDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}
