using UnityEngine;
using TMPro;

public class CollectedResourcesTracker : MonoBehaviour
{
    [SerializeField] private BaseManager _baseManager;
    [SerializeField] private TMP_Text _tmpText; 

    private void OnEnable()
    {
        _baseManager._onCollectedResourcesChanged += UpdateCollectedResources;
    }

    private void OnDisable()
    {
        _baseManager._onCollectedResourcesChanged -= UpdateCollectedResources;
    }

    private void UpdateCollectedResources(int countResource)
    {
        _tmpText.text = "Collected Resources: " + countResource.ToString(); 
    }
}
