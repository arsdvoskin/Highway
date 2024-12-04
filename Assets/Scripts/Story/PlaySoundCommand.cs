using UnityEngine;

public class PlaySoundCommand : StoryCommand
{
    [SerializeField] private AudioClip _clip;

    public override void Execute()
    {
        StoryManager.PlayStorySound(_clip);
    }
}