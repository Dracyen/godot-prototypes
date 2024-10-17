using Godot;
using VoxelBoxUtilities;

public partial class LevelButton : TextureButton
{
	[Export]
	PackedScene targetScene;

    public void LoadScene()
	{
        GamemodeManager.Instance.LoadScene(targetScene);
    }
}