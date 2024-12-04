using UnityEngine;

public class WriteTextCommand : StoryCommand
{
    [SerializeField] private string _text;

    public override void Execute()
    {
        StoryManager.WriteStoryText(_text);
    }
}