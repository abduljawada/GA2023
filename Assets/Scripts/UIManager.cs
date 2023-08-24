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
    }

    private void MutationManagerOnOnActivateMutation(object sender, MutationManager.MutationEventArgs e)
    {
        var mutationSlot = mutationSlotsUI[e.MutationSlot];
        mutationSlot.usesText.text = e.MutationData.remainingUses.ToString();
    }
    
    private void MutationManagerOnOnRemoveMutation(object sender, MutationManager.MutationEventArgs e)
    {
        var mutationSlot = mutationSlotsUI[e.MutationSlot];
        mutationSlot.mutationImage.sprite = null;
        mutationSlot.mutationImage.color = Color.clear;
        mutationSlot.usesText.text = "";
    }
}
