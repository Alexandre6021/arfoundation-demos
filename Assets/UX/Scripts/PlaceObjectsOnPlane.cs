using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{

    public GameObject spawner;
    public GameObject shootScript;

    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedTurret;
    [SerializeField]
    GameObject m_PlacedRocket;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefabTurret
    {
        get { return m_PlacedTurret; }
        set { m_PlacedTurret = value; }
    }
    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefabRocket
    {
        get { return m_PlacedRocket; }
        set { m_PlacedRocket = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    [SerializeField]
    bool m_CanReposition = true;

    public bool canReposition
    {
        get => m_CanReposition;
        set => m_CanReposition = value;
    }

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {
                        Debug.Log("Turret and planes are placed!!!");
                        Spawner.Instance.setTurretPosition(hitPose.position);
                        Spawner.Instance.spawnRedPlane();
                        Spawner.Instance.spawnGreenPlane();
                        Spawner.Instance.spawnRainbowPlane();
                        Spawner.Instance.spawnTurtleShell();

                        spawnedObject = Instantiate(m_PlacedTurret, hitPose.position, hitPose.rotation);

                        shootScript.GetComponent<Shoot>().turret = spawnedObject;

                        m_NumberOfPlacedObjects++;
                    }
                    else if (m_NumberOfPlacedObjects >= m_MaxNumberOfObjectsToPlace)
                    {
                        Debug.Log("Rocket is placed!!!");

                        Spawner.Instance.setRocketPosition(hitPose.position);

                        spawnedObject = Instantiate(m_PlacedRocket, hitPose.position, hitPose.rotation);

                        m_NumberOfPlacedObjects++;
                    }
                    else
                    {
                        if (m_CanReposition)
                        {
                            spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        }
                    }
                    
                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
    }
}
