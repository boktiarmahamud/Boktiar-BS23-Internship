namespace DependencyInjection.Services
{
    public class SingletonGuideService : IsingletonGuideService
    {
        private readonly Guid id;
        public SingletonGuideService()
        {
            id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return id.ToString();
        }
    }
}
