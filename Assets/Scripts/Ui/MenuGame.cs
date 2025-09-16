using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xuwu.FourDimensionalPortals.Demo;
using YG;

public class MenuGame : Singleton<MenuGame>
{
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private CanvasGroup startContainer, optionsContainer;
    [SerializeField] private CanvasGroup currentContainer;
    [SerializeField] private Button closeOptionsContainerButton;
    [SerializeField] private Toggle[] togglesColorsAim;
    [SerializeField] private Image aimIcon;
    [SerializeField] private Color32[] aimColors;
    [SerializeField] private Scrollbar sliderSensitive;
    [SerializeField] private CanvasGroup aimContainer;
    // Удалены константы PlayerPrefs - теперь используется YG2.saves
    private int selectedAimIndex = 0;
    private CanvasGroup currentContainerActive;

    private int savedMapValue;
    private int savedBotsValue;
    private int savedRoundValue;

    public bool currentMenuActive = false;
    private PlayerController player;
    
    private void Start()
    {
        currentContainerActive = startContainer;

        closeOptionsContainerButton.onClick.AddListener(delegate { ActiveContainer(startContainer); });
        optionsButton.onClick.AddListener(delegate { ActiveContainer(optionsContainer); });
        backButton.onClick.AddListener(OpenCurrentContainer);
        restartButton.onClick.AddListener(UiGame.Instance.LoseActiveContainer);
        string savedName = string.IsNullOrEmpty(YG2.saves.playerName) ? "123" : YG2.saves.playerName;
      

        // Загружаем значение чувствительности из YG2.saves и устанавливаем его для слайдера
        float savedSensitivity = YG2.saves.sensitivity == 0 ? 0.2f : YG2.saves.sensitivity;
        sliderSensitive.value = savedSensitivity;

        // Добавляем обработчик события для автосохранения чувствительности
        sliderSensitive.onValueChanged.AddListener(SaveSensitivity);
        menuButton.onClick.AddListener(MenyActive);

        TogglesAimActive();
        aimIcon.color = aimColors[YG2.saves.colorAim];
        
        
      
        
    }

    private void MenyActive()
    {
        Fade.Instance.ActiveFade(true,0);
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenCurrentContainer();
        }
    }

    public void OpenCurrentContainer()
    {
     
        if(!UiGame.Instance.isGameActive)
            return;
        
        MobileInputManager.Instance.ActiveMobileContainer();
        
        currentMenuActive = !currentMenuActive;
        currentContainer.DOFade(currentMenuActive ? 1 : 0, 0.3f);
        currentContainer.blocksRaycasts = currentMenuActive;
        MobileInputManager.Instance.ActiveMobileContainer();
        if (!currentMenuActive)
        {
            // Выключить курсор
            
            
            startContainer.DOFade(1, 0.3f);
            startContainer.blocksRaycasts = true;
            optionsContainer.DOFade(0, 0.3f);
            optionsContainer.blocksRaycasts = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            aimContainer.alpha = 1;
            MobileInputManager.Instance.ActiveMobileContainer();
            
        }
        else
        {
            MobileInputManager.Instance.ActiveMobileContainer();
            aimContainer.alpha = 0;
            // Включить курсор
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void SaveSensitivity(float sensitivity)
    {
        // Сохраняем значение чувствительности в YG2.saves
        YG2.saves.sensitivity = sensitivity;
        YG2.SaveProgress();
        player.UpdateSensitivity();
    }

    private void SaveName(string inputText)
    {
        // Сохраняем введенный текст в YG2.saves
        YG2.saves.playerName = inputText;
        YG2.SaveProgress();
    }

    public void ActiveContainer(CanvasGroup canvasGroup)
    {
        currentContainerActive.DOFade(0, 0.3f);
        currentContainerActive.blocksRaycasts = false;

        currentContainerActive = canvasGroup;
        currentContainerActive.DOFade(1, 0.3f);
        currentContainerActive.blocksRaycasts = true;
    }


    private void TogglesAimActive()
    {
        for (int i = 0; i < togglesColorsAim.Length; i++)
        {
            int index = i; // Скопируем индекс для замыкания

            // Установите начальное значение только для тогла с индексом 0
            if (i == YG2.saves.colorAim)
            {
                togglesColorsAim[i].isOn = true;
            }

            togglesColorsAim[i].onValueChanged.AddListener(isOn => OnToggleAimChanged(index, isOn));
        }
    }

    private void OnToggleAimChanged(int index, bool isOn)
    {
        if (isOn)
        {
            // Выключаем все тоглы, кроме выбранного
            for (int i = 0; i < togglesColorsAim.Length; i++)
            {
                if (i != index)
                {
                    togglesColorsAim[i].isOn = false;
                }
            }

            // Обновляем выбранный индекс
            selectedAimIndex = index;
            aimIcon.color = aimColors[index];
            // Сохраняем выбранный индекс в YG2.saves
            YG2.saves.colorAim = selectedAimIndex;
            YG2.SaveProgress();
        }
    }
}