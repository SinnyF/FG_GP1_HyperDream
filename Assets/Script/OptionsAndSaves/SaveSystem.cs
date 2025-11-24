using System;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    SaveData _saveData = new SaveData();
    [System.Serializable]
    public struct SaveData
    {
        public Language _languageSettings;
        public Text _textSettings;
        public float _volumeSettings, _highscore;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath  + "/ SaveData" + ".JSON";
        return saveFile;
    }

    public void Save()
    {
        HandleSave();

        Debug.Log("Saving savefile to path:" + SaveFileName());
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private void HandleSave()
    {
        if(GameManager.instance != null)
        {
            _saveData = GameManager.instance._saveData;
        }
    }

    public void Load()
    {
        Debug.Log("Loading savefile at: " + SaveFileName());
        if (File.Exists(SaveFileName()))
        {
            string saveContent = File.ReadAllText(SaveFileName());
            _saveData = JsonUtility.FromJson<SaveData>(saveContent);

            HandleLoad();
        }
        else Debug.LogWarning("Attempt to load savefile failed as it does not exist.");
    }

    private void HandleLoad()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance._saveData = _saveData;

            /*GameManager.instance._languageSettings = _saveData._languageSettings;
            GameManager.instance._textSettings = _saveData._textSettings;
            GameManager.instance._volumeSettings = _saveData._volumeSettings;
            GameManager.instance._highscore = _saveData._highscore;*/

            GameManager.instance.ApplyAllSettings();
        }
        else Debug.LogError("Bawlz in my Jawlz");
    }
}
