using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInfo : Singleton<GrabInfo>
{
    [SerializeField] private GameObject icon;


    private void Start()
    {
        icon.SetActive(false);
    }

    public void ActiveIcon(bool active)
    {
        icon.SetActive(active);
    }
}
