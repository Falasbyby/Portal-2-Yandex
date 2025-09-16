using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UiGame : Singleton<UiGame>
{
    [SerializeField] private CanvasGroup startContainer;
    [SerializeField] private Button startButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private Text timerTextSpeedRun;
    [SerializeField] private CanvasGroup finishContainer;
    [SerializeField] private Text timerRecord;
    [SerializeField] private Text maxTimerRecord;
    [SerializeField] private Button complitedButton;
    [SerializeField] private CanvasGroup levelNextContainer;
    [SerializeField] private Button nextLevelButton;

    public bool isGameActive = false;
    public float timerRun;

    private void Start()
    {
        complitedButton.onClick.AddListener(ComplitedGame);
        startButton.onClick.AddListener(StartGame);
        timerRun = YG2.saves.timerLider;
        string formattedTime = FormatTime(timerRun);

        nextLevelButton.onClick.AddListener(NextLevel);
        // Обновляем текст на таймере
        timerTextSpeedRun.text = formattedTime;
        StartCoroutine(TimerCursor());
    }

    private void NextLevel()
    {
        LevelController.Instance.NextLevel();
        Fade.Instance.ActiveFade(true, 1);
    }

    private void ComplitedGame()
    {
        Fade.Instance.ActiveFade(true, 0);
    }

    private IEnumerator TimerCursor()
    {
        yield return new WaitForSeconds(0.1f);
        ActiveCursore(true);
    }

    private void StartGame()
    {
        MobileInputManager.Instance.ActiveMobileContainer();
        ActiveCursore(false);
        isGameActive = true;
        startContainer.DOFade(0, 0.3f);
        startContainer.blocksRaycasts = false;
    }

    private void Update()
    {
        if (isGameActive)
        {
            timerRun += Time.deltaTime;

            // Форматируем время в формат "00:00:000"
            string formattedTime = FormatTime(timerRun);

            // Обновляем текст на таймере
            timerTextSpeedRun.text = formattedTime;
        }
    }

    private string FormatTime(float time)
    {
        // Преобразовываем время в минуты, секунды и миллисекунды
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        // Форматируем в строку в формате "00:00:000"
        string formattedTime = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        return formattedTime;
    }

    public void LoseActiveContainer()
    {
        MobileInputManager.Instance.ActiveMobileContainer();
        audioSource.clip = loseClip;
        audioSource.Play();

        isGameActive = false;
        Fade.Instance.ActiveFade(true, 1);
        //   YG2.saves.timerLider = timerRun;
    }

    private void ActiveCursore(bool active)
    {
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void WinActiveContainer()
    {
        MobileInputManager.Instance.ActiveMobileContainer();
        audioSource.clip = winClip;
        audioSource.Play();


        isGameActive = false;
        YG2.saves.timerLider = timerRun;
        if (LevelController.Instance.maxLevel)
        {
            Fade.Instance.ActiveFade(true,0);

        }
        else
        {
            levelNextContainer.alpha = 1;
            levelNextContainer.blocksRaycasts = true;
            ActiveCursore(true);
            YG2.SaveProgress();
        }
    }
}