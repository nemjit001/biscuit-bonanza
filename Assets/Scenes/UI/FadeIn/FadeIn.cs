using Godot;
using System;

public partial class FadeIn : Control
{
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("fade");
		GetNode<Timer>("FadeTimer").Timeout += OnAnimationComplete;
	}

	public void OnAnimationComplete()
	{
		QueueFree();
	}
}
