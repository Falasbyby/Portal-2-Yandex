using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOrPcImage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer SpriteIconPc;
    [SerializeField] private SpriteRenderer SpriteIconMobile;

    void Start()
    {
        if(MobileInputManager.Instance.IsMobileDevice())
        {
            SpriteIconMobile.gameObject.SetActive(true);
            SpriteIconPc.gameObject.SetActive(false);
        }
        else
        {
            SpriteIconMobile.gameObject.SetActive(false);
            SpriteIconPc.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
