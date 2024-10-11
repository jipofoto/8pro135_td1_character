using Godot;
using System;

public partial class Character : CharacterBody2D
{
	[Export]
	public float MoveSpeed = 200;

	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * MoveSpeed;
			animatedSprite2D.Play();

			if (velocity.X < 0)
			{
				animatedSprite2D.Animation = "move_left";
			}
			else if (velocity.X > 0)
			{
				animatedSprite2D.Animation = "move_right";
			}
			else if (velocity.Y < 0)
			{
				animatedSprite2D.Animation = "move_up";
			}
			else if (velocity.Y > 0)
			{
				animatedSprite2D.Animation = "move_down";
			}
		}
		else
		{
			animatedSprite2D.Stop();
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		return new Godot.Collections.Dictionary<string, Variant>()
		{
			{ "Filename", SceneFilePath },
			{ "Parent", GetParent().GetPath() },
			{ "Position", Position },
			{ "MoveSpeed", MoveSpeed },
			// Add any other variables that you want to save here
		};
	}
}