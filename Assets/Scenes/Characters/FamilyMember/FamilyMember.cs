using Godot;
using System;

public partial class FamilyMember : CharacterBody3D
{
	const string LOUIE_GROUP = "louie";

	[Export] public float MoveSpeed = 500.0F;
	[Export] public float Gravity = 9.81F;

	Node3D _Pivot = null;
	NavigationAgent3D _NavAgent = null;

	public override void _Ready()
	{
		Area3D captureArea = GetNode<Area3D>("Pivot/CaptureArea");
		captureArea.BodyEntered += OnBodyEntered;

		_Pivot = GetNode<Node3D>("Pivot");
		_NavAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		_NavAgent.VelocityComputed += OnVelocityComputed;

		GD.Print("FamilyMember Ready!");
	}

	public override void _Process(double delta)
	{
		if (_NavAgent.IsNavigationFinished())
		{
			// Update path position
			// TODO(nemjit001): If chasing and player is still visible this is the player position
			// else this is the next patrol point
			MoveToTarget(Vector3.Zero);
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		if (_NavAgent.IsNavigationFinished()) {
			return; // Nothing to do here :)
		}

		// Move to next path position using physics movement
        Vector3 moveTarget = _NavAgent.GetNextPathPosition();
        Vector3 moveDirection = GlobalPosition.DirectionTo(moveTarget);
		moveDirection.Y = 0; // We only care about x/z plane movement
        if (moveDirection.LengthSquared() > 1.0) {
            moveDirection = moveDirection.Normalized();
        }

		// Update character rotation
		if (moveDirection != Vector3.Zero) {
			_Pivot.Basis = Basis.LookingAt(moveDirection, Vector3.Up);
		}

		// Update movement velocity
		Vector3 velocity = moveDirection * MoveSpeed * (float)delta;
		velocity += Vector3.Down * Gravity * 1_000.0F * (float)(delta);

		if (_NavAgent.AvoidanceEnabled) {
			_NavAgent.Velocity = velocity;
		}
		else {
			OnVelocityComputed(velocity);
		}

		MoveAndSlide();
    }

	public void MoveToTarget(Vector3 target)
	{
        _NavAgent.TargetPosition = target;
    }

	public void OnVelocityComputed(Vector3 velocity)
	{
		Velocity = velocity;
        MoveAndSlide();
    }

    public void OnBodyEntered(Node3D node)
	{
		GD.Print("Did we catch him?");
		if (node.IsInGroup(LOUIE_GROUP)) {
			Player player = (Player)node;
			if (player.HasBiscuits()) {
				GD.Print("We caught him!");
				ScoreManager.Instance.Score.TimesCaught += 1;
				// TODO(nemjit001): Restart level
			}
		}
	}
}
