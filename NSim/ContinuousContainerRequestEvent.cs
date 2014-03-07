namespace NSim
{
    public class ContinuousContainerRequestEvent : EventBase
    {
        private readonly IContinuousContainer _owner;
        private readonly double _amount;

        public bool IsReleased { get; private set; }
        public IContinuousContainer Owner { get { return _owner; } }

        public double Amount
        {
            get { return _amount; }
        }

        internal void Release()
        {
            IsReleased = true;
        }

        internal ContinuousContainerRequestEvent(IContinuousContainer owner, double amount)
        {
            _owner = owner;
            _amount = amount;
        }

        public override void Schedule(IContext c)
        {
            
        }
    }
}