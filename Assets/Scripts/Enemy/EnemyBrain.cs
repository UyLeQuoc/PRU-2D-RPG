using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initState; // PatrolState
    [SerializeField] private FSMState[] states; // PatrolState | AttackState | DieState | ...

    public FSMState CurrentState { get; set; }
    public Transform Player { get; set; }

    private void Start()
    {
        ChangeState(initState);
    }
    private void Update()
    {
        CurrentState?.UpdateState(this);
    }

    public void ChangeState(string newStateID)
    {
        FSMState newState = GetState(newStateID);
        if (newState == null) return;
        CurrentState = newState;
    }

    private FSMState GetState(string newStateId)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].ID == newStateId)
            {
                return states[i];
            }
        }
        return null;
    }
}
