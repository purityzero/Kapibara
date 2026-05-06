using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataMgr : ClassSingleton<DataMgr>
{
    private List<Data> listData = new List<Data>();

    public void Init()
    {
        PlayerData player = GetData<PlayerData>();
        SaveData<PlayerData>(player);
    }

    public T GetData<T>() where T : Data, new()
    {
        var foundItem = listData.Find(x => x is T);
        if (foundItem != null)
        {
            return foundItem as T;
        }
        else
        {
            T data = LoadData<T>();
            listData.Add(data);  
            return data;
        }
    }

    public void SaveData<T>(T data) where T : Data
    {
        string path = GetFilePath<T>();
        string json = JsonUtility.ToJson(data); 
        
        File.WriteAllText(path, json);
    }

    private T LoadData<T>() where T : Data, new()
    {
        string path = GetFilePath<T>();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);  
            T data = JsonUtility.FromJson<T>(json); 

            if (data == null)
            {
                Debug.LogError("Loaded data is null.");
                data = new T();
                SaveData<T>(data); 
            }

            return data;
        }
        else
        {
            T data = new T();
            return data;
        }
    }

    private string GetFilePath<T>()
    {
        return Path.Combine(Application.persistentDataPath, $"{typeof(T).Name}.dat");
    }
}
