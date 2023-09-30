using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Dir = Animator.StringToHash("Dir");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private PlayerController PlayerController => GetComponentInParent<PlayerController>();
    private Animator Animator => GetComponent<Animator>();
    
    private void Start()
    {
        PlayerController.OnStop += PlayerControllerOnOnStop; 
        PlayerController.OnWalk += PlayerControllerOnOnWalk;
        PlayerController.OnJump += PlayerControllerOnOnJump;
    }
    
    private void PlayerControllerOnOnStop(object sender, EntityEventArgs e)
    {
        if (Time.timeScale == 0f) return;
        
        Animator.SetBool(IsWalking, false);
        Animator.SetBool(IsJumping, false);
    }
    
    private void PlayerControllerOnOnWalk(object sender, EntityEventArgs e)
    {
        if (Time.timeScale == 0f) return;
        
        Animator.SetBool(IsWalking, true);
        Animator.SetBool(IsJumping, false);
        Animator.SetFloat(Dir, e.Dir);
    }
    
    private void PlayerControllerOnOnJump(object sender, EntityEventArgs e)
    {
        Animator.SetBool(IsWalking, false);
        Animator.SetBool(IsJumping, true);
        if (e.Dir != 0f)
        {
            Animator.SetFloat(Dir, e.Dir);
        }
    }
}
