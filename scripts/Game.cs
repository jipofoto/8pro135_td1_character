using Godot;
using System;

public partial class Game : Node
{
	private Button saveButton;
	private SaveManager saveManager;

	public override void _Ready()
	{
		GD.Print("Game Ready");
		saveButton = GetNode<Button>("SaveButton");
		saveManager = GameManager.Get().GetSaveManager();
		saveButton.Pressed += _on_save_pressed;
	}

	private void _on_save_pressed()
	{
		GD.Print("Save Button pressed");
		saveManager.SaveGame();
	}

	private void _on_load_pressed()
	{
		GD.Print("Load Button pressed");
		saveManager.LoadGame();
	}

	private void _on_quit_pressed()
	{
		GD.Print("Quit Button pressed");
		GetTree().Quit();
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
