using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class CharacterController : Node
{
	private Node2D _head;
	private Node2D _hair;
	private Node2D _nose;
	private Node2D _eyes;
	private Node2D _mouth;
	private Node2D _body;

	private List<SpriteFrames> _availableHeads;
	private List<SpriteFrames> _availableBodies;
	private List<SpriteFrames> _availableNoses;
	private List<SpriteFrames> _availableEyes;
	private List<SpriteFrames> _availableMouths;
	private List<SpriteFrames> _availableHairs;


	private CursorManager _cm;

	private double _nextBlink;

	private double _blinkAcc;

	private string[] talks = new[] { "Talk", "Talk2", "Talk3"};


	private bool _talking = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_cm = GetNode<CursorManager>("/root/CursorManager");
		_head = GetNode<Node2D>("Head");
		_hair = GetNode<Node2D>("Hair");
		_nose = GetNode<Node2D>("Nose");
		_eyes = GetNode<Node2D>("Eyes");
		_mouth = GetNode<Node2D>("Mouth");
		_body = GetNode<Node2D>("Body");
		
		LoadAllHeads();
		LoadAllBodies();
		LoadAllHairs();
		LoadAllEyes();
		LoadAllNoses();
		LoadAllMouths();
		
		_nextBlink = new Random().Next(200, 700) / 150.0;
	}

	public void Blink()
	{
		_eyes.GetNode<AnimatedSprite2D>("Sprite").Play("Blink");
	}

	public void StopBlinking()
	{
		_eyes.GetNode<AnimatedSprite2D>("Sprite").Play("default");
	}

	public void Talk()
	{
		_talking = true;
		_mouth.GetNode<AnimatedSprite2D>("Sprite").Play("Talk");
	}

	public void OnTalkEnd()
	{
		if (_talking)
		{
			string talk = talks[new Random().Next(0, 3)];
			_mouth.GetNode<AnimatedSprite2D>("Sprite").Play(talk);
		}
	}

	public void StopTalking()
	{
		_talking = false;
		_mouth.GetNode<AnimatedSprite2D>("Sprite").Play("default");
	}

	public void OnMouseInput(Viewport viewport, InputEvent @event, int index)
	{
		if (!_talking)
		{
			_cm.SetCursorToMouth();
		}
		else
		{
			_cm.SetCursorToPointer();
		}

		if (@event is InputEventMouseButton pressedEvent)
		{
			if (!_talking && pressedEvent.IsPressed())
			{
				 Talk();
			}
		}
	}

	public void OnMouseExit()
	{
		_cm.SetCursorToPointer();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_blinkAcc += delta;
		if (_blinkAcc >= _nextBlink)
		{
			_blinkAcc = 0;
			_nextBlink = new Random().Next(200, 700) / 150.0;
			Blink();
		}
	}

	public void ReGenerate()
	{
		ReGenerateNode(_head, _availableHeads);
		ReGenerateNode(_hair, _availableHairs);
		ReGenerateNode(_body, _availableBodies);
		ReGenerateNode(_nose, _availableNoses);
		ReGenerateNode(_eyes, _availableEyes);
		ReGenerateNode(_mouth, _availableMouths);
	}

	private void ReGenerateNode(Node2D node, List<SpriteFrames> availableSprites)
	{
		node.GetNode<AnimatedSprite2D>("Sprite").SpriteFrames =
			availableSprites[new Random().Next(0, availableSprites.Count)];
	}


	private void LoadAllHeads()
	{
		_availableHeads = new();
		string[] allHeads = DirAccess.Open("res://Assets/Animations/Heads").GetFiles();
		foreach (string head in allHeads)
		{
			_availableHeads.Add(ResourceLoader.Load<SpriteFrames>($"res://Assets/Animations/Heads/{head}"));
		}
	}
	
	private void LoadAllHairs()
	{
		_availableHairs = new();
		string[] allHeads = DirAccess.Open("res://Assets/Animations/Hairs").GetFiles();
		foreach (string head in allHeads)
		{
			_availableHairs.Add(ResourceLoader.Load<SpriteFrames>($"res://Assets/Animations/Hairs/{head}"));
		}
	}
	
	private void LoadAllBodies()
	{
		_availableBodies = new();
		string[] allHeads = DirAccess.Open("res://Assets/Animations/Bodies").GetFiles();
		foreach (string head in allHeads)
		{
			_availableBodies.Add(ResourceLoader.Load<SpriteFrames>($"res://Assets/Animations/Bodies/{head}"));
		}
	}
	
	private void LoadAllEyes()
	{
		_availableEyes = new();
		string[] allHeads = DirAccess.Open("res://Assets/Animations/Eyes").GetFiles();
		foreach (string head in allHeads)
		{
			_availableEyes.Add(ResourceLoader.Load<SpriteFrames>($"res://Assets/Animations/Eyes/{head}"));
		}
	}
	
	private void LoadAllNoses()
	{
		_availableNoses = new();
		string[] allHeads = DirAccess.Open("res://Assets/Animations/Noses").GetFiles();
		foreach (string head in allHeads)
		{
			_availableNoses.Add(ResourceLoader.Load<SpriteFrames>($"res://Assets/Animations/Noses/{head}"));
		}
	}
	
	private void LoadAllMouths()
	{
		_availableMouths = new();
		string[] allHeads = DirAccess.Open("res://Assets/Animations/Mouths").GetFiles();
		foreach (string head in allHeads)
		{
			_availableMouths.Add(ResourceLoader.Load<SpriteFrames>($"res://Assets/Animations/Mouths/{head}"));
		}
	}
}
