using Godot;
using System;

public partial class TreeManager : Node
{

	[Export]
	public PackedScene trunkScene {get; set;}

	[Export]
	public int numTrees = 3;

	public Tuple<int, int> seedPodRange = new Tuple<int, int>(2, 5);

	public int numSeedPods;
	private RandomNumberGenerator gen = new();
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		numSeedPods = gen.RandiRange(seedPodRange.Item1, seedPodRange.Item2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnLeafImpact(bool isSeedPod)
	{
		GD.Print("Leaf from is a seed pod?: ", isSeedPod);
	}
}
