using Godot;

public partial class BranchBone : Bone2D
{
	public float baseAngle;
	public Trunk trunk;
	// Called when the node enters the scene tree for the first time.
	public BranchBone(Trunk trunk, float baseAngle)
	{
		this.trunk = trunk;
		this.baseAngle = baseAngle;
	}

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
