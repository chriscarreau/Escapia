using Godot;
using System;
using System.Linq;

public partial class Pin : Node2D
{
	public bool IsOnMap;
	public bool isDragging;
	public Vector2 dragOffset;
	private CursorManager cursorManager;
	private Area2D MapArea;
	private State GameState;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState = GetNode<State>("/root/State");
		MapArea = GetParent().GetNode<Area2D>("%MapArea");
		cursorManager = GetNode<CursorManager>("/root/CursorManager");
		MapArea.MouseEntered += () => IsOnMap = true;
		MapArea.MouseExited += () => IsOnMap = false;
	}

	public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			cursorManager.SetCursorToFist();
			if (pressedEvent.Pressed) {
				isDragging = pressedEvent.Pressed;
			}
			dragOffset = Position - GetGlobalMousePosition();
		}
        else if (!isDragging)
        {
			cursorManager.SetCursorToHand();
        }
    }

	public void OnExit()
	{
		if (!isDragging)
		{
			cursorManager.SetCursorToPointer();
		}
	}
	
	public override void _Process(double delta) {
		if (Input.IsMouseButtonPressed(MouseButton.Left) && isDragging) {
			var mousePos = GetGlobalMousePosition();
			if (isDragging) {
				Position = mousePos + dragOffset;
			}
		}
		else {
			// If this was dragged outside of desk, remove it
			if (isDragging) {
				if (!IsOnMap) {
					QueueFree();
					return;
				}
				var oldPin = GetTree().GetNodesInGroup("Pins").FirstOrDefault(p => p != this);
				if (oldPin != null) {
					oldPin.QueueFree();
				}
				GameState.CurrentLocationGuess = MapArea.GetNode<Node2D>("TEST").ToLocal(ToGlobal(Position));
				GD.Print($"Position is [{GameState.CurrentLocationGuess.X}, {GameState.CurrentLocationGuess.Y}]");
			}
			isDragging = false;
		}
	}
}
