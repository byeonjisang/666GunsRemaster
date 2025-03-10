using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<string> valuesJson = new List<string>(); // JSON으로 저장

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        valuesJson.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            valuesJson.Add(JsonUtility.ToJson(new Wrapper<TValue>(pair.Value))); // JSON으로 저장
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != valuesJson.Count)
            throw new Exception($"Mismatch between keys ({keys.Count}) and values ({valuesJson.Count}) after deserialization.");

        for (int i = 0; i < keys.Count; i++)
        {
            TValue value = JsonUtility.FromJson<Wrapper<TValue>>(valuesJson[i]).Value; // JSON에서 복원
            this.Add(keys[i], value);
        }
    }
}

// 🔹 TValue를 감싸서 JSON으로 저장 가능하게 하는 클래스
[Serializable]
public class Wrapper<T>
{
    public T Value;

    public Wrapper(T value)
    {
        Value = value;
    }
}
