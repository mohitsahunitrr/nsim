namespace NSim
{
    public interface IContinuousContainer
    {
        ContinuousContainerRequestEvent Get(double amount);
        ContinuousContainerRequestEvent Put(double amount);
        double Capacity { get; }
        double Level { get; }
    }
}