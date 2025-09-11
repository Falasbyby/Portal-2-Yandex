using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xuwu.FourDimensionalPortals.Demo;

public class MovePlatform : MonoBehaviour
{
    public Vector3 endPositionOffset = new Vector3(10, 0, 0); // Смещение относительно начальной позиции
    public float moveSpeed = 2.0f;
    public float stopTime = 2.0f;
    public LineRenderer lineRenderer;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingToEnd = true;
    private float timer = 0;
    public Material effectLine;
    public bool moveActive = false;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + endPositionOffset;
        
        lineRenderer.SetPosition(0,startPosition);
        lineRenderer.SetPosition(1,endPosition);
    }

    private void Update()
    {
        float offset = Time.time * -0.2f;
        Vector2 offsetVector = new Vector2(offset, 0); // Измените вектор с оффсетом по вашим потребностям

        effectLine.mainTextureOffset = offsetVector;
        
        
        if (!moveActive)
            return;
        if (movingToEnd)
        {
            // Двигаем платформу к конечной позиции
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPosition) < 0.1f)
            {
                // Если достигли конечной позиции, останавливаемся и ждем
                timer += Time.deltaTime;
                if (timer >= stopTime)
                {
                    movingToEnd = false;
                    timer = 0;
                }
            }
        }
        else
        {
            // Двигаем платформу к начальной позиции
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                // Если достигли начальной позиции, останавливаемся и ждем
                timer += Time.deltaTime;
                if (timer >= stopTime)
                {
                    movingToEnd = true;
                    timer = 0;
                }
            }
        }
    }
  
    private void OnDrawGizmos()
    {
        
       Vector3 startPositionGiz = transform.position;
      Vector3  endPositionGiz = startPositionGiz + endPositionOffset;
        // Рисуем круг (гизмос) в исходной точке
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(endPositionGiz, 0.5f);

    }
}