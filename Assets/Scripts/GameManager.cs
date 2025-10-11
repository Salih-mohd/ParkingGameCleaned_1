using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance {  get; private set; }
   // public GameObject[] carPrefabs;

    //events
    public UnityEvent onLevelWin = new UnityEvent();
    public UnityEvent onLevelLose = new UnityEvent();
    public UnityEvent<bool> isGearChanged = new UnityEvent<bool>();
    public UnityEvent OnMission=new UnityEvent();
    

    //public UnityEvent missionIsStarted= new UnityEvent();


    public string selectedCarId;
    private int unlockedLevel = 2;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
            //Debug.Log("game manager instance is active");
        }
        else
        {
           // Debug.Log("Duplicate GameManager found, destroying this instance");
            Destroy(gameObject);
        }
    }

    


    public void SelectCar(string carId)
    {
        selectedCarId = carId;
        SceneManager.LoadSceneAsync(1);
    }

    public string GetSelectedCarId()
    {
        return selectedCarId;
    }

    public void UnlockNextLevel(int currentLevel)
    {
        int nextLevel = currentLevel + 1;
        if(nextLevel> unlockedLevel)
        {
            unlockedLevel = nextLevel;
            PlayerPrefs.SetInt("unlockedLevel",unlockedLevel);
            PlayerPrefs.Save();
        }
        SceneManager.LoadSceneAsync(nextLevel);
    }

    public int GetUnlockedLevel()
    {
        return unlockedLevel;
    }

    public void SelectLevel(int level)
    {
        if (level <= unlockedLevel)
        {
            SceneManager.LoadSceneAsync(level);
        }
    }

    private void LoadGameData()
    {
        unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 2);
    }

    public void RestartLevel(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void TriggerWin()
    {
        onLevelWin?.Invoke();
    }

    public void TriggerLose()
    {
        onLevelLose?.Invoke();
    }

    public void TriggerGearChangingUi(bool change)
    {
        //Debug.Log($"TriggerGearChangingUi called with change: {change}");
        isGearChanged?.Invoke(change);
    }




    //public void TriggerMission()
    //{
    //    missionIsStarted?.Invoke();
    //}

}
