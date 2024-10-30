using System;
using Godot;

public partial class PlantTree : Area2D
{
	private const float MAX_GROW_VEL = 10f;

	public PackedScene BranchScene = GD.Load<PackedScene>("res://plant_tree.tscn");

	[Export]
	public PackedScene TreeScene { get; set; }

	[Export]
	public int MoveSpeed { get; set; } = 200; // How fast the tree moves by default.

	public bool createdBranches = false;

	[Export]
	public int RemainingBranches { get; set; } = 3;

	public float angle = 0;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		angle = rng.RandfRange(-40, 40);
		RotationDegrees = angle;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var growVelocity = rng.RandfRange(0, MAX_GROW_VEL);

		var moveVelocity = new Vector2(x: 1, y: 0);

		if (Input.IsActionPressed("wind_on")) {
			moveVelocity = new Vector2(x: 2, y: 0);
		}

		if (Input.IsActionJustPressed("new_tree") && !createdBranches && RemainingBranches > 0) {
			for (int i = 0; i < rng.RandiRange(1, 3); i++) {
				PlantTree branch = BranchScene.Instantiate<PlantTree>();
				
				branch.Position = new Vector2(0, -150);
				branch.MoveSpeed = 0;
				branch.RemainingBranches = Math.Max(RemainingBranches - 1, 0);

				AddChild(branch);
			}
			createdBranches = true;
		}

		if (moveVelocity.Length() > 0) {
			moveVelocity = moveVelocity.Normalized() * MoveSpeed;
		}

		Position += moveVelocity * (float)delta;
	}

	private void OnVisibleScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
