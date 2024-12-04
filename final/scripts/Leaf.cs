using Godot;
using System;

public partial class Leaf : CharacterBody2D
{
	public bool isSeedPod = false;
	private const float decayRate = 0.2f;
	private const float Gravity = 100.0f;
	private bool decay = false;
	private float _growProgress = 0;

	public const float maxAngle = 50;
	private float angle = 0;
	private const float spinSpeed = 10f;
	public Vector2 maxScale = new(0.15f, 0.15f);
	private float growSpeed;
	private bool gravityOn = false;
	private RandomNumberGenerator rng = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Scale = Vector2.Zero;
		RotationDegrees = rng.RandfRange(-maxAngle, maxAngle);
		growSpeed = rng.RandfRange(0.07f, 0.1f);	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_growProgress < 0.15f) {
			_growProgress = Math.Min(_growProgress + growSpeed * (float)delta, 0.15f);
			Scale = new Vector2(_growProgress, _growProgress);
		} else {
			gravityOn = true;
		}

		if (decay) {
			Scale -= new Vector2(decayRate * (float)delta, decayRate * (float)delta);
			if (Scale[0] <= 0f) {
				QueueFree();
			}
		}

	}
	public override void _PhysicsProcess(double delta)
    {
		if (!gravityOn) {
			return;
		}

        var velocity = Velocity;
        velocity.Y += (float)delta * Gravity;
        Velocity = velocity;

        var motion = velocity * (float)delta;

		/*
		angle = (angle + spinSpeed * (float)delta) % 360;
		RotationDegrees = angle;
		Position += new Vector2((float)Math.Sin(angle) * 20, 0);
		*/

        var collision = MoveAndCollide(motion);

		if (collision != null) {
			decay = true;
		}
    }
}
