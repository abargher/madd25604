using Godot;
using System;
using System.Collections.Generic;

public partial class TreeGardener : Node2D
{
	[Signal]
	public delegate void GrowSeedPodEventHandler(Vector2 position);

	[Export]
	public PackedScene trunkScene {get; set;}

	[Export]
	public int numTrees = 3;

	[Export]
	public int minSeedPods = 2;
	[Export]
	public int maxSeedPods = 5;

	public int numSeedPods;

	private RandomNumberGenerator gen = new();

	private List<Vector2> treePositions = new();
	private Node mainNode;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mainNode = GetNode<Node>("/root/Main");
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		GD.PrintErr("Viewport size: ", viewportSize);

		numSeedPods = gen.RandiRange(minSeedPods, maxSeedPods);

		for (int i = 0; i < numSeedPods; i++) {
			float newX = gen.RandfRange(50, viewportSize.X - 50);
			float newY = gen.RandfRange(viewportSize.Y * 0.95f, viewportSize.Y - 10);
			treePositions.Add(new Vector2(newX, newY));
		}

		foreach (Vector2 pos in treePositions) {
			EmitSignal(SignalName.GrowSeedPod, pos);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnLeafImpact(TreeTrunk trunk, bool isSeedPod)
	{
		// GD.Print("Leaf from is a seed pod?: ", isSeedPod);
	}

	public void OnGrowSeedPod(Vector2 position)
	{
		TreeTrunk newTrunk = trunkScene.Instantiate<TreeTrunk>();
		AddChild(newTrunk);
		newTrunk.GlobalPosition = position;
		GD.Print("Seed pod grown at: ", position);
		GD.Print("New trunk position: ", newTrunk.GlobalPosition);
	}
}
