using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class BaseManager : MonoBehaviour
{
    public event Action<int> _onCollectedResourcesChanged;

    [SerializeField] private List<UnitController> _allUnits = new List<UnitController>();
    private List<UnitController> _freeUnits = new List<UnitController>();

    private Queue<Resource> _foundResources = new Queue<Resource>();
    private Queue<Resource> _collectedResource = new Queue<Resource>();

    private void OnEnable()
    {
        foreach (var unit in _allUnits)
        {
            unit.OnUnitBecameIdle += HandleUnitBecameIdle;
        }
    }

    private void OnDisable()
    {
        foreach (var unit in _allUnits)
        {
            unit.OnUnitBecameIdle -= HandleUnitBecameIdle;
        }
    }

    private void Start()
    {
        _freeUnits.AddRange(_allUnits);
        StartCoroutine(SendUnitsPeriodically());
    }

    private void HandleUnitBecameIdle(UnitController unit)
    {
        if (!_freeUnits.Contains(unit) && unit.ShowBag() == null)
        {
            _freeUnits.Add(unit);
        }
    }

    public void SendUnitToResource()
    {
        if (_freeUnits.Count > 0 && _foundResources.Count > 0)
        {
            UnitController worksUnit = _freeUnits[0];
            _freeUnits.RemoveAt(0);
            worksUnit.MoveToResource(_foundResources.Dequeue());
        }
    }

    private IEnumerator SendUnitsPeriodically()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
            while (_freeUnits.Count > 0 && _foundResources.Count > 0)
            {
                SendUnitToResource();
                yield return wait;
            }

            yield return null;
        }
    }

    public void AddFoundResource(Resource resource)
    {
        _foundResources.Enqueue(resource);
    }

    public void AddCollectedResource(Resource resource)
    {
        _collectedResource.Enqueue(resource);
        _onCollectedResourcesChanged?.Invoke(_collectedResource.Count);
    }
}