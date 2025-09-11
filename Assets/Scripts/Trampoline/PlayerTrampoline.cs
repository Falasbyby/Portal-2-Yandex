using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Xuwu.FourDimensionalPortals;

public class PlayerTrampoline : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public LayerMask trampolineLayer;

    private bool isJump = false;
    public float bounceForce = 109.0f;

    private void Update()
    {
        // Создаем луч, направленный вниз из текущей позиции персонажа
        Ray ray = new Ray(transform.position + new Vector3(0, 0.3f, 0), Vector3.down);
        RaycastHit hit;

        // Проверяем, с чем столкнулся луч
        if (Physics.Raycast(ray, out hit, raycastDistance,trampolineLayer))
        {
            if(hit.collider.GetComponent<Portal>() )
                return;
            
            if (hit.collider.GetComponent<BounceForce>() && !isJump)
            {
                Debug.Log("Рейкаст в jump");
                isJump = true;

                Rigidbody playerRigidbody = GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    // Определите направление движения игрока.
                    Vector3 bounceDirection = transform.up;
                    Vector3 bouncePlayer = -playerRigidbody.velocity * bounceForce;
                    StartCoroutine(TimerJump());
                    // Примените силу отскока в противоположном направлении движения игрока.
                    playerRigidbody.AddForce(bounceDirection * bouncePlayer.y, ForceMode.Impulse);
                }
            }
        }
        else
        {
        }

        // Рисуем линию рейкаста в редакторе Unity
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }

    private IEnumerator TimerJump()
    {
        yield return new WaitForSeconds(0.3f);
        isJump = false;
    }
}