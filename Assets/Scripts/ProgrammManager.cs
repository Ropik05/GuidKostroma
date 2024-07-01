using NUnit.Framework.Internal.Execution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DollControl : MonoBehaviour
{

    public GameObject ARCamera;

    [SerializeField] private Camera ArCamera;

    public GameObject Button;

    private ARRaycastManager ARRaycastManagerScript;
    public GameObject PlaneMarker;

    private Vector2 TouchPosition;

    public GameObject VideoPlane;

    public GameObject CollectItem;

    private GameObject GameVideo;

    public static Transform VideoPoss;

    public static byte GameMode = 0;
    void Start()
    {
        PlaneMarker.SetActive(false);
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            PlaneMarker.transform.position = hits[0].pose.position;
            PlaneMarker.SetActive(true);
            if (GameMode == 0)
            {
                Button.SetActive(true);
            }
        }
        if (GameMode == 1)
        {
            GameVideo = Instantiate(VideoPlane, hits[0].pose.position, ArCamera.transform.rotation);
            GameMode++;
        }
        VideoPoss = ARCamera.transform;
        TouchCollectItem();

    }

    void TouchCollectItem()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = ArCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("UnTouch"))
                    {
                        hitObject.collider.gameObject.tag = "Touch";
                    }
                }
            }
        }


    }
}