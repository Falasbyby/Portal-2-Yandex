using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Xuwu.FourDimensionalPortals.Demo;

public class SpeedPlatform : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public LayerMask speedPlatform;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), Vector3.down);
        RaycastHit hit;
    
        // Проверяем, с чем столкнулся луч
        if (Physics.Raycast(ray, out hit, raycastDistance,speedPlatform))
        {
            playerController.SuperSpeed();
            Debug.Log("Speed");
        }
        else
        {
            playerController.StopSuperSpeed();
        }

        // Рисуем линию рейкаста в редакторе Unity
      //  Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }

    
}
