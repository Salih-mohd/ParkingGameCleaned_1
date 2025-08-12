using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Level_9_Mission : MissionBase
{

    
    

    public GameObject[] toDisable;
    public GameObject[] toEnable;
    public GameObject missionInfoCanvas;

    public GameObject missionInfoImage;
    public TextMeshProUGUI timerText;

    //timer

    public float missionDuration;
    public float currentTime;
    public bool isTimeRunning;

    


    protected override void OnMissionStart()
    {
        DisablingItems();
        SettingUpUI();
        EnablingObjects();



    }

    private void EnablingObjects()
    {
        foreach (GameObject obj in toEnable)
        {
            obj.SetActive(true);
        }
    }

    private void DisablingItems()
    {
        foreach (var item in toDisable)
        {
            item.SetActive(false);
        }
    }

    private void SettingUpUI()
    {
        missionInfoCanvas.SetActive(true);

        // coroutine to disable mission info image
        StartCoroutine("DisablingMissionInfoPage");

        // timer starting

        Startcountowun();
    }

    private void Startcountowun()
    {
        currentTime = missionDuration;
        isTimeRunning = true;
    }
    
    private void StopCountDown()
    {
        isTimeRunning=false;
    }

    private void TimeIsUp()
    {
        GameManager.instance.TriggerLose();
    }


    private void UpdateTimerUI()
    {
        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{00:00}:{1:00}", min, sec);
    }

    private void Update()
    {
        if (isTimeRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimeRunning = false;
                TimeIsUp();
            }

            UpdateTimerUI();
        }
    }


    IEnumerator DisablingMissionInfoPage()
    {
        yield return new WaitForSeconds(6);
        missionInfoImage.SetActive(false);
    }

    public void ActivatingCar()
    {
        if (!activeCar.activeInHierarchy)
        {
            activeCar.SetActive(true);
        }
    }


    


}
