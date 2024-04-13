using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 current_pos;
    private Vector3 rotatedDir;
    private float sineWave;
    private float azimuth;

    private void Start()
    {
        azimuth = 0;
        rotatedDir = new Vector3(15 - UnityEngine.Random.Range(-10f, 10f), 30 - UnityEngine.Random.Range(-10f, 10f), 45 - UnityEngine.Random.Range(-10f, 10f));
        //rotatedDir = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f));
    }
    // Update is called once per frame
    void Update()
    {
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        transform.Rotate(rotatedDir * Time.deltaTime);
        azimuth += (float)Time.deltaTime;
        current_pos = transform.position;
        sineWave = 0.001f * (float)Math.Sin(azimuth); // in millimieters
        transform.Translate(0, sineWave, 0, Space.World);
    }
}

//    angle += (float)elapsed_time * 20;
//azimuth += (float)elapsed_time * 0.8f;
//    model.setTranslation(model.getTranslation().x, 10*sin(azimuth), model.getTranslation().z);
//model.rotate(angle * DEG2RAD, Vector3(0, 1, 0));
