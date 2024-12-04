using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
	private int numLeavesImpacted = 0;

	public int numBranchesCurrentLayer;
	public int numBranchesGrown = 0;

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
		rootBranch = GetNode<Bone2D>("TreeSkeleton/TrunkBone");
		// GD.Print("Root branch: ", rootBranch);
		Scale = new Vector2(1, 0);
		ZIndex = 1;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (inGrowth) {
			Scale = new Vector2(1, Math.Clamp(Scale.Y + (float)delta * growDecayRate, 0, 1));
			if (Scale.Y >= 1) {
				inGrowth = false;
				Scale = new Vector2(1, 1);
				CreateFirstLayer();
			}
		} else if (inDecay) {
			// TODO: fade out

			// shrink trunk vertically
			Scale = new Vector2(Scale.X, Math.Clamp(Scale.Y - (float)delta * growDecayRate, 0, 1));

			if (Scale.Y <= 0f) {
				QueueFree();
			}
		}

	}

	// set branch's length, random center angle, and other fields
	private BranchBone CreateNewBranch()
	{
		// 20 degrees on either side of vertical.
		float angle = gen.RandfRange(-40f, 40f);
		BranchBone branch = branchBoneScene.Instantiate() as BranchBone;
		branch.trunk = this;
		branch.baseAngle = angle;
		branch.RotationDegrees = angle;
		branch.Position = new Vector2(0, -100);
		branch.Rest = branch.Transform;

		return branch;
	}

	public void CreateFirstLayer()
	{
		List<BranchBone> firstLayer = new();
		for (int i = 0; i < gen.RandiRange(minBranches, maxBranches); i++) {
			BranchBone branch = CreateNewBranch();
			rootBranch.AddChild(branch);
			firstLayer.Add(branch);
		}
		branchLayers.Push(firstLayer);
		numBranchesCurrentLayer = firstLayer.Count;
		currentLayer += 1;
	}

	public void CreateLayer()
	{
		if (currentLayer > maxLayers) {
			GD.PrintErr("Max layers passed");
			return;
		} else if (currentLayer == maxLayers) {
			currentLayer += 1;
			leafCount = 0;
			GD.Print("Leaf layer reached, creating leaves");

			foreach (BranchBone branch in branchLayers.Peek()) {
				leafCount += 1;
				Leaf leaf = leafScene.Instantiate() as Leaf;
				leaf.trunk = this;

				// add leaf to main scene, not as child of branch
				Vector2 offset = new((float)(branch.GetLength() * Math.Sin(branch.GlobalRotation)),
									-(float)(branch.GetLength() * Math.Cos(branch.GlobalRotation)));
				leaf.Position = branch.GlobalPosition + offset;
				mainNode.AddChild(leaf);

				leaves.Add(leaf);
			}
			GD.PrintErr("numPods: ", numPods);
			List<Leaf> chosenLeaves = leaves.OrderBy(x => gen.RandiRange(0, leaves.Count - 1)).Take(numPods).ToList();
			GD.Print("Chosen leaves: ", chosenLeaves.Count);

			foreach (Leaf seedPod in chosenLeaves) {
				seedPod.isSeedPod = true;
				GD.Print("setting seed pod true");
			}

			return;
		}

		currentLayer += 1;
		List<BranchBone> newLayer = new();

		foreach(BranchBone oldBranch in branchLayers.Peek()) {
			int newBranchCount = gen.RandiRange(minBranches, maxBranches);

			for (int branchNum = 0; branchNum < newBranchCount; branchNum++) {
				BranchBone branch = CreateNewBranch();
				oldBranch.AddChild(branch);
				newLayer.Add(branch);
			}
		}
		branchLayers.Push(newLayer);
		numBranchesCurrentLayer = newLayer.Count;

	}

	public void Decay()
	{
		inDecay = true;
	}

	public void OnGrowFinished()
	{
		numBranchesGrown += 1;
		// GD.Print("Grow finished");

		if (numBranchesGrown == numBranchesCurrentLayer) {
			numBranchesGrown = 0;
			// GD.Print("Creating new layer");
			CreateLayer();
		}
	}

	public void OnLeafImpact(TreeTrunk trunk)
	{
		if (trunk == this) {
			numLeavesImpacted += 1;
			if (numLeavesImpacted >= leafCount - numPods - 2) {
				GD.Print("All leaves impacted");
				Decay();
			}
		}
	}

}
