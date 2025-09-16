using UnityEngine;
using YG;

public class LevelController : Singleton<LevelController>
{
    
    [SerializeField] private Level[] levels;
    [SerializeField] private int[] countLevels;
    private int currentLevelIndex;

    public int GetCurrentLevel => currentLevelIndex;
    private int countEnemy;
    public bool maxLevel;
    private void Start()
    {
        // Проверяем, что выбранный уровень не превышает максимальный открытый уровень
        if (YG2.saves.currentLevel > YG2.saves.maxOpenLevel)
        {
            YG2.saves.currentLevel = YG2.saves.maxOpenLevel;
        }
        
        currentLevelIndex = YG2.saves.currentLevel;
        
        int maxLevelIndex = countLevels.Length - 1;
        if (currentLevelIndex >= maxLevelIndex)
        {
            currentLevelIndex = maxLevelIndex; // Set the current level to the maximum if all levels are completed
            maxLevel = true;
        }
        
        InstantiateLevelFromResources();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.O) && Input.GetKeyDown(KeyCode.K))
        {
            UiGame.Instance.WinActiveContainer();
        }
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        YG2.saves.currentLevel = currentLevelIndex;
        
        // Обновляем максимальный открытый уровень, если текущий уровень больше
        if (currentLevelIndex > YG2.saves.maxOpenLevel)
        {
            YG2.saves.maxOpenLevel = currentLevelIndex;
        }
        
        YG2.SaveProgress();
        YG2.InterstitialAdvShow();
    }
    
    // Метод для выбора уровня из меню
    public void SelectLevel(int levelIndex)
    {
        // Проверяем, что выбранный уровень не превышает максимальный открытый уровень
        if (levelIndex <= YG2.saves.maxOpenLevel)
        {
            YG2.saves.currentLevel = levelIndex;
            YG2.SaveProgress();
        }
        else
        {
            Debug.LogWarning($"Уровень {levelIndex} еще не открыт! Максимальный открытый уровень: {YG2.saves.maxOpenLevel}");
        }
    }
    private void InstantiateLevelFromResources()
    {
        // Предположим, что уровни находятся в папке "Resources/Levels
        Level level = Resources.Load<Level>("Levels/" + "Level_" + (currentLevelIndex+1));

        if (level != null)
        {
            Instantiate(level, transform);
        }
        else
        {
            Debug.LogError("Level not found in Resources: " + "Levels_" + (currentLevelIndex+1));
        }
    }
   
  

   
}