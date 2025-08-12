using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public Rigidbody rigidbody;
    public UiManager uiManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.TriggerLose();
            rigidbody.constraints=RigidbodyConstraints.FreezeAll;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MissionTrigger"))
        {
           // Debug.Log("mission is triggered next is cutscene");
            //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            MissionEventManager.instance.TriggerMission();
            
            
        }

        if (other.CompareTag("Level Trigger"))
        {
            
            uiManager.ShowWinPanel();
        }
    }
}
