using Godot;
using System;

public partial class Character : CharacterBody2D
{
	[Export]
	public float MoveSpeed = 200;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 inputDirection = new Vector2(
			Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up")
		);

		// Update velocity
		Vector2 velocity = inputDirection * MoveSpeed;

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * MoveSpeed;
			GetNode<AnimationPlayer>("AnimatedSprite2D").Play();
		}
		else
		{
			GetNode<AnimationPlayer>("AnimatedSprite2D").Stop();
		}

		if (velocity.X < 0)
		{
			GetNode<AnimationPlayer>("AnimatedSprite2D").Play("move_left");
		}
		if (velocity.X > 0)
		{
			GetNode<AnimationPlayer>("AnimatedSprite2D").Play("move_right");
		}
		if (velocity.Y < 0)
		{
			GetNode<AnimationPlayer>("AnimatedSprite2D").Play("move_up");
		}
		if (velocity.Y > 0)
		{
			GetNode<AnimationPlayer>("AnimatedSprite2D").Play("move_down");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}