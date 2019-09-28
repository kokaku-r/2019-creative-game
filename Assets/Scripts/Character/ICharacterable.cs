using UnityEngine;
using UnityEngine.AI;

public interface ICharacterable
{
    void Initialize();

    void OnMove();

    void OnDestruction(GameObject destroyer);
}
