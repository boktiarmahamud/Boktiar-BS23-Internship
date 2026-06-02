namespace DependencyInjection.Services
{
    public class ScopedGuideService : IScopedGuideService
    {
        private readonly Guid id;
        public ScopedGuideService()
        {
            id = Guid.NewGuid();
        }
        public string GetGuid()
        {   
            return id.ToString();
        }
    }
}
