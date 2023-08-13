using UnityEngine;

[CreateAssetMenu(fileName = "New Mutation", menuName = "MutationData")]
public class MutationData : ScriptableObject
{
    public Sprite icon;
    public float duration;
    public int maxUses = 3;
    public int remainingUses;
}
