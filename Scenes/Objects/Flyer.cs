using Godot;
using Godot.Collections;
using System;

public partial class Flyer : Node2D
{
	const int MAX_DRAG_REQUIRED = 200;
	public AnimationPlayer AnimationPlayer;
	public Array<Texture2D> Pages;
	public bool IsOnDesk;
	public bool isDragging;
	public Vector2 dragOffset;

	private CursorManager cursorManager;
	private bool isOpening;
	private Vector2 openingDragStart;
	
	private Area2D DeskArea;

	private Label isDraggingLbl;
	private Label isOnDeskLbl;

	private Sprite2D shadow;
	private State GameState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		if (Pages != null){
			var CoverPage = GetNode<Polygon2D>("FirstPage");
			CoverPage.Texture = Pages[0];
			var SecondPage = GetNode<Polygon2D>("SecondPage");
			SecondPage.Texture = Pages[1];
			var ThirdPage = GetNode<Polygon2D>("ThirdPage");
			ThirdPage.Texture = Pages[2];
		}
		GameState = GetNode<State>("/root/State");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		cursorManager = GetNode<CursorManager>("/root/CursorManager");
		DeskArea = GetParent().GetNode<Area2D>("%DeskArea");
		isDraggingLbl = GetNode<Label>("isDragging");
		isOnDeskLbl = GetNode<Label>("isOnDesk");
		shadow = GetNode<Sprite2D>("Shadow");
		DeskArea.MouseEntered += () => IsOnDesk = true;
		DeskArea.MouseExited += () => IsOnDesk = false;
	}

	public void OnExit()
	{
		if (!isDragging && !isOpening)
		{
			cursorManager.SetCursorToPointer();
		}
	}

    public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			cursorManager.SetCursorToFist();
			if (pressedEvent.Pressed && GameState.CurrentDraggedObject == null) {
				GameState.CurrentDraggedObject = this;
				var flyers = GetTree().GetNodesInGroup("Flyers");
				foreach (var flyer in flyers) {
					((Flyer)flyer).ZIndex = 0;
				}
				isDragging = true;
				ZIndex = 100;
			}
			dragOffset = Position - GetGlobalMousePosition();
		}
        else if (!isDragging && !isOpening)
        {
			cursorManager.SetCursorToHand();
        }
    }

    public void OnOpenInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			if (pressedEvent.Pressed && GameState.CurrentDraggedObject == null) {
				GameState.CurrentDraggedObject = this;
				cursorManager.SetCursorToFist90();
				isOpening = true;
				openingDragStart = GetGlobalMousePosition();
			}
		}
        else if (!isDragging && !isOpening)
        {
			cursorManager.SetCursorToHand90();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
		shadow.Visible = isDragging;
		isDraggingLbl.Visible = isDragging;
		isOnDeskLbl.Visible = IsOnDesk;
		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			var mousePos = GetGlobalMousePosition();
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
			if (isDragging) {
				Position = mousePos + dragOffset;
				if (mousePos.X <= 0 || mousePos.X >= GetWindow().Size.X || mousePos.Y <= 0 || mousePos.Y >= GetWindow().Size.Y) {
					GameState.CurrentDraggedObject = null;
					isDragging = false;
					ZIndex = 10;
					// If this was dragged outside of desk, remove it
					if (!IsOnDesk) {
						QueueFree();
					}
				}
			}
		}
		else {
			if (isDragging || isOpening) {
				GameState.CurrentDraggedObject = null;
				ZIndex = 10;
			}
			// If this was dragged outside of desk, remove it
			if (isDragging && !IsOnDesk) {
				QueueFree();
			}
			isDragging = false;
			isOpening = false;
		}
	}
}
