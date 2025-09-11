using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    public void RestartBox()
    {
        transform.position = startPos;
    }
}
