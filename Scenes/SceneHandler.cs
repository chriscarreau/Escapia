using Godot;
using Godot.Collections;

public partial class SceneHandler : Node
{
	PackedScene GameScene = (PackedScene)ResourceLoader.Load("res://Scenes/Gameplay/Game.tscn");
	PackedScene MenuScene = (PackedScene)ResourceLoader.Load("res://Scenes/Menus/MainMenu.tscn");
	Dictionary<ScenesEnum, PackedScene> Scenes = new Dictionary<ScenesEnum, PackedScene>();
	Events GlobalEvents;

	public override void _Ready()
	{
		GlobalEvents = GetNode<Events>("/root/Events");
		Scenes[ScenesEnum.Game] = GameScene;
		Scenes[ScenesEnum.MainMenu] = MenuScene;
		GlobalEvents.NewGame += () => TransitionToScene(ScenesEnum.Game);
		GlobalEvents.GameOver += () => TransitionToScene(ScenesEnum.MainMenu);
		GlobalEvents.ExitGame += Exit;
	}

	public void Exit()
	{
		GetTree().Quit();
	}

	/// <summary>
	/// Replace current scene by provided scene
	/// </summary>
	/// <param name="scene">The scene to transition to</param>
	public void TransitionToScene(ScenesEnum scene)
	{
		var currentScenes = GetChildren();
		foreach (Node sc in currentScenes)
		{
			RemoveChild(sc);
			sc.QueueFree();
		}
		var instance = Scenes[scene].Instantiate();
		AddChild(instance);
	}
}

public enum ScenesEnum
{
	MainMenu,
	Game
}
