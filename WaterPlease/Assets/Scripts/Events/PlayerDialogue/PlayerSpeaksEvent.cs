public class PlayerSpeaksEvent : IGameEvent
{
    public string PlayerDialogue {  get; }

    public PlayerSpeaksEvent(string playerDialogue)
    {
        PlayerDialogue = playerDialogue;
    }
}
