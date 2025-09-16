using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class StartMenuManager : MonoBehaviour
{
   
    [SerializeField] private Button newGameButton, optionsButton,continueGameButton;
    [SerializeField] private CanvasGroup startContainer, optionsContainer, loadContainer;
    [SerializeField] private CanvasGroup inputInfoContainer;
    [SerializeField] private Button   closeOptionsContainerButton;
    

    [SerializeField] private Scrollbar sliderSensitive;
    [SerializeField] private Button closeLoadContainer;
    [SerializeField] private Image barLoad;
    private const string PlayerPrefsKeyMouse = "MouseSensitivity";
   
    private CanvasGroup currentContainerActive;



    private bool inputOff = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstGameRun", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstGameRun", 1);
            
            continueGameButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("CurrentLevel", 0);
            PlayerPrefs.SetFloat("TimerLider", 0);
        }
        
        currentContainerActive = startContainer;
        newGameButton.onClick.AddListener(ActiveLoadContainerNewGame);
        continueGameButton.onClick.AddListener(ActiveLoadContainer);
        closeOptionsContainerButton.onClick.AddListener(delegate { ActiveContainer(startContainer); });
        optionsButton.onClick.AddListener(delegate { ActiveContainer(optionsContainer); });
        closeLoadContainer.onClick.AddListener(CloseLoad);
        
        float savedSensitivity = PlayerPrefs.GetFloat(PlayerPrefsKeyMouse, 0.2f);
        sliderSensitive.value = savedSensitivity;

        // Добавляем обработчик события для автосохранения чувствительности
        sliderSensitive.onValueChanged.AddListener(SaveSensitivity);


        //YandexGame.NewLBScoreTimeConvert("TimerLider1", PlayerPrefs.GetFloat("MaxTimerLider", 0));
        //leaderboardYg.UpdateLB();
    }

    private void Update()
    {
        if (IsMobileDevice() && !inputOff)
        {
            inputOff = true;
            inputInfoContainer.alpha = 0;
        }
    }

    private void CloseLoad()
    {
        loadSequence.Kill();
        ActiveContainer(startContainer);
    }

    private Sequence loadSequence;

    private void ActiveLoadContainer()
    {
        barLoad.fillAmount = 0;
        ActiveContainer(loadContainer);
        loadSequence = DOTween.Sequence()
            .Append(barLoad.DOFillAmount(0.3f, 1))
            .AppendInterval(1)
            .Append(barLoad.DOFillAmount(0.7f, 0.5f))
            .AppendInterval(1)
            .Append(barLoad.DOFillAmount(1, 2f)).OnComplete(() =>
            {
              
                StartGame();
            });
    }
    private void ActiveLoadContainerNewGame()
    {
        barLoad.fillAmount = 0;
        ActiveContainer(loadContainer);
        loadSequence = DOTween.Sequence()
            .Append(barLoad.DOFillAmount(0.3f, 1))
            .AppendInterval(1)
            .Append(barLoad.DOFillAmount(0.7f, 0.5f))
            .AppendInterval(1)
            .Append(barLoad.DOFillAmount(1, 2f)).OnComplete(() =>
            {
                PlayerPrefs.SetInt("CurrentLevel", 0);
                PlayerPrefs.SetFloat("TimerLider",0);
                StartGame();
            });
    }

    private void SaveSensitivity(float sensitivity)
    {
        // Сохраняем значение чувствительности в PlayerPrefs
        PlayerPrefs.SetFloat(PlayerPrefsKeyMouse, sensitivity);
        PlayerPrefs.Save();
    }

    

    public void ActiveContainer(CanvasGroup canvasGroup)
    {
        currentContainerActive.DOFade(0, 0.3f);
        currentContainerActive.blocksRaycasts = false;

        currentContainerActive = canvasGroup;
        currentContainerActive.DOFade(1, 0.3f);
        currentContainerActive.blocksRaycasts = true;
    }


    public void StartGame()
    {
        Fade.Instance.ActiveFade(true,1);
    }

 

   
#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();
#endif

    public bool InputTest = false;

    public bool IsMobileDevice()
    {
        var isMobile = false;

#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif


        bool deiveUser = Input.touchSupported;
        if (isMobile)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            return true;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return InputTest;
        }
    }
}