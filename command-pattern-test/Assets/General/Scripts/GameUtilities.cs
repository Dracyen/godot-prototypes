using Godot;
using Godot.Collections;
using System;

namespace VoxelBoxUtilities
{
    public enum BlockType
    {
        Sand = 0,
        Water = 1,
        Air = 2
    }

    public class Data
    {
        public static Dictionary<BlockType, Material> Blocks = new Dictionary<BlockType, Material>
        {
            { BlockType.Sand, (Material)GD.Load("res://Assets/VoxelBox/Materials/M_Sand.tres") },
            { BlockType.Water, (Material)GD.Load("res://Assets/VoxelBox/Materials/M_Water.tres") },
            { BlockType.Air, (Material)GD.Load("res://Assets/VoxelBox/Materials/M_Air.tres") }
        };
    }

    public struct VoxelInfo
    {
        public int X = 0;
        public int Y = 0;
        public int Z = 0;

        public BlockType type = BlockType.Air;

        public VoxelInfo(int X, int Y, int Z, BlockType type)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            this.type = type;
        }
    }

    public class GameActions
    {
        public static Action PlaceVoxel;

        public static Action RemoveVoxel;

        public static Action TogglePauseMenu;

        public static Action ToggleBlock1;

        public static Action ToggleBlock2;

        public static Action ToggleBlock3;

        public static Action ToggleBlock4;

        public static Action ToggleBlock5;
    }
}

namespace LaserCutterUtilities
{
    public class GameActions
    {
        public static Action Fire1;

        public static Action Fire2;

        public static Action ToggleEquip;

        public static Action<float> Elevate;

        public static Action<float> Rotate;

        public static Action<Vector2> Move;

        public static Action TogglePauseMenu;
    }
}

namespace HordeFighterUtilities
{
    public class GameActions
    {
        public static Action<Vector2> Move;

        public static Action TogglePauseMenu;
    }
}