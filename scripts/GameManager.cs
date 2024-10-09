using Godot;

namespace DefaultNamespace;
public class GameManager : SceneTree
{
    private static GameManager _instance;
    private LevelManager _levelManager;
    private SaveManager _saveManager;

    public override void _Ready()
    {
        _instance = this;
        
    }
    
    public LevelManager GetLevelManager() => _levelManager;
    
    public SaveManaga GetSaveManager() => _saveManager;


}