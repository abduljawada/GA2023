using UnityEngine;

public class Shrink : MonoBehaviour
{
    private const float CeilingCheckOffset = 1f;
    private const float CircleRadius = 0.4f;
    private static readonly LayerMask GroundLayer = 1 << 6;
    private const float NewScale = 0.5f;
    [HideInInspector] public bool isFromCeiling;

    private void Start()
    {
        transform.localScale = new Vector3(NewScale, NewScale, 1.0f);
    }

    private void Update()
    {
        if (!isFromCeiling) return;
        if (IsCeiling()) return;
        Destroy(this);
    }

    private void OnDestroy()
    {
        if (IsCeiling())
        {
            gameObject.AddComponent<Shrink>().isFromCeiling = true;
        }
        transform.localScale = Vector3.one;
    }
    
    private bool IsCeiling()
    {
        var transform1 = transform;
        return Physics2D.OverlapCircle(transform1.position + Vector3.up * (CeilingCheckOffset * transform1.localScale.x), CircleRadius, GroundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var transform1 = transform;
        Gizmos.DrawWireSphere(transform1.position + Vector3.up * (CeilingCheckOffset * transform1.localScale.x), CircleRadius);
    }
}
