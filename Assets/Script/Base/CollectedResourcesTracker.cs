using UnityEngine;
using TMPro;

public class CollectedResourcesTracker : MonoBehaviour
{
    [SerializeField] private BaseWarehouse _baseWarehouse;
    [SerializeField] private TMP_Text _tmpText; 

    private void OnEnable()
    {
        _baseWarehouse.OnCollectedResourcesChanged += UpdateCollectedResources;
    }

    private void OnDisable()
    {
        _baseWarehouse.OnCollectedResourcesChanged -= UpdateCollectedResources;
    }

    private void UpdateCollectedResources(int countResource)
    {
        _tmpText.text = "Collected Resources: " + countResource.ToString(); 
    }
}
