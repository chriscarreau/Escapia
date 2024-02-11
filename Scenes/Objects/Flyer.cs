using Godot;
using System;
using System.Collections.Generic;

public partial class Flyer : Node2D
{
	const int MAX_DRAG_REQUIRED = 200;
	Polygon2D SecondPage;
	AnimationPlayer AnimationPlayer;

	List<Texture2D> Pages;
	
	private bool isDragging;
	private bool isOpening;
	private Vector2 openingDragStart;
	private Vector2 dragOffset;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SecondPage = GetNode<Polygon2D>("SecondPage");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

    public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			isDragging = pressedEvent.Pressed;
			dragOffset = Position - GetGlobalMousePosition();
		}
    }

    public void OnOpenInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			isOpening = pressedEvent.Pressed;
			openingDragStart = GetGlobalMousePosition();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			var mousePos = GetGlobalMousePosition();
			if (isDragging) {
				Position = mousePos + dragOffset;
			}
			if (isOpening) {
				if (AnimationPlayer.CurrentAnimation != "Open") {
					AnimationPlayer.Play("Open", -1, 0);
				}
				var dragLength = mousePos.X - openingDragStart.X;
				double percentage = dragLength / MAX_DRAG_REQUIRED;
				percentage = Math.Clamp(AnimationPlayer.CurrentAnimationPosition + percentage, 0, 1);
				AnimationPlayer.Seek(percentage, true);
				openingDragStart = mousePos;
			}
			if (mousePos.X <= 0 || mousePos.X >= GetWindow().Size.X || mousePos.Y <= 0 || mousePos.Y >= GetWindow().Size.Y) {
				isDragging = false;
			}
		}
		else {
			isDragging = false;
			isOpening = false;
		}
	}
}
