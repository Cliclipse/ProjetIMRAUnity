using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceGameOnPlane : MonoBehaviour
{
    [SerializeField] private GameObject gamePrefab;

    private ARRaycastManager _raycastManager;
    private GameObject _spawnedGame;
    private static readonly List<ARRaycastHit> Hits = new();

    void Awake()
        => _raycastManager = GetComponent<ARRaycastManager>();

    void Update()
    {
        if (Input.touchCount == 0) return;

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        if (!_raycastManager.Raycast(touch.position, Hits,
                TrackableType.PlaneWithinPolygon)) return;

        var pose = Hits[0].pose;

        if (_spawnedGame == null)
            _spawnedGame = Instantiate(gamePrefab,
                pose.position, pose.rotation);
    }
}