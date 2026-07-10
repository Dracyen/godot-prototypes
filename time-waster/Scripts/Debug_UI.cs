using Godot;
using System;
using System.Collections.Generic;

public partial class Debug_UI : Control
{
    public static Debug_UI Instance { get; private set; }

    public RichTextLabel DebugLabel { get; private set; }

    public Dictionary<string, string> DebugInfoDictionary { get; private set; } = new Dictionary<string, string>();

    public override void _Ready()
    {
        if (Instance != null)
        {
            GD.PrintErr("Multiple instances of Debug_UI detected. This should not happen.");
            QueueFree();
            return;
        }

        Instance = this;

        DebugLabel = GetNode<RichTextLabel>("DebugLabel");
    }

     public override void _Process(double delta)
    {
        UpdateDebugDisplay();
    }

    private void UpdateDebugDisplay()
    {
        DebugLabel.Clear();
        foreach (var entry in DebugInfoDictionary)
        {
            DebugLabel.AddText($"{entry.Key}: {entry.Value}\n");
        }
    }

    public void AddInfo(string key, string value)
    {
        DebugInfoDictionary[key] = value;
    }

    public void UpdateInfo(string key, string value)
    {
        if (DebugInfoDictionary.ContainsKey(key))
        {
            DebugInfoDictionary[key] = value;
        }
        else
        {
            GD.PrintErr($"Key '{key}' not found in DebugInfoDictionary. Use AddInfo to add new entries.");
        }
    }

    public override void _ExitTree()
    {
        if (Instance == this)
            Instance = null;
    }
}
