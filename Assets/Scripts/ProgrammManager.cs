using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager : MonoBehaviour
{

    public GameObject ARCamera;

    private ARRaycastManager ARRaycastManagerScript;

    private Vector2 TouchPosition;

    public GameObject EkatDoll;

    private GameObject GameDoll;

<<<<<<< Updated upstream
    public Transform DollPos;
    void Start()
    {
        GameDoll = Instantiate(EkatDoll, ARCamera.transform.position + new Vector3(1f, 0, 0), ARCamera.transform.rotation);
=======
    public static Transform VideoPoss;

    public GameObject Plane;

    public GameObject NextMenu;

    public static byte GameMode = 0;
    void Start()
    {
        PlaneMarker.SetActive(false);
        ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
        Button.SetActive(false);
        Plane.SetActive(false);
        NextMenu.SetActive(false);
        GameMode = 0;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        //ShowMarker();
        if(CheckDist() >= 0.3f)
        {
            MoveObjectTo();
        }
<<<<<<< Updated upstream

        GameDoll.transform.LookAt(ARCamera.transform);
=======
        if (GameMode == 1)
        {
            Instantiate(VideoPlane, hits[0].pose.position, ArCamera.transform.rotation);
            GameMode++;
        }
        if (GameMode == 3) 
        { 
         Plane.SetActive(true);
        }
        if(GameMode == 5)
        {
            NextMenu.SetActive(true);
        }
        VideoPoss = ARCamera.transform;
        TouchCollectItem();
>>>>>>> Stashed changes

    }
    private float CheckDist()
    {
<<<<<<< Updated upstream
        float dist = Vector3.Distance(GameDoll.transform.position,DollPos.transform.position);
        return dist;
=======
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
>>>>>>> Stashed changes
    }
    private void MoveObjectTo()
    {
        GameDoll.transform.position = Vector3.Lerp(GameDoll.transform.position, DollPos.position,1f* Time.deltaTime);
    }
}
