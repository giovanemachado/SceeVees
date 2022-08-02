
public abstract class BaseGameState
{
    public virtual void EnterState(FlowManager flowManager) { }

    public virtual void UpdateState(FlowManager flowManager) { }

    public virtual void ExitState(FlowManager flowManager) { }
}