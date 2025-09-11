using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BallPlatform : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Ball ballPrefab; // Префаб шара
    [SerializeField] private GameObject spriteHit;
    [SerializeField] private ParticleSystem effectSpawn;
    private Ball currentBall; // Текущий активный шар

    private AudioSource audioSource; 
    private void Start()
    {
        audioSource=  GetComponent<AudioSource>();
        RaycastHit hit;

        if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit))
        {
            float distanceToMoveBack = 0.005f; // Укажите желаемое расстояние от попадания
            Vector3 newPosition = hit.point - spawnPoint.transform.forward * distanceToMoveBack;
            spriteHit.transform.position = newPosition;
            // Повернуть спрайт относительно нормали попадания
            spriteHit.transform.rotation = Quaternion.LookRotation(-hit.normal);

            // Если вам нужно отрегулировать угол поворота спрайта, вы можете добавить дополнительные углы в Quaternion.Euler
            //  spriteHit.transform.rotation *= Quaternion.Euler(0, 90, 0); 
        }
        StartCoroutine(TimerSpawn());
    }

  

    private void SpawnBall()
    {
        if (currentBall == null )
        {
            audioSource.Play();
            effectSpawn.Play();
            currentBall = Instantiate(ballPrefab, spawnPoint.transform.position,spawnPoint.transform.rotation);
            
        }
       
    }

    private IEnumerator TimerSpawn()
    {
        while (true)
        {
            SpawnBall(); 
            yield return new WaitForSeconds(4);
            
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + spawnPoint.transform.forward * 100f);
    }
}