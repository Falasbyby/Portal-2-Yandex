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
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevel", 0);

       
        
        int LevelMax = currentLevelIndex;
      
        int maxLevelIndex = countLevels.Length - 1;
        if (currentLevelIndex >= maxLevelIndex)
        {
            currentLevelIndex = maxLevelIndex; // Set the current level to the maximum if all levels are completed
            maxLevel = true;
        }
        
       
       // Instantiate(levels[currentLevelIndex], transform);
      // levels[currentLevelIndex].gameObject.SetActive(true);
        InstantiateLevelFromResources();
      //  YandexGame.FullscreenShow();
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
        PlayerPrefs.SetInt("CurrentLevel", currentLevelIndex);
        YG2.InterstitialAdvShow();
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