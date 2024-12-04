using Godot;
using System;
using System.Collections.Generic;

public partial class TreeTrunk : Node2D
{
	private Node mainNode;
	[Export]
	public PackedScene branchBoneScene {get; set;}
	[Export]
	public PackedScene leafScene {get; set;}

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
	public const float growDecayRate = 0.5f;  // percent/second
	public bool inDecay = false;
	public bool inGrowth = true;


	// append a new list of branches on each layer
	public int currentLayer = 0;
	private Stack<List<BranchBone>> branchLayers = new();
	private List<Leaf> leaves = new();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mainNode = GetNode<Node>("/root/Main");
		rootBranch = GetNode<BranchBone>("TrunkBone");
		GD.Print("Root branch: ", rootBranch);
		Scale = new Vector2(1, 0);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (inGrowth) {
			Scale = new Vector2(1, Math.Clamp(Scale.Y + (float)delta * growDecayRate, 0, 1));
			if (Scale.Y >= 1) {
				inGrowth = false;
				Scale = new Vector2(1, 1);
				// TODO: create first layer of branches
				CreateLayer();
			}
		} else if (inDecay) {
			// TODO: fade out

			// shrink trunk vertically
			Scale = new Vector2(Scale.X, Math.Clamp(Scale.Y - (float)delta * growDecayRate, 0, 1));

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

			foreach (BranchBone branch in branchLayers.Peek()) {
				leafCount += 1;
				Leaf leaf = leafScene.Instantiate() as Leaf;

				// add leaf to main scene, not as child of branch
				Vector2 worldSpaceCoord = ToGlobal(branch.Position);
				leaf.Position = worldSpaceCoord;
				mainNode.AddChild(leaf);

				leaves.Add(leaf);
			}

			return;
		} else if (currentLayer == 0) {
			// create first layer of branches
			List<BranchBone> firstLayer = new();
			int firstBranchCount = gen.RandiRange(minBranches, maxBranches);

			for (int branchNum = 0; branchNum < firstBranchCount; branchNum++) {
				// -180 is left, 0 is right. 20 degrees on either side of vertical.
				float branchAngle = gen.RandfRange(-110f, -70f);

				// set branch's length, random center angle, and other fields
				BranchBone branch = branchBoneScene.Instantiate() as BranchBone;
				GD.PrintErr("Branch: ", branch);
				branch.trunk = this;
				branch.baseAngle = branchAngle;

				rootBranch.AddChild(branch);
				firstLayer.Add(branch);
			}
			branchLayers.Push(firstLayer);
		}

		currentLayer += 1;
		List<BranchBone> newLayer = new();

		foreach(BranchBone oldBranch in branchLayers.Peek()) {
			int newBranchCount = gen.RandiRange(minBranches, maxBranches);

			for (int branchNum = 0; branchNum < newBranchCount; branchNum++) {
				// -180 is left, 0 is right. 20 degrees on either side of vertical.
				float branchAngle = gen.RandfRange(-110f, -70f);

				// set branch's length, random center angle, and other fields
				BranchBone branch = branchBoneScene.Instantiate() as BranchBone;
				branch.trunk = this;
				branch.baseAngle = branchAngle;

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
