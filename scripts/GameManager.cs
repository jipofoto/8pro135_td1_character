using Godot;
using System;

[GlobalClass]
public partial class GameManager : SceneTree
{
	private double timeElapsed = 0;
	
	private static GameManager instance;
	private LevelManager levelManager;
	private SaveManager saveManager;

	private string scenePath = "res://Scenes/test.tscn";

	public static GameManager Get()
	{
		if (instance == null)
		{
			instance = new GameManager();
		}
		return instance;
	}

	public LevelManager GetLevelManager()
	{
		if (levelManager == null)
		{
			levelManager = new LevelManager();
		}
		return levelManager;
	}

	public SaveManager GetSaveManager()
	{
		if (saveManager == null)
		{
			saveManager = new SaveManager();
		}
		return saveManager;
	}

	public string ScenePath
	{
		get { return scenePath; }
		set { scenePath = value; }
	}	
	public override void _Initialize()
	{
		GD.Print("Initialized:");
		GD.Print($"  Starting Time: {timeElapsed}");
		levelManager = GetLevelManager();
		levelManager.LoadLevel(scenePath);
	}

	public override bool _Process(double delta)
	{
		timeElapsed += delta;
		// Return true to end the main loop.
		return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
	}

	private void _Finalize()
	{
		GD.Print("Finalized:");
		GD.Print($"  End Time: {timeElapsed}");
	}
}