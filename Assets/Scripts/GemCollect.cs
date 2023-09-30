using TMPro;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject winText;
    private int _gems;

    public void AddGem()
    {
        _gems++;
        text.text = _gems.ToString();
        if (_gems < 4) return;
        winText.SetActive(true);
        Time.timeScale = 0f;
    }

}
