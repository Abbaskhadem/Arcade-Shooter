using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
    public AnimationCurve Curve;
    public float ShakeIndensity;
    public float ShakeImplitiude;
    public float _startTime;
    private Vector3 Shakepos;
    private Vector3 Shakerot;
    private Vector3 Origin;

    public void Shake()
    {
        _startTime = Time.time;
    }

    public void Awake()
    {
        Origin = transform.position;
    }
void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
        var xPos = (Time.time) * ShakeIndensity+10;
        var Ypos = (Time.time) * ShakeIndensity + 100;
        Shakepos = new Vector3((Mathf.PerlinNoise(xPos, 1) - 0.5f) * ShakeIndensity,
            (Mathf.PerlinNoise(Ypos, 1) - 0.5f) * ShakeIndensity, 0)*Curve.Evaluate(Time.time-_startTime);
        transform.position = Origin + Shakepos;

    }
}
