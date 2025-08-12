using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GearChangeMobileUi : MonoBehaviour
{
    // references 

    public Slider gearSlider;

    // values
    public float changingTime;

    private void Start()
    {
        //Debug.Log($"GameManager.instance is {(GameManager.instance == null ? "null":"not null")}");
        GameManager.instance.isGearChanged.AddListener(ChangingGearUi);
    }

    private void OnDisable()
    {
        GameManager.instance.isGearChanged.RemoveListener(ChangingGearUi);
    }

    public void ChangingGearUi(bool change)
    {
        float val=gearSlider.value;
        if (change) gearSlider.value = Mathf.MoveTowards(val, 0, changingTime);
        else if(!change) gearSlider.value =Mathf.MoveTowards(val,1,changingTime);
    }


    


    
}
