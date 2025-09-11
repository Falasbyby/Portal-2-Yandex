using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xuwu.FourDimensionalPortals.Demo;

public class Lava : MonoBehaviour
{
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
