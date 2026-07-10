using Godot;
using System;
using System.Diagnostics;

public partial class GamemodeManager : Node
{
    [Export]
    private Control MainMenu;

    public static GamemodeManager Instance { get; private set; }

    public Gamemode CurrentGamemode { get { return (Gamemode)LoadedScene.GetScript(); } }

    public static Node LoadedScene { get; private set; }

    public override void _Ready()
	{
        if (Instance == null)
            Instance = this;
    }

    public void LoadScene(PackedScene level)
    {
        UnloadScene();

        Node scene = ResourceLoader.Load<PackedScene>(level.ResourcePath).Instantiate();

        GetTree().Root.AddChild(scene);

        LoadedScene = scene;

        MainMenu.SetProcess(false);
        MainMenu.Hide();
    }

    public void UnloadScene()
    {
        if (LoadedScene == null)
            return;

        LoadedScene.Free();

        LoadedScene = null;

        MainMenu.SetProcess(true);
        MainMenu.Show();
    }
}