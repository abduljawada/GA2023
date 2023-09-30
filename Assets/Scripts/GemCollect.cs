using TMPro;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    [SerializeField] private SaveData saveData;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject[] gems;
    private int _gems;

    private void Start()
    {
        if (saveData.gem1)
        {
            _gems++;
            Destroy(gems[0]);
        }
        if (saveData.gem2)
        {
            _gems++;
            Destroy(gems[1]);
        }
        if (saveData.gem3)
        {
            _gems++;
            Destroy(gems[2]);
        }
        if (saveData.gem4)
        {
            _gems++;
            Destroy(gems[3]);
        }

        UpdateUI();
    }

    public void AddGem()
    {
        _gems++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        text.text = _gems.ToString();
        if (_gems < 4) return;
        winText.SetActive(true);
        Time.timeScale = 0f;
    }

}
