using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Singleton instance of this class
    public static Spawner Instance { get; private set; }

    //References to planes prefab
    public GameObject redPlane;
    public GameObject greenPlane;
    public GameObject rainbowPlane;

    //Turret position
    private Vector3 turretPosition;
    //Rocket position
    private Vector3 rocketPosition;

    /// <summary>
    /// Create the singleton instance before starting the game
    /// </summary>
    private void Awake()
    {
        // If there is an instance, and it's not this one, delete it
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            //Set this instance as the singleton instance
            Instance = this;
        }
    }

    public void setTurretPosition(Vector3 p_turretPosition)
    {
        turretPosition = p_turretPosition;
    }

    public void setRocketPosition(Vector3 p_RocketPosition)
    {
        p_RocketPosition.x += 2.0f;

        rocketPosition = p_RocketPosition;
    }

    public void spawnPlane(GameObject plane)
    {
        //Create an empty gameobject to work with transform
        GameObject planeSpawnPoint = new GameObject();

        //Random height to add from the turret position
        float extraHeight = Random.Range(0.5f, 1.5f);

        //Random depth to add from the turret position
        float extraDepth = Random.Range(0.5f, 1.5f);

        //Create the spawn position
        Vector3 planeSpawnPosition = new Vector3(0.0f, this.turretPosition.y + extraDepth, this.turretPosition.z + extraHeight);

        //Set the position of the spawn
        planeSpawnPoint.transform.position = planeSpawnPosition;

        //Instance the plane at the spawn point
        Instantiate(plane, planeSpawnPoint.transform.position, Quaternion.identity);
    }

    //Spawn the rocket at the rocket position
    public void spawnRocket(GameObject rocket)
    {
        //Instance the rocket at the rocket position
        Instantiate(rocket, rocketPosition, Quaternion.identity);
    }

    public void spawnRedPlane()
    {
        spawnPlane(redPlane);
    }

    public void spawnGreenPlane()
    {
        spawnPlane(greenPlane);
    }

    public void spawnRainbowPlane()
    {
        spawnPlane(rainbowPlane);
    }

    public void spawnRedPlaneAfterTime(float time)
    {
        Invoke("spawnRedPlane", time);
    }

    public void spawnGreenPlaneAfterTime(float time)
    {
        Invoke("spawnGreenPlane", time);
    }

    public void spawnRainbowPlaneAfterTime(float time)
    {
        Invoke("spawnRainbowPlane", time);
    }
}
