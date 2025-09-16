using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using YG;
namespace Portal_Git.Assets.Scripts.StartMenu
{
    public class ButtonLevel : MonoBehaviour
    {
        [SerializeField] private Button currentButton;
        [SerializeField] private Text levelText;
        [SerializeField] private GameObject selectIcon;
        [SerializeField] private GameObject lockIcon;
        private int level;
        public void SetLevel(int level)
        {
            this.level = level;
            string lang = YG2.lang;
            levelText.text = lang == "ru" ? "Уровень " + (level+1).ToString() : "Level " + (level+1).ToString();
            
            // Обновляем состояние кнопки в зависимости от того, открыт ли уровень
            UpdateButtonState();
        }
        
        public void UpdateButtonState()
        {
            bool isLevelOpen = level <= YG2.saves.maxOpenLevel;
            bool isCurrentLevel = level == YG2.saves.currentLevel;
            
            // Активируем/деактивируем кнопку
            currentButton.interactable = isLevelOpen;
            
            // Показываем/скрываем иконки
            if (lockIcon != null)
                lockIcon.SetActive(!isLevelOpen);
                
            if (selectIcon != null)
                selectIcon.SetActive(isCurrentLevel);
        }

        void Start()
        {
            currentButton.onClick.AddListener(OnClick);
            
        }
        

        private void OnClick()
        {
            // Проверяем, что уровень открыт (меньше или равен максимальному открытому уровню)
            if (level <= YG2.saves.maxOpenLevel)
            {
                YG2.saves.currentLevel = level;
                YG2.SaveProgress();
                Debug.Log($"Выбран уровень {level}");
            }
            else
            {
                Debug.Log($"Уровень {level} еще не открыт!");
            }
        }

    }
}