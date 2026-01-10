using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public int currentLevel;

    public Canvas winLoseCanvas;
    public GameObject winPanel, losePanel;
    public GameObject pausePanel;
    public InputActionAsset inputActions;
    public  GameObject dancingButtons;
    public  GameObject turningButtons;
    

    [HideInInspector] public GameObject activeCarUIManager;
    //public  GameObject[] CarPrefab;

    

    // testing
    //public Animation anim1;
    //public Animation anim2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //winLoseCanvas.enabled = false;
        losePanel.SetActive(false);
        winPanel.SetActive(false);


        GameManager.instance.onLevelLose.AddListener(ShowLosePanel);
        GameManager.instance.onLevelWin.AddListener(ShowWinPanel);

    }


    private void OnDisable()
    {
        GameManager.instance.onLevelLose.RemoveListener(ShowLosePanel);
        GameManager.instance.onLevelWin.RemoveListener(ShowWinPanel);
    }

    public void ShowWinPanel()
    {
        winLoseCanvas.enabled=true;
        winPanel.SetActive(true);
        losePanel.SetActive(false);

        
        inputActions.FindActionMap("Car").Disable();
        CollectingCarsInTheHierarchy();


        //Time.timeScale = 0f;



    }


    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        winLoseCanvas.enabled = true;
        inputActions.FindActionMap("Car").Disable();
    }

    public void disablePausePanel()
    {
        pausePanel.SetActive(false );
        inputActions.FindActionMap("Car").Enable();
        winLoseCanvas.enabled = false;
    }

    public void ShowLosePanel()
    {
        winLoseCanvas.enabled=true;
        winPanel.SetActive(false);
        losePanel.SetActive(true);
        inputActions.FindActionMap("car").Disable();

        CollectingCarsInTheHierarchy();

        //Time.timeScale = 0f;
    }

    public void ToNextLevel()
    {
        GameManager.instance.UnlockNextLevel(currentLevel);
    }

    public void LevelRestart()
    {
        GameManager.instance.RestartLevel(currentLevel);
    }

    public void ReturnHome()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void DancingKeysOnAndOff(bool change)
    {
        dancingButtons.SetActive(change);
        turningButtons.SetActive(!change);
    }

    //IEnumerator AnimController()
    //{

    //    while(true)
    //    {
    //        anim1.Play();
    //        yield return new WaitForSeconds(3);
    //        anim2.Play();
    //        yield return new WaitForSeconds(3);
    //    }
        
    //}

    private void CollectingCarsInTheHierarchy()
    {
        GameObject[] activeCars = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject car in activeCars)
        {
            if (car.activeInHierarchy)
            {
                car.SetActive(false);
                activeCarUIManager=car;
            }
                
        }

    }
}
