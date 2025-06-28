using _2510.DesignPatternModule.Singleton;

namespace _2510.DesignPatternModule.Samples.Singleton
{
    public class GameManager : UniversalMonoBehaviourSingleton<GameManager>
    {
        private SaveDataManager _saveDataManager;
        private SaveDataManager.SaveData _currentSaveData;
        
        protected override void Init()
        {
            _saveDataManager = SaveDataManager.Instance;
            _saveDataManager.OnLoad += ApplySaveData;
        }
        
        public void ApplySaveData(SaveDataManager.SaveData saveData)
        {
            _currentSaveData = saveData;
            // hero.SetHealth(saveData.HeroHealth);
            // hero.SetPosition(saveData.HeroPosition);
        }
    }
}