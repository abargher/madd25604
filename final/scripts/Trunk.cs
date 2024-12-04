using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class Trunk : Node2D
{
	
	[Export]
	public int maxLayers = 4;

	[Export]
	public int minBranches = 2;
	[Export]
	public int maxBranches = 4;

	private RandomNumberGenerator gen = new();


	// append a new list of branches on each layer
	public int currentLayer = 0;
	private Stack<List<Bone2D>> branchLayers = new();
	private List<Leaf> leaves = new();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CreateLayer()
	{
		if (currentLayer >= maxLayers) {
			Debug.WriteLine("Max layers reached");
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
				newLayer.Append(branch);
			}
		}
		branchLayers.Push(newLayer);


	}
}
