using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; } = null;

	Node _CurrentScene = null;

	public override void _Ready()
	{
		Instance = this;
		_CurrentScene = GetTree().Root.GetChild(-1);

		GD.Print("GameManager Ready!");
	}

	public void LoadScene(string path)
	{
		// Ensure call happens after scene update
		CallDeferred(MethodName.DeferredLoadScene, path);
	}

	public void DeferredLoadScene(string path)
	{
		PackedScene next = GD.Load<PackedScene>(path);
		if (next == null)
		{
			GD.PrintErr($"Could not load scene from path {path}");
			return;
		}

        _CurrentScene.Free();
        _CurrentScene = next.Instantiate();

		SceneTree scene = GetTree();
		scene.Root.AddChild(_CurrentScene);
		scene.CurrentScene = _CurrentScene;
	}

	public void QuitGame()
	{
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
		GetTree().Quit();
	}
}
