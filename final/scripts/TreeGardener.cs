using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TreeGardener : Node2D
{
	[Signal]
	public delegate void GrowSeedPodEventHandler(Vector2 position, int numNewSeedPods);

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

		// deal out all seed pod counts
		List<int> seedPods = Enumerable.Repeat(0, numSeedPods).ToList();


		int currPodIndex = 0;
		while (numSeedPods > 0) {
			seedPods[currPodIndex] += 1;
			numSeedPods -= 1;
			currPodIndex = (currPodIndex + 1) % seedPods.Count;
		}

		for (int i = 0; i < treePositions.Count; i++) {
			EmitSignal(SignalName.GrowSeedPod, treePositions[i], seedPods[i]);
		}
		treePositions.Clear();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnLeafImpact(TreeTrunk trunk, bool isSeedPod)
	{
		// GD.Print("Leaf from is a seed pod?: ", isSeedPod);
	}

	public void OnGrowSeedPod(Vector2 position, int numNewSeedPods)
	{
		TreeTrunk newTrunk = trunkScene.Instantiate<TreeTrunk>();
		AddChild(newTrunk);
		newTrunk.GlobalPosition = position;
		newTrunk.numPods = numNewSeedPods;
		GD.Print("Seed pod grown at: ", position);
	}
}
