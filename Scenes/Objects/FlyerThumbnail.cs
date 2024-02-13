using Godot;
using System;

public partial class FlyerThumbnail : Node2D
{
	[Export]
	public FlyerResource FlyerData;

	private PackedScene FlyerScene = ResourceLoader.Load<PackedScene>("res://Scenes/Objects/Flyer.tscn");
	private bool isDragging;
	private Vector2 dragOffset;
	private Vector2 dragStart;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (FlyerData != null) {
			GetNode<Sprite2D>("Sprite2D").Texture = FlyerData.Pages[0];
		}
	}

    public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
    {
        if (@event is InputEventMouseButton pressedEvent) {
			isDragging = pressedEvent.Pressed;
			dragOffset = Position - GetGlobalMousePosition();
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
		if(FlyerData == null) {
			return;
		}
		var flyer = FlyerScene.Instantiate<Flyer>();
		flyer.Pages = FlyerData.Pages;
		flyer.isDragging = true;
		flyer.ZIndex = 100;
		GetParent().AddChild(flyer);
	}
}
