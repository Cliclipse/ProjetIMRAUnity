using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceGame : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    private bool isPlaced = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (!raycastManager) return;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0) && !isPlaced)
        {
            isPlaced = true;

            if (Input.touchCount > 0)
            {
                PlaceObject(Input.GetTouch(0).position);
            }
            else
            {
                PlaceObject(Input.mousePosition);
            }
        }
    }
    void PlaceObject(Vector2 touchPosition) {
        var rayHits = new List<ARRaycastHit>();
        raycastManager.Raycast(touchPosition, rayHits, TrackableType.AllTypes);

        if (rayHits.Count > 0) {
            Vector3 hitPosePosition = rayHits[0].pose.position;
            Quaternion hitPoseRotation = rayHits[0].pose.rotation;
            Instantiate(raycastManager.raycastPrefab, hitPosePosition, hitPoseRotation);
        }
        StartCoroutine(SetIsPlacedToFalsWithDelay());
    }

    IEnumerator SetIsPlacedToFalsWithDelay() {
        yield return new WaitForSeconds(0.25f);
        isPlaced = false;
    }
}
