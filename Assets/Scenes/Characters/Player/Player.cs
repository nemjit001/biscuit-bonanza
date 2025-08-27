using Godot;
using System;

public partial class Player : Node3D
{
    const string MOVE_LEFT		= "move_left";
    const string MOVE_RIGHT		= "move_right";
    const string MOVE_FORWARD	= "move_forward";
    const string MOVE_BACK		= "move_back";
    const string ACTIVATE       = "activate";
    const string DROP_ITEM      = "drop_item";

    [Signal]
    public delegate void ActivateObjectEventHandler(Player player);

    [Export]
    public float CameraFollowSpeed = 5.0F;

    Vector3 _CurrMoveDirection = Vector3.Zero;
    PlayerBody _PlayerBody = null;
    Marker3D _CameraPivot = null;
    PackedScene _ItemScene = null;

    public override void _Ready()
    {
        _PlayerBody = GetNode<PlayerBody>("PlayerBody");
        _CameraPivot = GetNode<Marker3D>("CameraPivot");

        _PlayerBody.Initialize(this);

        GD.Print("Player Ready!");
    }

    public override void _Process(double delta)
    {
        // Get current move direction
        _CurrMoveDirection = GetMoveDirection();

        // Handle activation event
        if (Input.IsActionJustPressed(ACTIVATE)) {
            EmitSignal(SignalName.ActivateObject, this);
        }

        // Handle biscuit dropping
        if (Input.IsActionJustPressed(DROP_ITEM)) {
            DropBiscuits();
        }

        // Forward movement direction
        _PlayerBody.SetMoveDirection(_CurrMoveDirection);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Follow camera
        _CameraPivot.GlobalPosition = _CameraPivot.GlobalPosition.Lerp(_PlayerBody.GlobalPosition, CameraFollowSpeed * (float)delta);
    }

    public void CollectBiscuits(Biscuits biscuits)
    {
        _ItemScene = GD.Load<PackedScene>(biscuits.SceneFilePath);
    }

    public bool HasBiscuits()
    {
        return _ItemScene != null;
    }

    public void DropBiscuits()
    {
        if (!HasBiscuits()) {
            return;
        }

        Biscuits biscuits = _ItemScene.Instantiate<Biscuits>();
        biscuits.Position = _PlayerBody.GlobalPosition;

        Node3D root = GetOwner<Node3D>();
        root.AddChild(biscuits);

        GD.Print("Biscuits Dropped!");
        _ItemScene = null;
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
