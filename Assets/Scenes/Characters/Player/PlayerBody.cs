using Godot;
using System;

public partial class PlayerBody : CharacterBody3D
{
    [Export]
    public float Gravity = 9.81F;

    [Export]
    public float MoveSpeed = 500.0F;

    public Player Player { get; private set; } = null;

    Vector3 _CurrMoveDirection = Vector3.Zero;

    public override void _PhysicsProcess(double delta)
    {
        // Apply move velocity & gravity
        Velocity = _CurrMoveDirection * MoveSpeed * (float)(delta);
        Velocity += Vector3.Down * Gravity * 1_000.0F * (float)(delta);

        // Update position
        MoveAndSlide();
        UpdateRotation();
    }

    public void Initialize(Player player)
    {
        Player = player;
    }

    public void SetMoveDirection(Vector3 dir)
    {
        _CurrMoveDirection = dir;
    }

    private void UpdateRotation()
    {
        // TODO(nemjit001): Rotate towards move direction instead of snapping
        if (_CurrMoveDirection != Vector3.Zero)
        {
            Basis = Basis.LookingAt(_CurrMoveDirection, Vector3.Up);
        }
    }
}
