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

    [SerializeField] private Button newGameButton, optionsButton, continueGameButton, levelButton;
    [SerializeField] private CanvasGroup startContainer, optionsContainer, loadContainer;
    [SerializeField] private CanvasGroup inputInfoContainer;
    [SerializeField] private Button closeOptionsContainerButton, closeLevelContainerButton;
    [SerializeField] private CanvasGroup levelContainer;
    [SerializeField] private LevelGridManager levelGridManager; // Ссылка на менеджер сетки уровней


    [SerializeField] private Scrollbar sliderSensitive;
    [SerializeField] private Button closeLoadContainer;
    [SerializeField] private Image barLoad;
    // Удален PlayerPrefsKeyMouse - теперь используется YG2.saves.sensitivity

    private CanvasGroup currentContainerActive;



    private bool inputOff = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
        if (YG2.saves.maxOpenLevel == 0)
        {
            continueGameButton.gameObject.SetActive(false);
            YG2.saves.currentLevel = 0;
            YG2.saves.maxOpenLevel = 0; // Устанавливаем первый уровень как открытый
                                        //  YG2.saves.timerLider = 0;
        }

        currentContainerActive = startContainer;
        newGameButton.onClick.AddListener(ActiveLoadContainerNewGame);
        continueGameButton.onClick.AddListener(ActiveLoadContainer);
        closeOptionsContainerButton.onClick.AddListener(delegate { ActiveContainer(startContainer); });
        optionsButton.onClick.AddListener(delegate { ActiveContainer(optionsContainer); });
        closeLoadContainer.onClick.AddListener(CloseLoad);
        levelButton.onClick.AddListener(delegate { ActiveContainer(levelContainer); });
        closeLevelContainerButton.onClick.AddListener(delegate { ActiveContainer(startContainer); });
        float savedSensitivity = YG2.saves.sensitivity == 0 ? 0.2f : YG2.saves.sensitivity;
        sliderSensitive.value = savedSensitivity;

        // Добавляем обработчик события для автосохранения чувствительности
        sliderSensitive.onValueChanged.AddListener(SaveSensitivity);


        //YandexGame.NewLBScoreTimeConvert("TimerLider1", YG2.saves.maxTimerLider);
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

                YG2.saves.currentLevel = 0;
                YG2.saves.maxOpenLevel = 0; // Сбрасываем максимальный открытый уровень
                YG2.saves.timerLider = 0;
                YG2.SaveProgress();
                StartGame();
            });
    }

    private void SaveSensitivity(float sensitivity)
    {
        // Сохраняем значение чувствительности в YG2.saves
        YG2.saves.sensitivity = sensitivity;
        YG2.SaveProgress();
    }



    public void ActiveContainer(CanvasGroup canvasGroup)
    {
        currentContainerActive.DOFade(0, 0.3f);
        currentContainerActive.blocksRaycasts = false;

        currentContainerActive = canvasGroup;
        currentContainerActive.DOFade(1, 0.3f);
        currentContainerActive.blocksRaycasts = true;

        // Если открываем контейнер уровней, обновляем состояние кнопок
        if (canvasGroup == levelContainer && levelGridManager != null)
        {
            levelGridManager.UpdateAllButtons();
        }
    }


    public void StartGame()
    {
        Fade.Instance.ActiveFade(true, 1);
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