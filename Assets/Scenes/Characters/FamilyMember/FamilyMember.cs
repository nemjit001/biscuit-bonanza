using Godot;
using System;

public partial class FamilyMember : CharacterBody3D
{
	const string LOUIE_GROUP = "louie";

    [Export]
	public float MoveSpeed = 500.0F;

    [Export]
    public float RotationSpeed = 20.0F;

	[Export]
	public float ChaseDuration = 10.0F;

	Vector3 _CurrMoveDirection = Vector3.Zero;

	bool _IsChasing = false;
    Vector3 _ChaseTarget = Vector3.Zero;

    Node3D _Pivot = null;
	Timer _ChaseTimer = null;
	NavigationAgent3D _NavAgent = null;
	RayCastSearch _LouieSearch = null;

	public override void _Ready()
	{
		Area3D caputureArea = GetNode<Area3D>("Pivot/CaptureArea");
        caputureArea.BodyEntered += OnBodyEntered;

		_Pivot = GetNode<Node3D>("Pivot");

        _ChaseTimer = GetNode<Timer>("ChaseTimer");
        _ChaseTimer.Timeout += OnChaseEnd;

        _NavAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
		_NavAgent.VelocityComputed += OnVelocityComputed;

		_LouieSearch = GetNode<RayCastSearch>("Pivot/RayCastSearch");
		_LouieSearch.TargetSeen += OnLouieSeen;

        GD.Print("FamilyMember Ready!");
	}

	public override void _Process(double delta)
	{
		if (_IsChasing) {
			MoveToTarget(_ChaseTarget);
		}
		else {
			MoveToTarget(Vector3.Zero); // TODO(nemjit001): Move to random patrol point in scene
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		UpdateNavigation(delta);
		MoveAndSlide();
    }

	// Move this family member to a target position
	public void MoveToTarget(Vector3 target)
	{
        _NavAgent.TargetPosition = target;
    }

	// Handle velocity update by nav agent
	public void OnVelocityComputed(Vector3 velocity)
	{
		Velocity = velocity;
        MoveAndSlide();
    }

	// Handle possible capture of Louie
    public void OnBodyEntered(Node3D node)
	{
		if (!node.IsInGroup(LOUIE_GROUP)) {
			return;
		}

        PlayerBody body = (PlayerBody)node;
		if (!body.Player.HasBiscuits()) {
			return;
		}

		GD.Print("We caught him!");
		ScoreManager.Instance.Score.TimesCaught += 1;
		// TODO(nemjit001): Restart level
	}

	// Handle possible louie sighting
	public void OnLouieSeen(Node3D target, Vector3 position)
	{
		if (!target.IsInGroup(LOUIE_GROUP)) {
			return;
		}

        PlayerBody body = (PlayerBody)target;
        if (!body.Player.HasBiscuits()) { // Don't chase if they don't have biscuits
            return;
		}

		GD.Print($"We see him with the biscuits!");
		_IsChasing = true;
        _ChaseTarget = target.GlobalPosition;
		_ChaseTimer.Start(ChaseDuration);
	}

	public void OnChaseEnd()
	{
		_IsChasing = false;
		_ChaseTarget = Vector3.Zero;
	}

	private void UpdateNavigation(double delta)
	{
        if (_NavAgent.IsNavigationFinished()) {
            return; // Nothing to do here :)
        }

        // Move to next path position using physics movement
        Vector3 moveTarget = _NavAgent.GetNextPathPosition();
        _CurrMoveDirection = GlobalPosition.DirectionTo(moveTarget);
        _CurrMoveDirection.Y = 0; // We only care about x/z plane movement
        if (_CurrMoveDirection.LengthSquared() > 1.0) {
            _CurrMoveDirection = _CurrMoveDirection.Normalized();
        }

        // Update character rotation
        if (_CurrMoveDirection != Vector3.Zero) {
            _Pivot.Quaternion = _Pivot.Quaternion.Slerp(Basis.LookingAt(_CurrMoveDirection, Vector3.Up).GetRotationQuaternion(), RotationSpeed * (float)delta);
        }

        // Update movement velocity
        Vector3 velocity = _CurrMoveDirection * MoveSpeed * (float)delta;
        velocity += GetGravity();

        if (_NavAgent.AvoidanceEnabled) {
            _NavAgent.Velocity = velocity;
        }
        else {
            OnVelocityComputed(velocity);
        }
    }
}
