using Godot;
using System;

public partial class CursorManagerProxy : Node
{
	private CursorManager cm;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cm = GetNode<CursorManager>("/root/CursorManager");
	}

	public void Reset()
	{
		cm.ResetCursor();
	}

	public void SetMouth()
	{
		cm.SetCursorToMouth();
	}

	public void SetHand()
	{
		cm.SetCursorToHand();
	}

	public void SetHand90()
	{
		cm.SetCursorToHand90();
	}

	public void SetFist()
	{
		cm.SetCursorToFist();
	}

	public void SetFist90()
	{
		cm.SetCursorToFist90();
	}

	public void SetEye()
	{
		cm.SetCursorToEye();
	}

	public void ResetCursor()
	{
		cm.SetCursorToPointer();
	}

}
