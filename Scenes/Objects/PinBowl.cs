using Godot;
using System;

public partial class PinBowl : Node2D
{
	private PackedScene PinScene = ResourceLoader.Load<PackedScene>("res://Scenes/Objects/Pin.tscn");
	private bool isDragging;
	private Vector2 dragStart;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			isDragging = pressedEvent.Pressed;
			dragStart = GetGlobalMousePosition();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
		if (Input.IsMouseButtonPressed(MouseButton.Left) && isDragging) {
			if(dragStart != GetGlobalMousePosition()) {
				SpawnNewFlyer();
				isDragging = false;
			}
		}
		else {
			isDragging = false;
		}
	}

	public void SpawnNewFlyer() {
		var pin = PinScene.Instantiate<Pin>();
		pin.isDragging = true;
		GetParent().AddChild(pin);
	}
}
