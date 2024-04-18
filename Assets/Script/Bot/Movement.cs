using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(ItemPicker))]
public class Movement : Unit
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update(); 
    }

    protected override void MoveTo(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);

        if (_navMeshAgent.pathPending == false && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (_currentState == UnitState.MovingToResource)
            {
                _currentState = UnitState.ReturningToBase;
            }
            else if (_currentState == UnitState.ReturningToBase)
            {
                _currentState = UnitState.Idle;
            }
        }
    } 
}
