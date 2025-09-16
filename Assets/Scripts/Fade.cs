using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Fade : Singleton<Fade>
{
    [SerializeField] private Image outline;
    [SerializeField] private Color32[] color32Outline;
    private void Start()
    {
        outline.color = color32Outline[0];
        transform.localScale = new Vector3(2,2,2);
        transform.DOScale(0, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            
            //   YandexGame.FullscreenShow();
            
        });
    }

    public void ActiveFade(bool active,int level)
    {
        outline.color = color32Outline[1];
        transform.DOScale(active ? 2 : 0, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            
            
            SceneManager.LoadScene(level);
        });
     
    }
}
