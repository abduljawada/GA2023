using UnityEngine;

public class EventGetGem : MonoBehaviour
{
    [SerializeField] private SaveData saveData;
    
    public void GetGem(int gemIndex)
    {
        FindObjectOfType<GemCollect>().AddGem();

        switch (gemIndex)
        {
            case 0:
                saveData.gem1 = true;
                break;
            case 1:
                saveData.gem2 = true;
                break;
            case 2:
                saveData.gem3 = true;
                break;
            case 3:
                saveData.gem4 = true;
                break;
        }
    }
}
