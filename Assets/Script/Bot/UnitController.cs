using UnityEngine;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(ObjectHandler))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))] 

public class UnitController : MonoBehaviour
{
    public event Action<UnitController> OnUnitBecameIdle;

    private enum UnitState
    {
        Idle,
        MovingToResource,
        ReturningToBase
    }

    [SerializeField] private BaseManager _base;

    private UnitState _currentState = UnitState.Idle;
    private Resource _targetResource;
    private NavMeshAgent _navMeshAgent;
    private ObjectHandler _objectHandler; 

    private void Start()
    {
        _objectHandler = GetComponent<ObjectHandler>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case UnitState.Idle:
                OnUnitBecameIdle?.Invoke(this);
                break;
            case UnitState.MovingToResource:
                MoveTo(_targetResource.transform.position);
                break;
            case UnitState.ReturningToBase:
                MoveTo(_base.transform.position);
                break;
        }
    }

    private void MoveTo(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (_currentState == UnitState.MovingToResource)
            {
                _currentState = UnitState.ReturningToBase;
            }
            else if (_currentState == UnitState.ReturningToBase)
            {
                _currentState = UnitState.Idle;
                _targetResource = null;
            }
        }
    }

    public void MoveToResource(Resource resource)
    {
        _targetResource = resource;
        _objectHandler.AssignResourceIndex(resource.GetResourceIndex());
        _currentState = UnitState.MovingToResource;
    }

    public Resource ShowBag()
    {
        return _targetResource;
    }
}
