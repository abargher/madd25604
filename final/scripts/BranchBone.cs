using Godot;
using System;

public partial class BranchBone : Bone2D
{
	[Signal]
	public delegate void GrowFinishedEventHandler();

	[Export]
	public float baseAngle = -90;
	public TreeTrunk trunk;

	public bool inGrowth = true;
	public float growthRate = 0.5f;  // percent/second
	private RandomNumberGenerator rng = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// register signal handler
		GrowFinished += trunk.OnGrowFinished;
		growthRate = rng.RandfRange(0.4f, 0.6f);


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
				EmitSignal(SignalName.GrowFinished);
			}
		}
	}
}
