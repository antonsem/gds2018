using UnityEngine;

public interface IUpdate
{
    void Register();
    void Unregister();

    void FistUpdate();
    void SecondUpdate();
    void ThirdUpdate();

    Transform GetTransform();
}
