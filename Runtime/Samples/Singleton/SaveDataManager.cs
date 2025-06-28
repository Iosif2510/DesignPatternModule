using System;
using System.IO;
using System.Threading.Tasks;
using _2510.DesignPatternModule.Singleton;
using _2510.DesignPatternModule.Utilities;
using UnityEngine;

namespace _2510.DesignPatternModule.Samples.Singleton
{
    public sealed class SaveDataManager : NativeSingleton<SaveDataManager>
    {
        public class SaveData
        {
            public int CurrentLevel;
            public int Health;
            public Vector3 Position;

            public SaveData(int currentLevel, int health, Vector3 position)
            {
                CurrentLevel = currentLevel;
                Health = health;
                Position = position;
            }
        }
        
        private string _saveDataJson;
        private SaveData _saveData;

        private string _savePath;
        private const string FileName = "saveData.json";

        public event Action OnSave;
        public event Action<SaveData> OnLoad;
        
        private SaveDataManager() : base()
        {
            
        }
        
        protected override void Init()
        {
            _savePath = $"{Application.persistentDataPath}/saves";
        }
        
        public void InitializeSaveData(int currentLevel, int health, Vector3 position)
        {
            _saveData = new SaveData(currentLevel, health, position);
        }

        public async Task SaveGame()
        {
            _saveDataJson = JsonUtility.ToJson(_saveData);
            await SaveLoadUtility.SaveJsonAsync(_saveDataJson, _savePath, FileName);
            OnSave?.Invoke();
        }

        public async Task LoadGame()
        {
            _saveDataJson = await SaveLoadUtility.LoadJsonAsync(_savePath, FileName);
            _saveData = JsonUtility.FromJson<SaveData>(_saveDataJson);
            OnLoad?.Invoke(_saveData);
        }
    }
}