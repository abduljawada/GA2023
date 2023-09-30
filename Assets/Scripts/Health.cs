using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    private int _currentHealth;

    [SerializeField] private UnityEvent onDamageEvent;
    [SerializeField] private UnityEvent onDeathEvent;

    [SerializeField] private GameObject sixHearts;
    [SerializeField] private GameObject fiveHearts;
    [SerializeField] private GameObject fourHearts;
    [SerializeField] private GameObject threeHearts;
    [SerializeField] private GameObject twoHearts;
    [SerializeField] private GameObject oneHeart;

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
            switch(_currentHealth)
            {
                case 6:
                    sixHearts.SetActive(true);
                    fiveHearts.SetActive(false);
                    fourHearts.SetActive(false);
                    threeHearts.SetActive(false);
                    twoHearts.SetActive(false);
                    oneHeart.SetActive(false);
                    break;
                case 5:
                    sixHearts.SetActive(false);
                    fiveHearts.SetActive(true);
                    fourHearts.SetActive(false);
                    threeHearts.SetActive(false);
                    twoHearts.SetActive(false);
                    oneHeart.SetActive(false);
                    break;
                case 4:
                    sixHearts.SetActive(false);
                    fiveHearts.SetActive(false);
                    fourHearts.SetActive(true);
                    threeHearts.SetActive(false);
                    twoHearts.SetActive(false);
                    oneHeart.SetActive(false);
                    break;
                case 3:
                    sixHearts.SetActive(false);
                    fiveHearts.SetActive(false);
                    fourHearts.SetActive(false);
                    threeHearts.SetActive(true);
                    twoHearts.SetActive(false);
                    oneHeart.SetActive(false);
                    break;
                case 2:
                    sixHearts.SetActive(false);
                    fiveHearts.SetActive(false);
                    fourHearts.SetActive(false);
                    threeHearts.SetActive(false);
                    twoHearts.SetActive(true);
                    oneHeart.SetActive(false);
                    break;
                case 1:
                    sixHearts.SetActive(false);
                    fiveHearts.SetActive(false);
                    fourHearts.SetActive(false);
                    threeHearts.SetActive(false);
                    twoHearts.SetActive(false);
                    oneHeart.SetActive(true);
                    break;
            }
        }

        if (_currentHealth > 0) return;
        
        onDeathEvent?.Invoke();
    }

    public void Reset()
    {
        _currentHealth = maxHealth;
    }
}
