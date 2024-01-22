using UnityEngine;

public abstract class AIBaseState
{
    public abstract void EnterState(AIStateManager ai);
    public abstract void UpdateState(AIStateManager ai);
    public abstract void OnCollisionEnterState(AIStateManager ai,Collision col);
    public abstract void CheckEveryNSecond(AIStateManager ai,float second); // n Saniyede bir checkler
    public abstract void ExitState(AIStateManager ai);
}
