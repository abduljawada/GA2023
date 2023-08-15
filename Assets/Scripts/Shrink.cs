using UnityEngine;

public class Shrink : MonoBehaviour
{
    private const float NewScale = 0.5f;

    private void Start()
    {
        transform.localScale = new Vector3(NewScale, NewScale, 1.0f);
    }

    private void OnDestroy()
    {
        transform.localScale = Vector3.one;
    }
}
