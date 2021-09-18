public abstract class StateMachine
{
    public virtual void OnEnter() {}
    public virtual void OnExit() {}
    public virtual void OnUpdate() {}
    public virtual void OnFixedUpdate() {}
    public virtual void OnTransition() {}
}