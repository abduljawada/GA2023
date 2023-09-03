using TMPro;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject winText;
    private int gems;

    public void AddGem()
    {
        gems++;
        text.text = gems.ToString();
        if (gems >= 4)
        {
            winText.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
