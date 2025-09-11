using System;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Rotate(new Vector3(0,0,10*speed));
    }
}