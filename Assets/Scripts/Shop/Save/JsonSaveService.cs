using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Shop.Save
{
    public class JsonSaveService : ISaveService
    {
        private const string FileName = "shop_save.json";
        private readonly string _savePath;

        private readonly int _defaultBalance;

        private readonly IEnumerable<ISavable> _savableList;
        
        public JsonSaveService(IEnumerable<ISavable> savableList, MainData mainData)
        {
            _savableList = savableList;
            _savePath = Path.Combine(Application.persistentDataPath, FileName);
            _defaultBalance = mainData.DefaultBalance;
        }
        
        public void Load()
        {
            PlayerSaveData saveData;
            
            if (!File.Exists(_savePath))
                saveData = CreateDefaultData();
            else
            {
                try
                {
                    var json = File.ReadAllText(_savePath);
                    var data = JsonUtility.FromJson<PlayerSaveData>(json);

                    if (data == null)
                        saveData = CreateDefaultData();

                    saveData = data;
                }
                catch
                {
                    saveData = CreateDefaultData();
                }
            }
            
            foreach (var savable in _savableList)
                savable.Load(saveData);
        }

        public void Save()
        {
            var saveData = new PlayerSaveData();

            foreach (var savable in _savableList)
                savable.Write(saveData);
            
            var json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(_savePath, json);

            Debug.Log($"Game saved to: {_savePath}");
        }
        
        private PlayerSaveData CreateDefaultData()
        {
            return new PlayerSaveData
            {
                Balance = _defaultBalance,
                PurchasedItemIds = new List<string>()
            };
        }
    }
}