using UnityEngine;

public enum SceneType
{
    MainMenu,
    Gameplay,
    Cutscene
}

public class SceneContext : MonoBehaviour
{
    public SceneType sceneType;
}