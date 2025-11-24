using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    SaveSystem _saveData = new SaveSystem();
    public Language _languageSettings;
    public Text _textSettings;
    public float _volumeSettings;
    public static GameManager instance;
    public float _highscore;

    AudioSource[] _audioSources;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        _saveData.Load();

        /*if(MenuManager.instance != null)
        {
        _menuManager = MenuManager.instance
        }*/
        

        SceneManager.sceneLoaded += SceneChanged;
    }

    void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
    {
        ApplyAllSettings();
    }

    public void ApplyAllSettings()
    {
        if(MenuManager.instance != null)
        {
            MenuManager.instance.m_Language = _languageSettings;
            MenuManager.instance.m_Text = _textSettings;
            MenuManager.instance.volumeSlider.value = _volumeSettings;
        }
        _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in _audioSources)
        {
            source.volume = _volumeSettings;
        }
        if(HTPAnim.instance != null)
        {
            HTPAnim.instance._highscore = _highscore;
        }
    }
}
