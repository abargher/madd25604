using Godot;
using System;

public partial class BranchBone : Bone2D
{
	public Trunk trunk;
	// Called when the node enters the scene tree for the first time.
	public BranchBone(Trunk trunk)
	{
		this.trunk = trunk;
	}

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
