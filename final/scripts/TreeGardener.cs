using Godot;
using System;
using System.Collections.Generic;

public partial class TreeManager : Node
{

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
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		numSeedPods = gen.RandiRange(minSeedPods, maxSeedPods);
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		for (int i = 0; i < numSeedPods; i++) {
			float newX = gen.RandfRange(50, viewportSize.X - 50);
			float newY = gen.RandfRange(viewportSize.Y * 0.95f, viewportSize.Y * 0.8f);
			treePositions.Add(new Vector2(newX, newY));
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
}
