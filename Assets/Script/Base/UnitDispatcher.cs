using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UnitDispatcher : MonoBehaviour
{
    [SerializeField] private List<Movement> _allUnits = new List<Movement>();

    private Queue<Resource> _foundResources = new Queue<Resource>();
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(2f);

    private void Start()
    {
        StartCoroutine(DispatchUnits());
    }

    private IEnumerator DispatchUnits()
    {
        while (true)
        {
            Movement freeUnit = GetFreeUnit();
            SendUnitToResource(freeUnit);
            yield return _waitForSeconds;
        }
    }

    private Movement GetFreeUnit()
    {
        foreach (var unit in _allUnits)
        {
            if (unit.CurrentResource == null)
            {
                return unit;
            }
        }
        return null;
    }

    private void SendUnitToResource(Movement unit)
    {
        if (_foundResources.Count > 0 && unit != null)
        {
            unit.InitiateMoveToResource(_foundResources.Dequeue());
        }
    }

    public void AddFoundResource(Resource resource)
    {
        _foundResources.Enqueue(resource);
    }
}
