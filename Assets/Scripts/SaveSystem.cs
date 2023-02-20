using UnityEngine;

public static class SaveSystem
{
    public static float TryLoad(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetFloat(key);
        return 0;
    }

    public static void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
}
