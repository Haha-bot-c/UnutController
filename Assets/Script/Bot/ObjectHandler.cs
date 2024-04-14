using UnityEngine;
using DG.Tweening; 

public class ObjectHandler : MonoBehaviour
{
    private const int ZeroIndexResorses = 0;

    [SerializeField] private Transform _backpackPoint;

    private float _moveDuration = 0.2f;
    private GameObject _heldObject; 
    private bool _isCarryingObject = false;
    private int _indexRessorse;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isCarryingObject && other.TryGetComponent(out Resource resource) && resource.GetResourceIndex() == _indexRessorse)
        {
            PickupObject(other.gameObject);
        }
        else if (_isCarryingObject && other.TryGetComponent(out BaseWarehouse baseManager) )
        {
            DropObject(other.transform);
        }
    }

    private void PickupObject(GameObject objectToPickup)
    {
        _heldObject = objectToPickup;
        _isCarryingObject = true;

        Rigidbody objectRigidbody = _heldObject.GetComponent<Rigidbody>();

        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = true;
        }

        _heldObject.transform.DOMove(_backpackPoint.position, _moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _heldObject.transform.SetParent(transform);
        });
    }

    private void DropObject(Transform transform)
    {
        Rigidbody objectRigidbody = _heldObject.GetComponent<Rigidbody>();

        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = false;
        }

        _heldObject.transform.position = transform.position;

        _heldObject.transform.SetParent(null); 

        _heldObject = null;
        _isCarryingObject = false;
        _indexRessorse = ZeroIndexResorses;
    }

    public void AssignResourceIndex(int resourceIndex)
    {
        _indexRessorse = resourceIndex;
    }
}
