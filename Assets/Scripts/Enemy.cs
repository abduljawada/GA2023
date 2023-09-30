using System;
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

    public event EventHandler OnIdle;
    public event EventHandler OnChase;
    public event EventHandler<EntityEventArgs> ChangeDir;

    protected void OnChangeDir(EntityEventArgs e)
    {
        ChangeDir?.Invoke(this, e);
    }

    protected void CheckPlayerInRange()
    {
        var playerCollider2D = Physics2D.OverlapCircle(transform.position, hearingRange, _playerLayerMask);
        if (!playerCollider2D) return;

        var transform1 = transform;
        var position = transform1.position;
        var raycastHit2D = Physics2D.Raycast(position, playerCollider2D.transform.position - position);
        //Debug.Log(raycastHit2D.collider.name);
        if (!raycastHit2D.collider.Equals(playerCollider2D) && !raycastHit2D.collider.name.Equals("Player")) return;
        
        TransitionToChase(playerCollider2D);
    }

    protected void TransitionToIdle()
    {
        State = States.Idle;
        OnIdle?.Invoke(this, EventArgs.Empty);
    }
    
    protected virtual void TransitionToChase(Collider2D player)
    {
        State = States.Chase;
        OnChase?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded) return;
        if (!mutationPrefab) return;
        Instantiate(mutationPrefab, transform.position, Quaternion.identity);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
