using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private float timerAsteroid;

    [SerializeField]
    private Vector2 minPos, maxPos;

    [SerializeField]
    private float initYPosition;

    [SerializeField]
    private Vector2 initTimeAsteroid;

    [SerializeField]
    private Vector2 asteroidDirection;

    [SerializeField]
    private Vector2 asteroidVelocity;

    [SerializeField]
    private float asteroidSizeMin;

    [SerializeField]
    private float asteroidSizeMax;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CreateAsteroid), 0.0f, timerAsteroid);
    }

    private void CreateAsteroid()
    {
        GameObject currentAsteroid = Instantiate(asteroidPrefab);
        float x = Random.Range(minPos.x, maxPos.x);
        float z = Random.Range(minPos.y, maxPos.y);
        currentAsteroid.transform.position = new Vector3(x, initYPosition, z);
        float size = Random.Range(asteroidSizeMin, asteroidSizeMax);
        currentAsteroid.transform.localScale = new Vector3(size, size, size);

        float activeTime = Random.Range(initTimeAsteroid.x, initTimeAsteroid.y);
        float asteroidV = Random.Range(asteroidVelocity.x, asteroidVelocity.y);
        currentAsteroid.GetComponent<Asteroid>().InitAsteroid(activeTime, asteroidDirection, asteroidV);
    }

}
