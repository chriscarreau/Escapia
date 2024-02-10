using Godot;

/// <summary>
/// All global events that can be triggered from anywhere and subscribed to anywhere
/// Follows guidelines from <seealso href="https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_signals.html">Godot C# Signals</seealso>
/// </summary>
public partial class Events : Node
{
    [Signal]
    public delegate void NewGameEventHandler();

    [Signal]
    public delegate void ExitGameEventHandler();

    [Signal]
    public delegate void PauseEventHandler();

    [Signal]
    public delegate void GameOverEventHandler();

    [Signal]
    public delegate void ResetGameStateEventHandler();
}