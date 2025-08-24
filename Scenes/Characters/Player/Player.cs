using Godot;
using System;

public partial class Player : CharacterBody3D
{
    const string MOVE_LEFT		= "move_left";
    const string MOVE_RIGHT		= "move_right";
    const string MOVE_FORWARD	= "move_forward";
    const string MOVE_BACK		= "move_back";
    const string ACTIVATE       = "activate";

    [Signal]
    public delegate void ActivateObjectEventHandler();

    [Export] public float Gravity = 9.81F;
    [Export] public float MoveSpeed = 500.0F;

    Vector3 _CurrMoveDirection = Vector3.Zero;
    Node3D _Pivot = null;

    public override void _Ready()
    {
        _Pivot = GetNode<Node3D>("Pivot");
        GD.Print("Player Ready!");
    }

    public override void _Process(double delta)
    {
        // Get current move direction
        _CurrMoveDirection = GetMoveDirection();

        // Handle activation event
        if (Input.IsActionJustPressed(ACTIVATE)) {
            EmitSignal(SignalName.ActivateObject);
        }

        // Update pivot rotation
        if (_CurrMoveDirection != Vector3.Zero)
        {
            Vector3 MoveTarget = Position + _CurrMoveDirection;
            _Pivot.LookAt(MoveTarget, Vector3.Up);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // Apply move velocity & gravity
        Velocity = _CurrMoveDirection * MoveSpeed * (float)(delta);
        Velocity += Vector3.Down * Gravity * 1_000.0F * (float)(delta);

        // Update position
        MoveAndSlide();
    }

    private Vector3 GetMoveDirection()
    {
        // Get current movement input
        Vector3 MoveDirection = Vector3.Zero;
        MoveDirection.X -= Input.GetActionStrength(MOVE_LEFT);
        MoveDirection.X += Input.GetActionStrength(MOVE_RIGHT);
        MoveDirection.Z -= Input.GetActionStrength(MOVE_FORWARD);
        MoveDirection.Z += Input.GetActionStrength(MOVE_BACK);
        if (MoveDirection.LengthSquared() > 1.0) {
            MoveDirection = MoveDirection.Normalized();
        }

        return MoveDirection;
    }
}
