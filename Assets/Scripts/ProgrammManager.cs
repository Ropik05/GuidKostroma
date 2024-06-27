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

    public Transform DollPos;
    void Start()
    {
        GameDoll = Instantiate(EkatDoll, ARCamera.transform.position + new Vector3(1f, 0, 0), ARCamera.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //ShowMarker();
        if(CheckDist() >= 0.3f)
        {
            MoveObjectTo();
        }

        GameDoll.transform.LookAt(ARCamera.transform);

    }
    private float CheckDist()
    {
        float dist = Vector3.Distance(GameDoll.transform.position,DollPos.transform.position);
        return dist;
    }
    private void MoveObjectTo()
    {
        GameDoll.transform.position = Vector3.Lerp(GameDoll.transform.position, DollPos.position,1f* Time.deltaTime);
    }
}
