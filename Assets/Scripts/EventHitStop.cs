using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventHitStop : MonoBehaviour
{
    [SerializeField] private float hitStopTime = 0.3f;
    [SerializeField] private UnityEvent onHitStopEnd;
    public void StartHitStop()
    {
        StartCoroutine(HitStopCoroutine());
    }

    public void ResetTime()
    {
        Time.timeScale = 1f;
    }

    private IEnumerator HitStopCoroutine()
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1f;
        onHitStopEnd?.Invoke();
    }
}
