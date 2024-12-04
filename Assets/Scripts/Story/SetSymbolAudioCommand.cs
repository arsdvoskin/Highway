using UnityEngine;

public class SetSymbolAudioCommand : StoryCommand
{
    [SerializeField] private AudioClip _clip;

    public override void Execute()
    {
        StoryManager.SetSymbolAudio(_clip);
    }
}