using UnityEngine;

public class SetImageCommand : StoryCommand
{
    [SerializeField] private Sprite _sprite;

    public override void Execute()
    {
        StoryManager.SetImageSprite(_sprite);
    }
}