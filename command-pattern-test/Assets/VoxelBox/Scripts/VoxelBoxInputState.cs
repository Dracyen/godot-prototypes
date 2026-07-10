using Godot;
using System;
using VoxelBoxUtilities;

public partial class VoxelBoxInputState : InputState
{
    public override void Interact1()
    {
        if(GameActions.PlaceVoxel != null)
            GameActions.PlaceVoxel();
    }

    public override void Interact2()
    {
        if (GameActions.RemoveVoxel != null)
            GameActions.RemoveVoxel();
    }

    public override void Cancel()
    {
        if (GameActions.TogglePauseMenu != null)
            GameActions.TogglePauseMenu();
    }

    public override void OptionTop1()
    {
        if (GameActions.ToggleBlock1 != null)
            GameActions.ToggleBlock1();
    }

    public override void OptionTop2()
    {
        if (GameActions.ToggleBlock2 != null)
            GameActions.ToggleBlock2();
    }

    public override void OptionTop3()
    {
        if (GameActions.ToggleBlock3 != null)
            GameActions.ToggleBlock3();
    }

    public override void OptionTop4()
    {
        if (GameActions.ToggleBlock4 != null)
            GameActions.ToggleBlock4();
    }

    public override void OptionTop5()
    {
        if (GameActions.ToggleBlock5 != null)
            GameActions.ToggleBlock5();
    }
}
