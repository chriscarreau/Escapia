using Godot;
using System;
using System.Collections.Generic;

public partial class CursorManager : Node
{
	private Resource pointer;
	private Resource pointer2;
	private Resource pointer3;
	
	private Resource mouthClosed;
	private Resource mouthSemi;
	private Resource mouthOpened;
	
	private Resource eyeOpened;
	private Resource eyeSemi;
	private Resource eyeClosed;

	private Resource handOpened;
	private Resource handSemi;
	private Resource handClosed;
	
	private Resource hand90Opened;
	private Resource hand90Semi;
	private Resource hand90Closed;
	
	private Resource fist1;
	private Resource fist2;
	private Resource fist3;
	
	private Resource fist190;
	private Resource fist290;
	private Resource fist390;
	
	private Vector2 cursorCenter;

	private double timeAcc = 0;

	private double timePerFrame = 0.12f;

	private int currentFrame = 0;
	private int frameIncrement = 1;
	private List<Resource> cursorFrames;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pointer = ResourceLoader.Load("res://Assets/Images/Cursor/Pointer/Pointer.png");
		pointer2 = ResourceLoader.Load("res://Assets/Images/Cursor/Pointer/Pointer2.png");
		pointer3 = ResourceLoader.Load("res://Assets/Images/Cursor/Pointer/Pointer3.png");
		
		mouthClosed = ResourceLoader.Load("res://Assets/Images/Cursor/Mouth/Mouth-Closed.png");
		mouthSemi = ResourceLoader.Load("res://Assets/Images/Cursor/Mouth/Mouth-Semi.png");
		mouthOpened = ResourceLoader.Load("res://Assets/Images/Cursor/Mouth/Mouth-Open.png");
		
		eyeOpened = ResourceLoader.Load("res://Assets/Images/Cursor/Eye/Eye-Open.png");
		eyeSemi = ResourceLoader.Load("res://Assets/Images/Cursor/Eye/Eye-Semi.png");
		eyeClosed = ResourceLoader.Load("res://Assets/Images/Cursor/Eye/Eye-Closed.png");
		
		
		handOpened = ResourceLoader.Load("res://Assets/Images/Cursor/Hand/Hand-Open.png");
		handSemi = ResourceLoader.Load("res://Assets/Images/Cursor/Hand/Hand-Semi.png");
		handClosed = ResourceLoader.Load("res://Assets/Images/Cursor/Hand/Hand-Closed.png");
		
		hand90Opened = ResourceLoader.Load("res://Assets/Images/Cursor/Hand90/Hand90-Open.png");
		hand90Semi = ResourceLoader.Load("res://Assets/Images/Cursor/Hand90/Hand90-Semi.png");
		hand90Closed = ResourceLoader.Load("res://Assets/Images/Cursor/Hand90/Hand90-Closed.png");
		
		fist1 = ResourceLoader.Load("res://Assets/Images/Cursor/Fist/Fist1.png");
		fist2 = ResourceLoader.Load("res://Assets/Images/Cursor/Fist/Fist2.png");
		fist3 = ResourceLoader.Load("res://Assets/Images/Cursor/Fist/Fist3.png");
		
		fist190 = ResourceLoader.Load("res://Assets/Images/Cursor/Fist90/Fist1.png");
		fist290 = ResourceLoader.Load("res://Assets/Images/Cursor/Fist90/Fist2.png");
		fist390 = ResourceLoader.Load("res://Assets/Images/Cursor/Fist90/Fist3.png");
		
		cursorCenter = new Vector2(50, 50);
		
		cursorFrames = new();
		SetCusorToPointer();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (cursorFrames.Count > 1)
		{
			UpdateCursorFrame(delta);
		}
	}

	public void SetCusorToPointer()
	{
		ResetAnim();
		cursorFrames.Add(pointer);
		cursorFrames.Add(pointer2);
		cursorFrames.Add(pointer3);
		Input.SetCustomMouseCursor(pointer, 0, cursorCenter);
	}

	public void SetCursorToHand()
	{
		ResetAnim();
		cursorFrames.Add(handOpened);
		cursorFrames.Add(handSemi);
		cursorFrames.Add(handClosed);
		Input.SetCustomMouseCursor(cursorFrames[0], 0, cursorCenter);
	}
	
	public void SetCursorToHand90()
	{
		ResetAnim();
		cursorFrames.Add(hand90Opened);
		cursorFrames.Add(hand90Semi);
		cursorFrames.Add(hand90Closed);
		Input.SetCustomMouseCursor(cursorFrames[0], 0, cursorCenter);
	}

	public void SetCursorToEye()
	{
		ResetAnim();
		cursorFrames.Add(eyeClosed);
		cursorFrames.Add(eyeSemi);
		cursorFrames.Add(eyeOpened);
		Input.SetCustomMouseCursor(cursorFrames[0], 0, cursorCenter);
	}

	public void SetCursorToMouth()
	{
		ResetAnim();
		cursorFrames.Add(mouthClosed);
		cursorFrames.Add(mouthSemi);
		cursorFrames.Add(mouthOpened);
		Input.SetCustomMouseCursor(cursorFrames[0], 0, cursorCenter);
	}

	public void SetCursorToFist()
	{
		ResetAnim();
		cursorFrames.Add(fist1);
		cursorFrames.Add(fist2);
		cursorFrames.Add(fist3);
		Input.SetCustomMouseCursor(cursorFrames[0], 0, cursorCenter);
	}
	
	public void SetCursorToFist90()
	{
		ResetAnim();
		cursorFrames.Add(fist190);
		cursorFrames.Add(fist290);
		cursorFrames.Add(fist390);
		Input.SetCustomMouseCursor(cursorFrames[0], 0, cursorCenter);
	}
	
	public void ResetAnim()
	{
		cursorFrames.Clear();
		timeAcc = 0;
		currentFrame = 0;
		frameIncrement = 1;
	}

	private void UpdateCursorFrame(double delta)
	{
		timeAcc += delta;
		if (timeAcc >= timePerFrame)
		{
			timeAcc = 0;
			if (currentFrame + frameIncrement > cursorFrames.Count - 1|| currentFrame + frameIncrement < 0)
			{
				frameIncrement = frameIncrement * -1;
			}

			currentFrame += frameIncrement;
			Input.SetCustomMouseCursor(cursorFrames[currentFrame], 0, new Vector2(50, 50));
		}
	}
}
