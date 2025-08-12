using UnityEngine;

public class LiftAnimation : MonoBehaviour
{
    public Animator animator;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.Play("Lift aniamtion");
        }
    }
}
