using System;
using System.Linq;
using UnityEngine;

public class MutationManager : MonoBehaviour
{
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
        var emptyElement = -1;
        
        for (var i = 0; i < _mutationInventory.Length; i++)
        {
            Debug.Log(_mutationInventory[i]);
            if (_mutationInventory[i] != null) continue;
            Debug.Log("Element " + i + " is empty");
            emptyElement = i;
            break;
        }
        
        if (emptyElement == -1) return;
        
        if (_mutationInventory.Contains(mutationData)) return;
        Debug.Log("Array doesn't contain mutation");

        _mutationInventory[emptyElement] = mutationData;
        mutationData.remainingUses = mutationData.maxUses;
        
        Destroy(mutationObject);
    }

    private void ActivateMutation(int index)
    {
        var mutationData = _mutationInventory[index];
        Destroy(gameObject.AddComponent(Type.GetType(mutationData.name)), mutationData.duration);
        mutationData.remainingUses--;
        Debug.Log(mutationData.remainingUses);
        if (mutationData.remainingUses <= 0)
        {
            _mutationInventory[index] = null;
            Debug.Log("removed mutation from inventory");
        }
    }
}
