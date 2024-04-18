using UnityEngine;
using System.Collections.Generic;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private UnitDispatcher _base;
    private int _resorseIndex;

    private List<Resource> _foundResources = new List<Resource>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resourceComponent))
        {
            if (_foundResources.Contains(resourceComponent) == false)
            {
                _resorseIndex++;
                resourceComponent.AssignResourceIndex(_resorseIndex);
                _base.AddFoundResource(resourceComponent);
                _foundResources.Add(resourceComponent);
            }
        }
    }
}
