using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] private float jumpHeight  = 10f;
    [SerializeField] private Transform jumpTarget;

    private void Start()
    {
        //  jumpTarget.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                GetComponent<AudioSource>().Play();
                // Рассчитываем необходимую начальную скорость для достижения цели
                float gravity = Physics.gravity.magnitude;
                float initialVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);

                // Рассчитываем направление к цели
                Vector3 jumpDirection = (jumpTarget.position - transform.position).normalized;

                // Вычисляем силу отталкивания, используя начальную скорость и направление
                Vector3 jumpForce = jumpDirection * initialVelocity * playerRigidbody.mass;

                // Применяем силу к игроку
                playerRigidbody.AddForce(jumpForce, ForceMode.Impulse);
            }
        }
    }
}




