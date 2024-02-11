using Godot;
using System;

public partial class CursorReset : Node
{
	private CursorManager _cursorManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_cursorManager = GetNode<CursorManager>("/root/CursorManager");
	}
	
    public void ResetMouseEvent(Viewport viewport, InputEvent @event, int index)
    {
	    if (@event is InputEventMouseButton buttonEvent)
	    {
		    if (!buttonEvent.IsPressed())
		    {
				 _cursorManager.SetCursorToPointer();
		    }
	    }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
