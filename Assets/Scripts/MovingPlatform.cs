using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private int distance;

    private Vector3 initPosition;

    private Vector3 switchPosition;

    private bool switchActive = false;

    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;


    private void Start()
    {
        initPosition = transform.position;
    }


    private void Update()
    {
        //transform.position = transform.position + (dir * velocity * Time.deltaTime);
        //float d = Vector3.Distance(transform.position, initPosition); 
        //if (d > distance)
        //{
        //    dir *= -1;
        //}

        if (switchActive)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            Vector3 pos = Vector3.Lerp(initPosition, switchPosition, interpolationRatio);
            transform.position = pos;
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
            if (elapsedFrames >= interpolationFramesCount)
            {
                switchActive = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

    public void ActivePlatform(Vector3 _switchPosition)
    {
        switchActive = true;
        switchPosition = _switchPosition;
    }
}
