using Godot;
using System;

public partial class RayCastSearch : Node3D
{
    [Signal]
    public delegate void TargetSeenEventHandler(Node3D target, Vector3 position);

    [Export]
    public float RayCastFOV = 60.0F;

    [Export]
    public uint RayCount = 10;

    [Export]
    public float RayCastLength = 10.0F;

    [Export(PropertyHint.Layers3DPhysics)]
    public uint CollisionMask = 0;

    [Export]
    public Godot.Collections.Array<CollisionObject3D> IgnoredNodes = [];

	public override void _PhysicsProcess(double delta)
	{
        // get ignored RIDs
        Godot.Collections.Array<Rid> exclude = new Godot.Collections.Array<Rid>();
        foreach (var node in IgnoredNodes) {
            exclude.Add(node.GetRid());
        }

        // Set up raycast queries
        float stepSize = RayCastFOV / RayCount;
        Vector3 start = GlobalPosition;
        for (int i = 0; i <= RayCount; i++)
        {
            float startAngle = -(RayCastFOV / 2.0F);
            float currentAngle = startAngle + stepSize * i;

            Vector3 forward = -GlobalTransform.Basis.Z;
            Vector3 direction = forward.Rotated(Vector3.Up, Mathf.DegToRad(currentAngle));
            Vector3 end = start + direction * RayCastLength;

            PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(start, end, CollisionMask, exclude);
            RayQuery(query);
        }
    }

    private void RayQuery(PhysicsRayQueryParameters3D query)
    {
        // Perform raycasts
        PhysicsDirectSpaceState3D space = GetWorld3D().DirectSpaceState;
        Godot.Collections.Dictionary result = space.IntersectRay(query);

        if (result.Count > 0) {
            EmitSignal(SignalName.TargetSeen, (Node3D)result["collider"], (Vector3)result["position"]);
        }
    }
}
