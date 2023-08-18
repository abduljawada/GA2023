using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Generic Attributes")]
    [SerializeField] private float hearingRange = 5f;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private GameObject mutationPrefab;
    protected enum States
    {
        Idle,
        Chase
    }

    protected States State { get; private set; }= States.Idle;

    protected void CheckPlayerInRange()
    {
        if (!Physics2D.OverlapCircle(transform.position, hearingRange, playerLayerMask)) return;

        //TODO: Check LOS
        
        TransitionToChase();
    }

    protected virtual void TransitionToChase()
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
