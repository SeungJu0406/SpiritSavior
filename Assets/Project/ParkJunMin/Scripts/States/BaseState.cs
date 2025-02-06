namespace Project.ParkJunMin.Scripts.States
{
    public abstract class BaseState
    {
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
        public virtual void FixedUpdate() { }   
    }
}
