using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AICollisionDetection : MonoBehaviour
{
    public AICarEngine AIEngine;
    public LevelFiveMission LevelFiveMission;
    public bool isLevel5;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AiSlowArea"))
        {

            // AIEngine.isBraking = true;
            StartCoroutine(SlowingDownAi());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (isLevel5)
        {
            if (LevelFiveMission.isMissionActive)
            {
                if (collision.collider.CompareTag("Car"))
                {
                    LevelFiveMission.DecreaseHealth(2);
                }
            }
        }
        
        
    }

    IEnumerator SlowingDownAi()
    {
        AIEngine.isBraking = true;
        yield return new WaitForSeconds(3);
        AIEngine.isBraking = false;
    }


   

}
