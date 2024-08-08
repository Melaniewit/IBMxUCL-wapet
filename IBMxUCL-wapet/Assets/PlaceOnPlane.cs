using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlacedPrefab;

    [SerializeField]
    private GameObject visualObject;

    private ARRaycastManager m_RaycastManager;
    private GameObject spawnedObject;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        Debug.Log("Touch detected at position: " + touchPosition);

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Debug.Log("Raycast hit plane");

            var hitPose = s_Hits[0].pose;

            if (spawnedObject == null)
            {
                Debug.Log("Instantiating object at: " + hitPose.position);
                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                // spawnedObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Adjust as needed
                Debug.Log("Object instantiated successfully");
            }
            else
            {
                Debug.Log("Moving object to: " + hitPose.position);
                spawnedObject.transform.position = hitPose.position;
            }

            if (visualObject != null)
            {
                visualObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit plane");
        }
    }
}
