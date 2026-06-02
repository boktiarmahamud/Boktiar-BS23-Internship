namespace DependencyInjection.Services
{
    public class TransientGuideService : ITransientGuideService
    {
        private readonly Guid id;
        public TransientGuideService()
        {
            id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return id.ToString();
        }
    }
}
