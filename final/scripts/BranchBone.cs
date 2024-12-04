using Godot;
using System;

public partial class BranchBone : Bone2D
{
	[Export]
	public float baseAngle = -90;
	public TreeTrunk trunk;

	public bool inGrowth = true;
	public const float growthRate = 0.5f;  // percent/second

	public BranchBone(TreeTrunk trunk, float baseAngle)
	{
		this.trunk = trunk;
		this.baseAngle = baseAngle;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RotationDegrees = baseAngle;
		Scale = new Vector2(1, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (inGrowth) {
			Scale = new Vector2(1, Math.Clamp(Scale.Y + (float)delta * growthRate, 0, 1));
			if (Scale.Y >= 1) {
				inGrowth = false;
				Scale = new Vector2(1, 1);
			}
		}
	}
}
