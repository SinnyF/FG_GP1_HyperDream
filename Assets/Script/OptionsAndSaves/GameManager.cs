using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    SaveSystem _saveSystem = new SaveSystem();
    public static GameManager instance;
    public SaveSystem.SaveData _saveData = new SaveSystem.SaveData();
    Language _languageSettings;
    Text _textSettings;
    float _volumeSettings;
    float _highscore;

    AudioSource[] _audioSources;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log($"Instance is equal to: {name}");
            Destroy(gameObject); 
            return;
        }

            DontDestroyOnLoad(gameObject);
        _saveSystem.Load();

        /*if(MenuManager.instance != null)
        {
        _menuManager = MenuManager.instance
        }*/
        

        SceneManager.sceneLoaded += SceneChanged;
    }

    void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("GameManager has seen a scene change.");
        ApplyAllSettings();
    }

    public void ApplyAllSettings()
    {
        if(MenuManager.instance != null)
        {
            MenuManager.instance.m_Language = _saveData._languageSettings;
            MenuManager.instance.m_Text = _saveData._textSettings;
            MenuManager.instance.volumeSlider.value = _saveData._volumeSettings;
        }
        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in _audioSources)
        {
            source.volume = _saveData._volumeSettings;
        }
        if(HTPAnim.instance != null)
        {
            HTPAnim.instance._highscore = _saveData._highscore;
        }
    }
}
