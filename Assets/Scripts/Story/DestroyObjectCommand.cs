using UnityEngine;

public class DestroyObjectCommand : StoryCommand
{
    [SerializeField] private GameObject _target;

    public override void Execute()
    {
        StoryManager.DestroyObject(_target);
    }
}