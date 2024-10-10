using Godot;

public partial class LevelManager : Node
{
	private Node currentLevel;

	public void LoadLevel(string scenePath)
	{
		// Load the specified scene
		PackedScene scene = ResourceLoader.Load<PackedScene>(scenePath);
		if (scene != null)
		{
			// If there is a current level, remove it from the scene tree
			if (currentLevel != null)
			{
				GetTree().GetRoot().RemoveChild(currentLevel);
				currentLevel.QueueFree();
			}

			// Instantiate the scene and add it to the current scene tree
			currentLevel = scene.Instantiate();
			GetTree().GetRoot().AddChild(currentLevel);
		}
		else
		{
			GD.PrintErr($"Failed to load scene: {scenePath}");
		}
	}
}
