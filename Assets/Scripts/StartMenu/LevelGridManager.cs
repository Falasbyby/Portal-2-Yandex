using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Portal_Git.Assets.Scripts.StartMenu
{
    public class LevelGridManager : MonoBehaviour
    {
        [Header("Настройки сетки")]
        [SerializeField] private ButtonLevel buttonLevelPrefab; // Префаб кнопки уровня
        [SerializeField] private Transform gridParent; // Родительский объект для Grid Layout
        
        [Header("Настройки сетки")]
        [SerializeField] private int totalLevels = 37; // Общее количество уровней
        [SerializeField] private Button startGameButton;
        private List<ButtonLevel> levelButtons = new List<ButtonLevel>();

        
        private void Start()
        {
            CreateLevelButtons();
            startGameButton.onClick.AddListener(StartGame);
        }
        
        private void StartGame()
        {
            Fade.Instance.ActiveFade(true,1);
        }

        private void CreateLevelButtons()
        {
           
            
            // Создаем кнопки для каждого уровня
            for (int i = 0; i <= totalLevels; i++)
            {
                CreateLevelButton(i);
            }
        }
        
        private void CreateLevelButton(int levelNumber)
        {
            if (buttonLevelPrefab == null)
            {
                Debug.LogError("ButtonLevelPrefab не назначен!");
                return;
            }
            
            if (gridParent == null)
            {
                Debug.LogError("GridParent не назначен!");
                return;
            }
            
            // Создаем кнопку из префаба
            ButtonLevel buttonLevel = Instantiate(buttonLevelPrefab, gridParent);
            
            if (buttonLevel != null)
            {
                // Устанавливаем номер уровня
                buttonLevel.SetLevel(levelNumber);
                levelButtons.Add(buttonLevel);
            }
            else
            {
                Debug.LogError($"ButtonLevel компонент не найден на префабе для уровня {levelNumber}");
            }
        }
        
        // Метод для обновления всех кнопок (например, при загрузке сохранений)
        public void UpdateAllButtons()
        {
            foreach (ButtonLevel button in levelButtons)
            {
                if (button != null)
                {
                    button.UpdateButtonState();
                }
            }
        }
        
        // Метод для получения кнопки по номеру уровня
        public ButtonLevel GetButtonByLevel(int levelNumber)
        {
            if (levelNumber >= 1 && levelNumber <= levelButtons.Count)
            {
                return levelButtons[levelNumber - 1];
            }
            return null;
        }
        
        // Метод для обновления состояния конкретной кнопки
        public void UpdateButtonState(int levelNumber)
        {
            ButtonLevel button = GetButtonByLevel(levelNumber);
            if (button != null)
            {
                button.UpdateButtonState();
            }
        }
    }
}
