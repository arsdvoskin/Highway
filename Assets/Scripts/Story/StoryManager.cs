using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class StoryManager : MonoSingleton<StoryManager>
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _symbolSound;
    [SerializeField] private float _symbolsDelay;
    [SerializeField] StoryCommand[] _commands;
    private int _commandIndex = 0;
    private bool _nextCommandIsAvailable = true;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ExecuteNextCommand();
    }

    private void Update()
    {
        if (!_nextCommandIsAvailable)
        {
            return;
        }

        bool input = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (input)
        {
            ExecuteNextCommand();
        }
    }

    public void ExecuteNextCommand()
    {
        if (_commandIndex > _commands.Length - 1)
        {
            SceneManager.LoadScene((int)Scenes.Race, LoadSceneMode.Single);
        }

        _commands[_commandIndex].Execute();
        _commandIndex++;

        if (_commands[_commandIndex - 1] is DestroyObjectCommand || 
            _commands[_commandIndex - 1] is SetImageCommand || 
            _commands[_commandIndex - 1] is SetSymbolAudioCommand)
        {
            ExecuteNextCommand();
        }
    }

    public static void WriteStoryText(string text)
    {
        _instance.StartCoroutine(_instance.WriteText(text));
    }

    private IEnumerator WriteText(string text)
    {
        _instance._nextCommandIsAvailable = false;

        _storyText.text = "";
        _audioSource.clip = _symbolSound;
        var delay = new WaitForSeconds(_symbolsDelay);

        for (int i = 0; i < text.Length; i++)
        {
            _storyText.text += text[i];

            if (text[i] != ' ' && text[i] != ',' && text[i] != '.')
            {
                _audioSource.Play();
            }

            yield return delay;
        }

        _instance._nextCommandIsAvailable = true;
    }

    public static void PlayStorySound(AudioClip clip)
    {
        _instance.StartCoroutine(_instance.PlaySound(clip));
    }

    private IEnumerator PlaySound(AudioClip clip)
    {
        _instance._nextCommandIsAvailable = false;

        _instance._audioSource.clip = clip;
        _instance._audioSource.Play();
        var delay = new WaitForEndOfFrame();

        while (_instance._audioSource.isPlaying)
        {
            yield return delay;
        }

        _instance._nextCommandIsAvailable = true;
    }

    public static void SetImageSprite(Sprite sprite)
    {
        _instance._image.sprite = sprite;
    }

    public static void DestroyObject(GameObject obj)
    {
        Object.Destroy(obj);
    }

    public static void SetSymbolAudio(AudioClip clip)
    {
        _instance._symbolSound = clip;
        _instance._audioSource.clip = clip;
    }

    [ContextMenu("Collect Commands")]
    private void CollectCommands()
    {
        _commands = GetComponentsInChildren<StoryCommand>();
    }

    private void OnValidate()
    {
        CollectCommands();
    }
}
