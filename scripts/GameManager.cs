using Godot;
using System;

[GlobalClass]
public partial class GameManager : MainLoop
{
	private double _timeElapsed = 0;
	
	private static GameManager _instance;
	private LevelManager _levelManager;
	private SaveManager _saveManager;

	private string _scenePath;

	public static GameManager Get()
	{
		if (_instance == null)
		{
			_instance = new GameManager();
		}
		return _instance;
	}

	public LevelManager GetLevelManager()
	{
		if (_levelManager == null)
		{
			_levelManager = new LevelManager();
		}
		return _levelManager;
	}

	public SaveManager GetSaveManager()
	{
		if (_saveManager == null)
		{
			_saveManager = new SaveManager();
		}
		return _saveManager;
	}

	public string ScenePath
	{
		get { return _scenePath; }
		set { _scenePath = value; }
	}	
	public override void _Initialize()
	{
		GD.Print("Initialized:");
		GD.Print($"  Starting Time: {_timeElapsed}");
	}

	public override bool _Process(double delta)
	{
		_timeElapsed += delta;
		// Return true to end the main loop.
		return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
	}

	private void _Finalize()
	{
		GD.Print("Finalized:");
		GD.Print($"  End Time: {_timeElapsed}");
	}
}
