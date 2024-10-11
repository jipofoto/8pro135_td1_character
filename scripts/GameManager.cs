using Godot;
using System;

[GlobalClass]
public partial class GameManager : SceneTree
{
	private double timeElapsed = 0;
	private bool isMenuOpen = false;

	private static GameManager instance;
	private LevelManager levelManager;
	private SaveManager saveManager;
	private CanvasLayer menuCanvasLayer;

	private string scenePath = "res://Scenes/game.tscn";

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
	
		// menuCanvasLayer = GetTree().GetNode<CanvasLayer>("MenuCanvasLayer");
		menuCanvasLayer.Hide();
	}
	public override bool _Process(double delta)
	{
		timeElapsed += delta;

		if (Input.IsKeyPressed(Key.Escape))
		{
			isMenuOpen = !isMenuOpen;
			if (isMenuOpen)
			{
				menuCanvasLayer.Show();
			}
			else
			{
				menuCanvasLayer.Hide();
			}
			if (Input.IsActionJustPressed("quit"))
			{
				QuitGame();
			}

			// Return true to end the main loop.
			return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
		}

		// Return true to end the main loop.
		return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
	}

	public override void _Finalize()
	{
		GD.Print("Finalized:");
		GD.Print($"  End Time: {timeElapsed}");
	}

	public void SaveGame()
	{
		saveManager.SaveGame();
		isMenuOpen = false;
		menuCanvasLayer.Hide();
	}

	public void LoadGame()
	{
		saveManager.LoadGame();
		isMenuOpen = false;
		menuCanvasLayer.Hide();
	}

	public void QuitGame()
	{
		// GetTree().Quit();
	}
	}