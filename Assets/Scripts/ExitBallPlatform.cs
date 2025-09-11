using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBallPlatform : MonoBehaviour
{
    [SerializeField] private MovePlatform movePlatform;
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform startPosLine;
    [SerializeField] private GameObject modelRotate;
    private void Start()
    {
        RaycastHit hit;

        if (Physics.Raycast(startPosLine.transform.position, startPosLine.transform.forward * 10, out hit))
        {
            lineRenderer.SetPosition(0,startPosLine.transform.position);
            lineRenderer.SetPosition(1,hit.point);
        }
       
    }

    private void Update()
    {
        modelRotate.transform.Rotate(new Vector3(0,90*Time.deltaTime,0));
    }

    public void Finish()
    {
        movePlatform.moveActive = true;
        lineRenderer.gameObject.SetActive(false);
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].Play();
        }
        GetComponent<AudioSource>().Play();
    }
}
