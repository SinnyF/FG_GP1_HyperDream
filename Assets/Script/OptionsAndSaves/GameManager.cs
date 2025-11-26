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

    [SerializeField]AudioSource[] _audioSources;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
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

        SetupReferences();
    }

    void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("GameManager has seen a scene change.");
        SetupReferences();
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
        
        foreach (AudioSource source in _audioSources)
        {
            //Volume of audio source is equal to "volumeSettings / the max value for the slider in the MenuManager"
            source.volume = _saveData._volumeSettings / 10;
        }
        if(HTPAnim.instance != null)
        {
            HTPAnim.instance._highscore = _saveData._highscore;
        }
    }

    //Code for finding objects that need to be modified by the options, currently just audio and text.
    private void SetupReferences()
    {
        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }
}
