using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public abstract class MissionBase : MonoBehaviour
{
    public bool isMissionActive=false;
    public InputActionAsset inputActions;
    public GameObject[] CarPrefab;
    public PlayableDirector cutscene_1;

    private GameObject[] activeCars;
    public GameObject activeCar;

    public void StartCutScene()
    {
       // Debug.Log("cutscene started");
        inputActions.FindActionMap("car").Disable();
        CollectingCarsInTheHierarchy();
        
        //FreezingCar();
        cutscene_1.Play();
        
        
    }

    
    //protected abstract void CutSceneStarted();

    public void CutSceneEnded()
    {
        isMissionActive = true;
        inputActions.FindActionMap("car").Enable();
        NoneFreezingCar();
        
        OnMissionStart();
       // Debug.Log("cutscene ended");
    }
    protected abstract void OnMissionStart();

    private void CollectingCarsInTheHierarchy()
    {
        activeCars = GameObject.FindGameObjectsWithTag("Car");
        foreach(var car in activeCars)
        {
            if(car.activeInHierarchy) activeCar = car;
        }
        FreezingCar();


    }

    private void FreezingCar()
    {
       activeCar.SetActive(false);
    }
    private void NoneFreezingCar()
    {
        activeCar.SetActive(true);
    }




}
