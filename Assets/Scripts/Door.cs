using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private PhysicsButton[] allbutton;
    [SerializeField] private Animator anim;
    public GameObject doorLine;
    private void Start()
    {
        allbutton = GetComponentsInChildren<PhysicsButton>();
    }

    private void Update()
    {
        
        bool allButtonsPressed = true;

        // Проверяем, если все кнопки в массиве нажаты
        for (int i = 0; i <allbutton.Length ; i++)
        {
            if (!allbutton[i].isPressed)
            {
                allButtonsPressed = false;
                break; // Если хотя бы одна кнопка не нажата, выходим из цикла
            }
        }

        if (allButtonsPressed ||allbutton.Length<=0 )
        {
            // Если все кнопки нажаты, выполните метод
            YourMethodToExecute();
        }
        else
        {
            // Иначе, выполните другой метод
            YourOtherMethodToExecute();
        }
    }

    private void YourMethodToExecute()
    {
        anim.SetBool("Door",true);
    }

    private void YourOtherMethodToExecute()
    {
        anim.SetBool("Door",false);
    }
   
}
