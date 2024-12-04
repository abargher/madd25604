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
	public int maxSeedPods = 4;

	public int numSeedPods;
	private int numSeedPodImpacts = 0;

	private RandomNumberGenerator gen = new();

	private List<Vector2> treePositions = new();
	private Node mainNode;
	

	private void SetTreePositions(bool first)
	{
		Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
		for (int i = 0; i < numSeedPods; i++) {
			float newX = gen.RandfRange(50, viewportSize.X - 50);
			float newY = gen.RandfRange(viewportSize.Y * 0.95f, viewportSize.Y - 10);
			treePositions.Add(new Vector2(newX, newY));
		}

		numSeedPods = gen.RandiRange(minSeedPods, maxSeedPods);

		// deal out all new seed pod counts
		List<int> seedPods = Enumerable.Repeat(0, numSeedPods).ToList();


		int currPodIndex = 0;
		int seedPodsTracker = numSeedPods;
		while (seedPodsTracker > 0) {
			seedPods[currPodIndex] += 1;
			seedPodsTracker -= 1;
			currPodIndex = (currPodIndex + 1) % seedPods.Count;
		}

		if (first) {
			for (int i = 0; i < treePositions.Count; i++) {
				EmitSignal(SignalName.GrowSeedPod, treePositions[i],
				seedPods[i]);
			}
		}
		treePositions.Clear();

	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mainNode = GetNode<Node>("/root/Main");

		numSeedPods = gen.RandiRange(minSeedPods, maxSeedPods);

		SetTreePositions(true);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnLeafImpact(TreeTrunk trunk, bool isSeedPod)
	{
		if (isSeedPod) {
			numSeedPodImpacts += 1;
			EmitSignal(SignalName.GrowSeedPod, trunk.GlobalPosition, gen.RandiRange(minSeedPods, maxSeedPods));
			if (numSeedPodImpacts == numSeedPods) {
				GD.PrintErr("All seed pods have been impacted!");
				numSeedPods = gen.RandiRange(minSeedPods, maxSeedPods);
				numSeedPodImpacts = 0;
				SetTreePositions(false);
			}
		}
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
