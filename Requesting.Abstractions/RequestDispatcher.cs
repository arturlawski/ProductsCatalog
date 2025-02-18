namespace MediaExpert
{
    /// <summary>
    /// Dystrybutor żądania
    /// </summary>
    public interface RequestDispatcher : CommandDispatcher, CommandWithResponseDispatcher, QueryDispatcher
    {
    }
}
