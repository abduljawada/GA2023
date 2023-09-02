using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float groundCheckOffset = 1f;
    [SerializeField] private float circleRadius = 0.3f;
    private readonly LayerMask _groundLayer = 1;
    
    public bool IsGrounded()
    {
        var transform1 = transform;
        return Physics2D.OverlapCircle(transform1.position - Vector3.up * (groundCheckOffset * transform1.localScale.x), circleRadius, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var transform1 = transform;
        Gizmos.DrawWireSphere(transform1.position - Vector3.up * (groundCheckOffset * transform1.localScale.x), circleRadius);
    }
}
