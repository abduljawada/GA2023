using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MutationManager : MonoBehaviour
{
    public event EventHandler<MutationEventArgs> OnAddMutation;
    public event EventHandler<MutationEventArgs> OnActivateMutation;
    public event EventHandler<MutationEventArgs> OnRemoveMutation;
    public class MutationEventArgs : EventArgs
    {
        public MutationData MutationData;
        public int MutationSlot;
    }
    private readonly MutationData[] _mutationInventory = new MutationData[2];
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!_mutationInventory[0]) return;
            if (!GetComponent(Type.GetType(_mutationInventory[0].name))) ActivateMutation(0);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_mutationInventory[1]) return;
            if (!GetComponent(Type.GetType(_mutationInventory[1].name))) ActivateMutation(1);
        }
        
    }

    public void AddMutation(GameObject mutationObject, MutationData mutationData)
    {
        if (_mutationInventory.Contains(mutationData))
        {
            mutationData.remainingUses = mutationData.maxUses;
            Destroy(mutationObject);
            for (var i = 0; i < _mutationInventory.Length; i++)
            {
                if (!_mutationInventory[i].Equals(mutationData)) continue;
                OnAddMutation?.Invoke(this, new MutationEventArgs { MutationData = _mutationInventory[i], MutationSlot = i });
                return;
            }
            return;
        }
        
        var emptyElement = -1;
        
        for (var i = 0; i < _mutationInventory.Length; i++)
        {
            if (_mutationInventory[i] != null) continue;
            emptyElement = i;
            break;
        }
        
        if (emptyElement == -1) return;
        

        _mutationInventory[emptyElement] = mutationData;
        mutationData.remainingUses = mutationData.maxUses;
        
        Destroy(mutationObject);
        
        OnAddMutation?.Invoke(this, new MutationEventArgs { MutationData = _mutationInventory[emptyElement], MutationSlot = emptyElement });
    }

    private void ActivateMutation(int index)
    {
        var mutationData = _mutationInventory[index];
        Destroy(gameObject.AddComponent(Type.GetType(mutationData.name)), mutationData.duration);
        mutationData.remainingUses--;
        if (mutationData.remainingUses <= 0)
        {
            StartCoroutine(RemoveMutationCoroutine(index));
        }
        
        OnActivateMutation?.Invoke(this, new MutationEventArgs { MutationData = _mutationInventory[index], MutationSlot = index });
    }

    private IEnumerator RemoveMutationCoroutine(int index)
    {
        yield return new WaitForSeconds(_mutationInventory[index].duration);
        _mutationInventory[index] = null;
        OnRemoveMutation?.Invoke(this, new MutationEventArgs { MutationData = _mutationInventory[index], MutationSlot = index });
    }
}
