using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    [SerializeField] private Animator animator;
    
    
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
}
