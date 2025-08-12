using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;

    private void Start()
    {
        if(car1 != null) car1.SetActive(false);
        if(car2 != null) car2.SetActive(false);

        string selectedCarId=GameManager.instance.GetSelectedCarId();

        if (selectedCarId == "car1")
        {
            car1.SetActive(true);
        }else if(selectedCarId == "car2")
        {
            car2.SetActive(true);
        }
        else
        {
            car2.SetActive(true);
        }
    }
}
