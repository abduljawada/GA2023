using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int _currentHealth;

    [SerializeField] private UnityEvent onDamageEvent;
    [SerializeField] private UnityEvent onDeathEvent;

    [SerializeField] private TMP_Text playerHealthText;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void LoseHealth(int damage = 1)
    {
        _currentHealth -= damage;
        
        onDamageEvent?.Invoke();

        if (transform.tag.Equals("Player"))
        {
                playerHealthText.text = _currentHealth.ToString();
        }

        if (_currentHealth > 0) return;
        onDeathEvent?.Invoke();

        if (transform.tag.Equals("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	
        Destroy(transform.parent.gameObject);
    }

    public void Reset()
    {
        _currentHealth = maxHealth;

        if (!gameObject.tag.Equals("Player")) return;
        if (playerHealthText)
        {
            playerHealthText.text = _currentHealth.ToString();
        }
    }
}
