using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowPathAndDestroyRocket : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 1.0f;
    float distanceTravelled;
    public GameObject m_Rocket;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_Rocket; }
        set { m_Rocket = value; }
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        //Destroy the rocket when it reaches the end of the path
        if (distanceTravelled >= pathCreator.path.length)
        {
            Destroy(this.gameObject);
        }
    }
}
