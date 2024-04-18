using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public bool IsCarryingObject { get; private set; } = false;

    [SerializeField] private Transform _backpackPoint;

    private Resource _heldObject;
    private Movement _movement;

    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCarryingObject == false && other.TryGetComponent(out Resource resource) && resource.ResourceIndex == _movement.CurrentResource?.ResourceIndex)
        {
            PickupObject(resource);
        }
        else if (IsCarryingObject && other.TryGetComponent(out BaseWarehouse baseManager))
        {
            DropObject(other.transform);
        }
    }

    private void PickupObject(Resource objectToPickup)
    {
        _heldObject = objectToPickup;
        IsCarryingObject = true;

        if (_heldObject.TryGetComponent(out Rigidbody objectRigidbody))
        {
            objectRigidbody.isKinematic = true;
        }

        _heldObject.transform.position = _backpackPoint.position;
        _heldObject.transform.SetParent(transform);
    }

    private void DropObject(Transform transform)
    {
        if (_heldObject.TryGetComponent(out Rigidbody objectRigidbody))
        {
            objectRigidbody.isKinematic = false;
        }

        _heldObject.transform.position = transform.position;

        _heldObject.transform.SetParent(null);
        _movement.ResetCurrentResource();
        _heldObject = null;
        IsCarryingObject = false;
    }
}
