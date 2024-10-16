using System;
using Godot;

public partial class SaveManager : Node
{
	// Note: This can be called from anywhere inside the tree. This function is
	// path independent.
	// Go through everything in the persist category and ask them to return a
	// dict of relevant variables.
	public void SaveGame()
	{
		try
		{
			using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

			var saveNodes = GetTree().GetNodesInGroup("Persist");
			foreach (Node saveNode in saveNodes)
			{
				// Check the node is an instanced scene so it can be instanced again during load.
				if (string.IsNullOrEmpty(saveNode.SceneFilePath))
				{
					GD.PrintErr($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
					continue;
				}

				// Check the node has a save function.
				if (!saveNode.HasMethod("Save"))
				{
					GD.PrintErr($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
					continue;
				}

				// Call the node's save function.
				var nodeData = saveNode.Call("Save");

				// Json provides a static method to serialized JSON string.
				var jsonString = Json.Stringify(nodeData);

				// Store the save dictionary as a new line in the save file.
				saveFile.StoreLine(jsonString);
			}
		}
		catch (Exception ex)
		{
			GD.PrintErr($"Error while saving game: {ex.Message}");
		}
	}

	// Note: This can be called from anywhere inside the tree. This function is
	// path independent.
	public void LoadGame()
	{
	    try
	    {
	        if (!FileAccess.FileExists("user://savegame.save"))
	        {
	            GD.PrintErr("No savegame.save file found. Starting a new game.");
	            return;
	        }

	        // We need to revert the game state so we're not cloning objects during loading.
	        var saveNodes = GetTree().GetNodesInGroup("Persist");
	        foreach (Node saveNode in saveNodes)
	        {
	            saveNode.QueueFree();
	        }

	        // Load the file line by line and process that dictionary to restore the object
	        // it represents.
	        using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);

	        while (saveFile.GetPosition() < saveFile.GetLength())
	        {
	            var jsonString = saveFile.GetLine();

	            // Creates the helper class to interact with JSON.
	            var json = new Json();
	            var parseResult = json.Parse(jsonString);
	            if (parseResult != Error.Ok)
	            {
	                GD.PrintErr($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
	                continue;
	            }

	            // Get the data from the JSON object.
	            var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

	            // Firstly, we need to create the object and add it to the tree and set its position.
	            var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
	            var newObject = newObjectScene.Instantiate<Node>();
	            GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
	            newObject.Set(Node2D.PropertyName.Position, (Vector2)nodeData["Position"]);


	            // Now we set the remaining variables.
	            foreach (var (key, value) in nodeData)
	            {
	                if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
	                {
	                    continue;
	                }
	                newObject.Set(key, value);
	            }
	        }
	    }
	    catch (Exception ex)
	    {
	        GD.PrintErr($"Error while loading game: {ex.Message}");
	    }
	}

}