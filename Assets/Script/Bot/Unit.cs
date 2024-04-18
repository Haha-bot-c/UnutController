using UnityEngine.AI;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Resource CurrentResource { get; private set; }

    protected enum UnitState
    {
        Idle,
        MovingToResource,
        ReturningToBase
    }

    [SerializeField] private UnitDispatcher _base;

    protected UnitState _currentState = UnitState.Idle;

    protected NavMeshAgent _navMeshAgent;

    protected Vector3 _positionResourse;
    protected Vector3 _positionBase;
    private Vector3 _startingPosition;

    protected virtual void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _positionBase = _base.transform.position;
        _startingPosition = transform.position;
    }

    protected virtual void Update()
    {

        switch (_currentState)
        {
            case UnitState.Idle:
                MoveTo(_startingPosition);
                break;
            case UnitState.MovingToResource:
                MoveTo(_positionResourse);
                break;
            case UnitState.ReturningToBase:
                MoveTo(_positionBase);
                break;
        }
    }

    protected abstract void MoveTo(Vector3 position);

    public void ResetCurrentResource()
    {
        CurrentResource = null;
    }

    public void InitiateMoveToResource(Resource resource)
    {
        CurrentResource = resource;
        _positionResourse = resource.transform.position;
        _currentState = UnitState.MovingToResource;
    }
}
