using System;
using Godot;

public partial class PlantTree : Area2D
{
	private const float MAX_GROW_VEL = 10f;
	private const int maxDepth = 3;

	public float maxAngle = 10;

	public PackedScene BranchScene = GD.Load<PackedScene>("res://plant_tree.tscn");

	[Export]
	public PackedScene TreeScene { get; set; }

	[Export]
	public int MoveSpeed { get; set; } = 200; // How fast the tree moves by default.

	public bool createdBranches = false;

	[Export]
	public int RemainingBranches { get; set; } = maxDepth;

	public float angle = 0;

	private float growSpeed;
	private float _growProgress = 0;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		angle = rng.RandfRange(-maxAngle, maxAngle);
		RotationDegrees = angle;
		Scale = new Vector2(1, 0);

		growSpeed = rng.RandfRange(0.7f, 0.9f);	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_growProgress < 1.0) {
			_growProgress = Math.Min(_growProgress + growSpeed * (float)delta, 1.0f);
			Scale = new Vector2(1, _growProgress);
		}

		var moveVelocity = new Vector2(x: 1, y: 0);

		if (Input.IsActionPressed("wind_on")) {
			moveVelocity = new Vector2(x: 2, y: 0);
		}

		if (Input.IsActionJustPressed("new_tree") && !createdBranches && RemainingBranches > 0) {
			for (int i = 0; i < rng.RandiRange(2, 4); i++) {
				PlantTree branch = BranchScene.Instantiate<PlantTree>();
				
				branch.Position = new Vector2(0, -150);
				branch.MoveSpeed = 0;
				branch.RemainingBranches = Math.Max(RemainingBranches - 1, 0);
				branch.maxAngle = maxAngle + 15;

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
