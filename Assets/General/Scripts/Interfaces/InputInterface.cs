using Godot;
using System;

public partial interface InputInterface
{
    void Interact1();

    void Interact2();

    void Move(Vector2 force);

    void Cancel();

    void OptionTop1();

    void OptionTop2();

    void OptionTop3();

    void OptionTop4();

    void OptionTop5();
}
