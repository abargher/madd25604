using Godot;

public partial class BranchBone : Bone2D
{
	[Export]
	public float baseAngle = -90;
	public TreeTrunk trunk;

	public BranchBone(TreeTrunk trunk, float baseAngle)
	{
		this.trunk = trunk;
		this.baseAngle = baseAngle;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RotationDegrees = baseAngle;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
