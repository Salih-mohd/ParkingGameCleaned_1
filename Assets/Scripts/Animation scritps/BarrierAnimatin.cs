using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BarrierAnimatin : MonoBehaviour
{
    public Animator animator;

    public bool isAlwaysAnimate;
    public bool isOneTimeAnimate;

    private void Start()
    {
        if (isAlwaysAnimate)
        {
            StartCoroutine(AnimeController());
        }
        
        
    }
    
  
    IEnumerator AnimeController()
    {
        while (true)
        {
            
            animator.Play("barrier open animation");
            yield return new WaitForSeconds(4);
            animator.Play("barrier closeing animation");
            yield return new WaitForSeconds(4);
            
        }
       
    }

    public void OneTimeAnimate()
    {
        animator.Play("barrier open animation");
    }
    
}
