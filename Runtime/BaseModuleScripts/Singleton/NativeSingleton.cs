namespace _2510.DesignPatternModule.Singleton
{
    /// <summary>
    /// Abstract class for a native singleton instance
    /// </summary>
    /// <example>
    /// <code>
    /// public class GameManager : NativeSingleton&lt;GameManager&gt;
    /// </code>
    /// </example>
    /// <typeparam name="T">Type of derived singleton class</typeparam>
    public abstract class NativeSingleton<T> where T : NativeSingleton<T>
    {
        private static T _instance = null;
        
        public static T Instance
        {
            get
            {
                if (_instance == null) InitSingleton();
                return _instance;
            }
        }        
        
        private static void InitSingleton()
        {
            if (_instance != null) return;

            _instance = System.Activator.CreateInstance<T>();
            _instance.Init();
        }

        protected NativeSingleton()
        {
            // Protected constructor to prevent instantiation from outside
            // This ensures that the singleton instance is created only through the Instance property
        }

        protected abstract void Init();
    }
}