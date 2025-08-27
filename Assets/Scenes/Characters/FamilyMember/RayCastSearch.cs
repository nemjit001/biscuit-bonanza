using Godot;
using System;

public partial class RayCastSearch : Node3D
{
    [Signal]
    public delegate void TargetSeenEventHandler(Node3D target, Vector3 position);

    [Export]
    public bool ShowDebugMesh = false;

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

    MeshInstance3D _DebugMesh = null;
    ImmediateMesh _Raymesh = null;

    public override void _Ready()
    {
        _Raymesh = new ImmediateMesh();
        _DebugMesh = new MeshInstance3D();
        _DebugMesh.Mesh = _Raymesh;

        AddChild(_DebugMesh);
    }

	public override void _PhysicsProcess(double delta)
	{
        // get ignored RIDs
        Godot.Collections.Array<Rid> exclude = new Godot.Collections.Array<Rid>();
        foreach (var node in IgnoredNodes)
        {
            exclude.Add(node.GetRid());
        }

        // Clear debug ray mesh
        _Raymesh.ClearSurfaces();

        // Set up raycast queries
        float stepSize = RayCastFOV / RayCount;
        Vector3 start = GlobalPosition;
        for (int i = 0; i <= RayCount; i++)
        {
            float startAngle = -(RayCastFOV / 2.0F);
            float currentAngle = startAngle + stepSize * i;

            // Calc end position of ray cast
            Vector3 forward = -GlobalTransform.Basis.Z;
            Vector3 direction = forward.Rotated(Vector3.Up, Mathf.DegToRad(currentAngle));
            Vector3 end = start + direction * RayCastLength;

            // Add debug mesh if required
            if (ShowDebugMesh)
            {
                _Raymesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
                _Raymesh.SurfaceAddVertex(ToLocal(start));
                _Raymesh.SurfaceAddVertex(ToLocal(end));
                _Raymesh.SurfaceEnd();
            }

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
