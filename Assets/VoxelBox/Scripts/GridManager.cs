using Godot;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using VoxelBoxUtilities;

public partial class GridManager : Node3D
{
    [ExportGroup("Dimensions:")]
    [Export] int blockPerRowAmount = 10;
	[Export] Vector3 gridTotalSize;

    [ExportGroup("References:")]
	[Export] MeshInstance3D highlight;
    [Export] Vector3 gridOrigin;
    [Export] PackedScene blockPrefab;
    [Export] Node nodeParent;

    BlockType[,,] gridTypes;
    MeshInstance3D[,,] gridNodes;

    Vector3 blockSize;

    BlockType selectedBlockType;

    Random rng = new Random();

    double timer = 0;

    public override void _Ready()
    {
        blockSize = gridTotalSize / blockPerRowAmount;
        gridOrigin = GlobalPosition - blockPerRowAmount * blockSize / 2;
		gridOrigin.Y = GlobalPosition.Y + Scale.Y / 2;

        gridTypes = new BlockType[blockPerRowAmount, blockPerRowAmount, blockPerRowAmount];
        gridNodes = new MeshInstance3D[blockPerRowAmount, blockPerRowAmount, blockPerRowAmount];

        highlight.Scale = new Vector3(blockSize.X, 0.1f, blockSize.Z);

        selectedBlockType = BlockType.Water;

        for (int x = 0; x < gridTypes.GetLength(0); x++)
            for (int y = 0; y < gridTypes.GetLength(0); y++)
                for (int z = 0; z < gridTypes.GetLength(0); z++)
                {
                    gridTypes[x, y, z] = BlockType.Air;
                    Node3D tempNode = (Node3D)ResourceLoader.Load<PackedScene>(blockPrefab.ResourcePath).Instantiate();
                    MeshInstance3D tempMesh = tempNode.GetChild<MeshInstance3D>(0);

                    nodeParent.AddChild(tempNode);

                    BlockType[] values = Data.Blocks.Keys.ToArray();
                    BlockType randomBlockType = values[rng.Next(values.Length)];

                    tempMesh.SetSurfaceOverrideMaterial(0, Data.Blocks[randomBlockType]);
                    tempNode.Scale = blockSize;
                    tempNode.Position = GridToWorldPosition(x, y, z);

                    gridNodes[x, y, z] = tempMesh;
                }
    }

    public override void _Process(double delta)
    {
        if (timer < 1)
        {
            timer += delta;
            return;
        }

        for (int x = 0; x < gridTypes.GetLength(0); x++)
            for (int y = 0; y < gridTypes.GetLength(0); y++)
                for (int z = 0; z < gridTypes.GetLength(0); z++)
                {
                    BlockType[] values = Data.Blocks.Keys.ToArray();
                    BlockType randomBlockType = values[rng.Next(values.Length)];

                    gridTypes[x, y, z] = randomBlockType;

                    ThreadPool.QueueUserWorkItem(UpdateVoxel, new VoxelInfo(x, y, z, gridTypes[x, y, z]));
                }

        timer = 0;
    }

    public void UpdateGridSelection(Vector3 point)
	{
        Vector3I gridPos = WorldToGridPosition(point);

        Vector3 newPos = GridToWorldPosition(gridPos);

        newPos.Y = 0.05f;

        newPos += gridOrigin;

        highlight.GlobalPosition = newPos;
    }

    private void UpdateVoxel(Object stateInfo)
    {
        VoxelInfo info = (VoxelInfo)stateInfo;

        gridNodes[info.X, info.Y, info.Z].SetSurfaceOverrideMaterial(0, Data.Blocks[info.type]);
    }

    private Vector3 GridToWorldPosition(int x, int y, int z)
    {
        return GridToWorldPosition(new Vector3I(x, y, z));
    }

    private Vector3 GridToWorldPosition(Vector3I gridPos)
	{
		Vector3 pos = Vector3.Zero;
		
		pos.X = gridPos.X * blockSize.X + blockSize.X / 2;
        pos.Y = gridPos.Y * blockSize.Y + blockSize.Y / 2;
        pos.Z = gridPos.Z * blockSize.Z + blockSize.Z / 2;

        return pos;
	}

    private Vector3I WorldToGridPosition(Vector3 pos)
    {
        float percentage;

        Vector3I gridPos = Vector3I.Zero;

        pos -= gridOrigin;

        percentage = pos.X / Scale.X;
		gridPos.X = Mathf.FloorToInt(blockPerRowAmount * percentage);
		percentage = pos.Y / Scale.Y;
        gridPos.Y = Mathf.FloorToInt(blockPerRowAmount * percentage);
		percentage = pos.Z / Scale.Z;
        gridPos.Z = Mathf.FloorToInt(blockPerRowAmount * percentage);

        return gridPos;
    }
}