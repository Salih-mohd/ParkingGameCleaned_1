using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    public Button[] levelButtons; 

    private void Start()
    {
        int unlockedLevel = GameManager.instance.GetUnlockedLevel();
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 2; 
            levelButtons[i].interactable = (level <= unlockedLevel);
            int levelIndex = level; 
            levelButtons[i].onClick.AddListener(() => GameManager.instance.SelectLevel(levelIndex));
        }
    }
}