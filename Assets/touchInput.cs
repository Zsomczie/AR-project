using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;

public class touchInput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private TMP_Text CubeText;
    [SerializeField] private TMP_Text SphereText;

    [SerializeField] GameObject CubePrefab;
    private ARRaycastManager ARRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private TrackableType Trackable = TrackableType.PlaneWithinPolygon;
    private void Start()
    {
        ARRaycastManager = GetComponent<ARRaycastManager>();
    }
    public void SingleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            var touchPos = ctx.ReadValue<Vector2>();
            debugText.text = touchPos.ToString();

            if (ARRaycastManager.Raycast(touchPos, hits, Trackable))
            {
                var cube = Instantiate(CubePrefab, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("CuteCube").Count() >1)
        {
            CubeText.text = "Detected cube pic";
        }
        if (GameObject.FindGameObjectsWithTag("CuteSphere").Count() > 1)
        {
            SphereText.text = "Detected sphere pic";
        }
    }
}
