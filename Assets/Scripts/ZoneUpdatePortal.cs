using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xuwu.FourDimensionalPortals.Demo;

public class ZoneUpdatePortal : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimerActiveCollider());
    }

    private IEnumerator TimerActiveCollider()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>())
        {
            PortalGun.Instance.GrabOff();
            other.GetComponent<Box>().RestartBox();
        }

        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().portalGun.ClosePortal();
        }
    }
}
