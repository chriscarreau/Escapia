using Godot;
using System;

public partial class MainMenu : Control
{
	Events GlobalEvents;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalEvents = GetNode<Events>("/root/Events");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnStartPressed() {
		GlobalEvents.EmitSignal(Events.SignalName.NewGame);
	}
}
