using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xuwu.FourDimensionalPortals.Demo;

public class Lava : MonoBehaviour
{
    [Header("Material Animation")]
    public Material lavaMaterial;
    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.3f;
    
    private Vector2 currentOffset;

    private void Start()
    {
        scrollSpeedX = UnityEngine.Random.Range(-0.2f, 0.2f);
        scrollSpeedY = UnityEngine.Random.Range(-0.2f, 0.2f);
        if (lavaMaterial == null)
        {
            // Если материал не назначен, попробуем получить его из Renderer
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                lavaMaterial = renderer.material;
            }
        }
        
        currentOffset = Vector2.zero;
    }

    private void Update()
    {
        if (lavaMaterial != null)
        {
            // Обновляем offset материала
            currentOffset.x += scrollSpeedX * Time.deltaTime;
            currentOffset.y += scrollSpeedY * Time.deltaTime;
            
            // Применяем новый offset к материалу
            lavaMaterial.mainTextureOffset = currentOffset;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<PlayerController>())
        {
            UiGame.Instance.LoseActiveContainer();
        }

        if (other.collider.GetComponent<Box>())
        {
            other.collider.GetComponent<Box>().RestartBox();
        }
    }
}
