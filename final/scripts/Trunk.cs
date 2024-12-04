using Godot;
using System;
using System.Collections.Generic;

public partial class Trunk : Node2D
{
	private RandomNumberGenerator gen = new();

	[Export]
	public int maxLayers = 4;

	[Export]
	public int minBranches = 2;
	[Export]
	public int maxBranches = 4;

	// How many of this Trunk's leaves will be seed pods
	public int numPods;
	public int leafCount = -1;  // no one should ever see this inital value

	public Bone2D rootBranch;
	public const float decayRate = 0.15f;  // percent/second
	public bool inDecay = false;


	// append a new list of branches on each layer
	public int currentLayer = 0;
	private Stack<List<Bone2D>> branchLayers = new();
	private List<Leaf> leaves = new();



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rootBranch = GetNode<Bone2D>("TrunkBone");
		GD.Print("Root branch: ", rootBranch);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (inDecay) {
			// TODO: fade out

			// shrink trunk vertically
			Scale = new Vector2(Scale.X, Math.Clamp(Scale.Y - (float)delta * decayRate, 0, 1));

			if (Scale.Y <= 0.5f) {
				QueueFree();
			}
		}
	}


	public void CreateLayer()
	{
		if (currentLayer > maxLayers) {
			GD.Print("Max layers passed");
			return;
		} else if (currentLayer == maxLayers) {
			currentLayer += 1;
			leafCount = 0;
			GD.Print("Leaf layer reached, creating leaves");

			foreach (Bone2D branch in branchLayers.Peek()) {
				leafCount += 1;
				Leaf leaf = new Leaf();
				branch.AddChild(leaf);
				leaves.Add(leaf);
			}

			return;
		}

		currentLayer += 1;
		List<Bone2D> newLayer = new List<Bone2D>();

		foreach(Bone2D oldBranch in branchLayers.Peek()) {
			int newBranchCount = gen.RandiRange(minBranches, maxBranches);

			for (int branchNum = 0; branchNum < newBranchCount; branchNum++) {
				Bone2D branch = new Bone2D();
				// set branch's length, random center angle, and other fields
				oldBranch.AddChild(branch);
				newLayer.Add(branch);
			}
		}
		branchLayers.Push(newLayer);

	}

	public void Decay()
	{
		inDecay = true;
	}
}
