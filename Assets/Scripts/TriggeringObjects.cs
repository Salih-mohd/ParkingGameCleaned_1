using UnityEngine;
using UnityEngine.Events;

public class TriggeringObjects : MonoBehaviour
{
    public UnityEvent TriggeringInLevelObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            TriggeringInLevelObjects.Invoke();
            gameObject.SetActive(false);
        }
    }
}
