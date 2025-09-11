using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using YG;

public class MobileInputManager : Singleton<MobileInputManager>
{
    [SerializeField] private CanvasGroup mobileInputContainer;
    [SerializeField] private CanvasGroup fireContainer;
    public bool TestInIspector = false;
    private bool activeContainer;
    private bool activeFireContainer = false;
    private bool Mobile = false;
    private int FirstCheckMobile = 0;
    public void ActiveMobileContainer()
    {
        activeContainer = !activeContainer;
        if (IsMobileDevice())
        {
           
            mobileInputContainer.alpha = activeContainer ? 1 : 0;
            mobileInputContainer.blocksRaycasts = activeContainer ? true : false;
        }
    }

    public void ActiveContainerFire(bool active)
    {
        if (IsMobileDevice())
        {
            activeFireContainer = active;
            fireContainer.alpha = activeFireContainer == false ? 0 : 1;
            fireContainer.blocksRaycasts = activeFireContainer;
        }
    }

  

 

   
    private void Start()
    {
        // Вызывает JavaScript для проверки сенсорности устройства
        Application.ExternalEval("checkForTouchDevice()");
    }

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();

#endif
    public bool IsMobileDevice()
    {
        if (FirstCheckMobile == 1)
           return Mobile;
        FirstCheckMobile = 1;
        
        
        var isMobile = false;
       
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif


        if (isMobile)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Mobile = true;
            return true;
        }
        else
        {
            if (TestInIspector)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            Mobile = TestInIspector;
            return TestInIspector;
        }
    }
}