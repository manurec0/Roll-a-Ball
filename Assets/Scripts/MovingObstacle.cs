using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private float MaxDist;
    private float StartPos;
    public float direction;
    public float offset;

    private Vector3 currentPos;
    private float sineWave;
    private float azimuth;

    // Start is called before the first frame update
    void Start()
    {
        azimuth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        azimuth += (float)Time.deltaTime;
        currentPos = transform.position;
        sineWave = 0.04f * (float)Math.Sin(azimuth); // in millimieters
        transform.Translate(sineWave * direction, 0, 0, Space.World);
    }
}
