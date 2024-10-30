using Godot;

public partial class PlantTree : Area2D
{
	private const float GROW_VEL_SCALE = 10f;

	public PackedScene BranchScene = GD.Load<PackedScene>("res://plant_tree.tscn");

	[Export]
	public PackedScene TreeScene { get; set; }

	[Export]
	public int MoveSpeed { get; set; } = 200; // How fast the tree moves by default.


	public float angle = 0;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		angle = rng.RandfRange(-30, 30);
		RotationDegrees = angle;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var growVelocity = rng.RandfRange(0, GROW_VEL_SCALE);

		var moveVelocity = new Vector2(x: 1, y: 0);

		if (Input.IsActionPressed("wind_on")) {
			moveVelocity = new Vector2(x: 2, y: 0);
		}

		if (Input.IsActionJustPressed("new_tree")) {
			PlantTree branch = BranchScene.Instantiate<PlantTree>();
			// var endPos = GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().End;
			
			branch.Position = new Vector2(0, -100);
			branch.MoveSpeed = 0;

			AddChild(branch);

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
