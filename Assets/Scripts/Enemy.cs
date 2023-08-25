using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Generic Attributes")]
    [SerializeField] private float hearingRange = 5f;
    [SerializeField] private GameObject mutationPrefab;
    private readonly LayerMask _playerLayerMask = 1 << 8;
    protected enum States
    {
        Idle,
        Chase
    }

    protected States State { get; private set; }= States.Idle;

    protected void CheckPlayerInRange()
    {
        var playerCollider2D = Physics2D.OverlapCircle(transform.position, hearingRange, _playerLayerMask);
        if (!playerCollider2D) return;

        if (!Physics2D.Raycast(transform.position, playerCollider2D.transform.position - transform.position).collider.Equals(playerCollider2D)) return;
        
        TransitionToChase(playerCollider2D);
    }

    protected virtual void TransitionToIdle()
    {
        State = States.Idle;
    }
    
    protected virtual void TransitionToChase(Collider2D player)
    {
        State = States.Chase;
    }
    
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        Instantiate(mutationPrefab, transform.position, Quaternion.identity);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
