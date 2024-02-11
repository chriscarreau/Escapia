using Godot;
using System;

public partial class FlyerThumbnail : Node2D
{
	
	private bool isDragging;
	private Vector2 dragOffset;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			var mousePos = GetGlobalMousePosition();
			if (isDragging) {
				Position = mousePos + dragOffset;
			}
			if (mousePos.X <= 0 || mousePos.X >= GetWindow().Size.X || mousePos.Y <= 0 || mousePos.Y >= GetWindow().Size.Y) {
				isDragging = false;
			}
		}
		else {
			isDragging = false;
		}
	}
}
