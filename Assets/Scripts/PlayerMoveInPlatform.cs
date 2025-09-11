using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInPlatform : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public LayerMask platformLayer;

    private bool isOnPlatform = false;
    private Transform currentPlatform = null;

    private void Update()
    {
        // Создаем луч, направленный вниз из текущей позиции персонажа
        Ray ray = new Ray(transform.position + new Vector3(0,0.3f,0), Vector3.down);
        RaycastHit hit;

        // Проверяем, с чем столкнулся луч
        if (Physics.Raycast(ray, out hit, raycastDistance, platformLayer))
        {
            Debug.Log("Рейкаст в платформу");
            // Если столкнулись с платформой, то двигаемся за ней
            isOnPlatform = true;
            currentPlatform = hit.transform;
            transform.parent = currentPlatform.transform;
        }
        else
        {
            if (isOnPlatform)
            {
                // Если не столкнулись с платформой, то прекращаем движение за ней
                isOnPlatform = false;
                transform.parent = null;
                currentPlatform = null;
            }
        }

        // Рисуем линию рейкаста в редакторе Unity
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }

    // Метод для движения вместе с платформой
}