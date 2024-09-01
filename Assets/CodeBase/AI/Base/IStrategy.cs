namespace CodeBase.AI.Base
{
    public interface IStrategy
    {
        Node.Status Process();

        void Reset()
        {
            // Noop
        }
    }
}