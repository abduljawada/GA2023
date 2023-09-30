using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MutationManager mutationManager;
    [SerializeField] private MutationSlot[] mutationSlotsUI = new MutationSlot[2];

    [System.Serializable]
    public class MutationSlot
    {
        public Image mutationImage;
        public TMP_Text usesText;
        public Image fill;
        public GameObject buttonText;
    }
    private void Start()
    {
        mutationManager.OnAddMutation += MutationManagerOnOnAddMutation;
        mutationManager.OnActivateMutation += MutationManagerOnOnActivateMutation;
        mutationManager.OnRemoveMutation += MutationManagerOnOnRemoveMutation;
    }
    
    private void MutationManagerOnOnAddMutation(object sender, MutationManager.MutationEventArgs e)
    {
        var mutationSlot = mutationSlotsUI[e.MutationSlot];
        mutationSlot.mutationImage.sprite = e.MutationData.icon;
        mutationSlot.mutationImage.color = Color.white;
        mutationSlot.usesText.text = e.MutationData.remainingUses.ToString();
        mutationSlot.buttonText.SetActive(true);
    }

    private void MutationManagerOnOnActivateMutation(object sender, MutationManager.MutationEventArgs e)
    {
        var mutationSlot = mutationSlotsUI[e.MutationSlot];
        mutationSlot.usesText.text = e.MutationData.remainingUses.ToString();
        mutationSlot.fill.fillAmount = 1f;
        StartCoroutine(AnimateImageFillCoroutine(e.MutationData, mutationSlot));
    }
    
    private void MutationManagerOnOnRemoveMutation(object sender, MutationManager.MutationEventArgs e)
    {
        var mutationSlot = mutationSlotsUI[e.MutationSlot];
        mutationSlot.mutationImage.sprite = null;
        mutationSlot.mutationImage.color = Color.clear;
        mutationSlot.usesText.text = "";
        mutationSlot.buttonText.SetActive(false);
    }

    private static IEnumerator AnimateImageFillCoroutine(MutationData mutationData, MutationSlot mutationSlot)
    {
        var mutationEndTime = Time.time + mutationData.duration;
        while (Time.time < mutationEndTime)
        {
            mutationSlot.fill.fillAmount = (mutationEndTime - Time.time) / mutationData.duration;
            yield return new WaitForEndOfFrame();
        }

        mutationSlot.fill.fillAmount = 0f;
    }
}
