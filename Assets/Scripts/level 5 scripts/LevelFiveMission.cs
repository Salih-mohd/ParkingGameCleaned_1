using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class LevelFiveMission : MissionBase
{

    
    public GameObject[] ToDisable;
    public GameObject AICar;
    

    // UI
    public Canvas missionInfoCanvas;
    public GameObject missionInfoImage;
    public GameObject imageTimer;
    public TextMeshProUGUI timerText;
    
    public Slider healthBarOfCar;

    // Timer

    public float missionDuration = 120f;
    private float currentTime;
    private bool isTimeRunning=false;


    //cutscenes 

    public PlayableDirector cutScene_2;

    // FX
    public GameObject[] effects;


    

    protected override void OnMissionStart()
    {
        
        DisablingItems();
        SettingUpUI();

    }

    private void DisablingItems()
    {
        foreach (var item in ToDisable)
        {
            item.SetActive(false);
        }
        //Debug.Log("mission started after cutscene");
    }

    private void SettingUpUI()
    {
        missionInfoCanvas.enabled = true;



        // disabling misson infor image using coroutine
        StartCoroutine(DeactivatingInfoPage());

        // starting timer
        StartCountDown();

    }

    public void DecreaseHealth(int health)
    {
        healthBarOfCar.value-=health;

        if (healthBarOfCar.value == 0)
        {
            //Debug.Log("car is destroyed");
            StopCountDown();


            cutScene_2.Play();
            Array.ForEach(effects, effect => effect.SetActive(true));
            
            AICar.SetActive(false );
            activeCar.SetActive(false);

            

            
        }
           
    }

    IEnumerator DeactivatingInfoPage()
    {
        yield return new WaitForSeconds(5);
        missionInfoImage.SetActive(false );
    }


    private void StartCountDown()
    {
        currentTime=missionDuration;
        isTimeRunning=true;
    }

    private void StopCountDown()
    {
        isTimeRunning = false;
    }
    
    private void TimeIsUP()
    {
        // Debug.Log("time is up");
        GameManager.instance.TriggerLose();
        AICar.SetActive(false) ;

    }

    private void UpdateTimerUi()
    {
        int min = Mathf.FloorToInt(currentTime / 60);
        int sec=Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{00:00}:{1:00}", min, sec);
    }

    private void Update()
    {
        if (isTimeRunning)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                currentTime = 0;
                isTimeRunning = false;
                TimeIsUP(); 
            }

            UpdateTimerUi();
        }

        

        
    }
    public void DeactivatingEffectsAfterCutscene_2()
    {
        Array.ForEach(effects, effect => effect.SetActive(false));
    }
    










}
