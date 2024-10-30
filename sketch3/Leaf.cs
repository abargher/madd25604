using Godot;
using System;

public partial class Leaf : CharacterBody2D
{
	private const float Gravity = 200.0f;

	public const float maxAngle = 50;
	public Vector2 maxScale = new(0.15f, 0.15f);
	private RandomNumberGenerator rng = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RotationDegrees = rng.RandfRange(-maxAngle, maxAngle);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        velocity.Y += (float)delta * Gravity;
        Velocity = velocity;

        var motion = velocity * (float)delta;
        MoveAndCollide(motion);
    }
}
