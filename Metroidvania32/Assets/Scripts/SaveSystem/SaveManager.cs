using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    
    private string savePath => Path.Combine(Application.persistentDataPath, "savefile.json");

    public void SaveGame(SaveData state)
    {
        string json = JsonUtility.ToJson(state);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
    }

    public SaveData LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found at: " + savePath);
            return null;
        }

        string json = File.ReadAllText(savePath);
        SaveData state = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("Game loaded from: " + savePath);
        return state;
    }

    public bool SaveExists()
    {
        return File.Exists(savePath);
    }

    private string GetSavePath(int slot)
    {
        return Path.Combine(
            Application.persistentDataPath,
            $"save_slot_{slot}.json");
    }

}
