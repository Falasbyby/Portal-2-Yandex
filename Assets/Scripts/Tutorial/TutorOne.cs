using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class TutorOne : MonoBehaviour
{
    [SerializeField] private Animator animatorDoor;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    private bool trigger = false;
    public float firstTimer = 3;
    public float nextTimer;
    private void Start()
    {
    
        StartCoroutine(timerAnim());
    }

    private IEnumerator timerAnim()
    {
       

        if (YandexGame.EnvironmentData.language == "ru")
        {
            yield return new WaitForSeconds(firstTimer);
            audioSource.clip = audioClip[0];
            audioSource.Play();
            yield return new WaitForSeconds(nextTimer);
            if(animatorDoor)
                animatorDoor.SetBool("Door", true);
        }
        else
        {
            if(animatorDoor)
                animatorDoor.SetBool("Door", true);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trigger && YandexGame.EnvironmentData.language == "ru")
        {
            trigger = true;
            audioSource.clip = audioClip[1];
            audioSource.Play();
        }
      
    }
}