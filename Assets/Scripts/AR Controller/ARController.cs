using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{

    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Slider slider;
    [SerializeField] private ARRaycastManager m_RaycastManager;
    [SerializeField] private GameObject spawnablePrefab;
    [SerializeField] private GameObject placementIndicatorr;
    private GameObject placementIndicator;
    
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    private Camera arCam;
    private GameObject spawnedObject;
    private TouchHeliInput touchHeliInput;

    private bool isSpawned = false;
    


    private void Awake()
    {
        placementIndicator = Instantiate(placementIndicatorr);
        placementIndicator.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        
    }

   

    // Update is called once per frame
    void Update()
    {
        HandlePlaneDetectionAndPlacement();
        UpdatePlacementIndicator();
        
    }

    void HandlePlaneDetectionAndPlacement()
    {
        if (Input.touchCount == 0)
            return;

        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position,m_Hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Spawnnable")
                    {
                        spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(m_Hits[0].pose.position);
                    }
                }
            }else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {

                if (isSpawned)
                    return;
                
                spawnedObject.transform.position = m_Hits[0].pose.position;
            }

            /*if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }*/
        }
        
    }

    void UpdatePlacementIndicator()
    {

        if (isSpawned)
        {
            placementIndicator.SetActive(false);
            return;
        }


        var screenCenter = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        m_RaycastManager.Raycast(screenCenter, hits);

        bool poseIsValid = hits.Count > 0;

        if (poseIsValid)
        {
            Pose pose = hits[0].pose;

            var cameraForward = arCam.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0,cameraForward.z).normalized;
            pose.rotation = Quaternion.LookRotation(cameraBearing);
            
            
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(pose.position,pose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        if (isSpawned)
            return;
        
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        
        //Start the engine and rotor of fans immediately after spawning
        touchHeliInput.HandleThrottle(1f);
        touchHeliInput.HandleThrottle(1f);
        touchHeliInput.HandleThrottle(1f);
        
        isSpawned = true;
        
       spawnedObject.transform.Find("HeliPad").gameObject.SetActive(false);
    }
    
    // Handle button click
    
    private void FixedUpdate()
    {
        // Listen to the Joystick events here
        JoyStickListener();
    }

    void JoyStickListener()
    {
        if (spawnedObject == null)
            return;
        
        
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        float absoluteHorizontal = Mathf.Abs(horizontal);
        slider.value = absoluteHorizontal;
        
        /*TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();*/
        touchHeliInput.HandleVerHorTouch(vertical,horizontal);
        touchHeliInput.HandleCyclic();
        
        


    }

    public void ThrottleStartClick()
    {
        if (spawnedObject == null)
            return;
        
        
        //TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        touchHeliInput.HandleThrottle(1f);
        
    }

    public void ThrottleStopClick()
    {
        
        if (spawnedObject == null)
            return;
        
        //TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        touchHeliInput.HandleThrottle(-1f);
    }

    public void CollectiveUp()
    {
        
        if (spawnedObject == null)
            return;
        
        //TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        touchHeliInput.HandleCollective(0.05f);
    }

    public void CollectiveDown()
    {
        if (spawnedObject == null)
            return;
        
        //TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        touchHeliInput.HandleCollective(-0.05f);
    }

    public void PedalRight()
    {
        if (spawnedObject == null)
            return;
        
        //TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        touchHeliInput.HandlePedal(0.3f);
    }

    public void PedalLeft()
    {
        if (spawnedObject == null)
            return;
        
        //TouchHeliInput touchHeliInput = spawnedObject.GetComponentInChildren<TouchHeliInput>();
        touchHeliInput.HandlePedal(-0.3f);
    }
}
