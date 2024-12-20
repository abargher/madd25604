using System;
using Godot;

public partial class PlantTree : Area2D
{
	private const float MAX_GROW_VEL = 10f;
	public const int maxDepth = 3;
	private const float decayRate = 0.25f;

	public float maxAngle = 10;
	public bool hasLeaves = false;
	public bool decay = false;

	public PackedScene BranchScene = GD.Load<PackedScene>("res://plant_tree.tscn");

	[Export]
	public PackedScene LeafScene { get; set; }

	[Export]
	public int MoveSpeed { get; set; } = 200; // How fast the tree moves by default.

	public bool createdBranches = false;

	[Export]
	public int RemainingBranches { get; set; } = maxDepth;

	public float angle = 0;

	private float growSpeed;
	private float _growProgress = 0;

	private RandomNumberGenerator rng = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		angle = rng.RandfRange(-maxAngle, maxAngle);
		RotationDegrees = angle;
		Scale = new Vector2(1, 0);

		growSpeed = rng.RandfRange(0.7f, 0.9f);	
		AddToGroup("trees");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (_growProgress < 1.0) {
			_growProgress = Math.Min(_growProgress + growSpeed * (float)delta, 1.0f);
			Scale = new Vector2(1, _growProgress);
		} else if (!createdBranches && RemainingBranches > 0) {
			for (int i = 0; i < rng.RandiRange(2, 4); i++) {
				PlantTree branch = BranchScene.Instantiate<PlantTree>();
				
				branch.Position = new Vector2(0, -150);
				branch.MoveSpeed = 0;
				branch.RemainingBranches = Math.Max(RemainingBranches - 1, 0);
				branch.maxAngle = maxAngle + 15;

				AddChild(branch);
			}
			createdBranches = true;
		} else if (RemainingBranches == 0 && !hasLeaves) {
			// generate leaves
			for (int i = 0; i < 2; i++) {
				Leaf leaf = LeafScene.Instantiate<Leaf>();
				leaf.Position = new Vector2(rng.RandiRange(-10, 10), rng.RandiRange(-10, 10) -180);
				AddChild(leaf);
			}
			hasLeaves = true;
		}

		if (decay) {
			Scale -= new Vector2(decayRate * (float)delta, decayRate * (float)delta);
			if (Scale[0] <= 0f) {
				QueueFree();
			}
		}
	}

	private void OnVisibleScreenNotifier2DScreenExited()
	{
		QueueFree();
	}

	public void EnableDecay()
	{
		decay = true;
	}
}
