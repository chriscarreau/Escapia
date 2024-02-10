using Godot;
using System;

public partial class Flyer : Node2D
{
	StaticBody2D DragArea;
	
	private bool isDragging;
	private Vector2 dragOffset;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DragArea = GetNode<StaticBody2D>("DragArea");
	}

    public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			isDragging = pressedEvent.Pressed;
			dragOffset = Position - GetGlobalMousePosition();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
		if (Input.IsMouseButtonPressed(MouseButton.Left) && isDragging) {
			Position = GetGlobalMousePosition() + dragOffset;
		}
	}
}
