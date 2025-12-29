using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressablesLoader
{  
    // 이미 로드된 에셋들 캐싱용 딕셔너리
    private static Dictionary<Type, object> _dataCache = new Dictionary<Type, object>();

    /// <summary>
    /// Addressables에서 에셋을 로드하는 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="address"></param>
    /// <param name="onCompleted"></param>
    // public static void LoadAsset<T>(string address, System.Action<T> onCompleted) where T : UnityEngine.Object
    // {
    //     Type type = typeof(T);

    //     if (_loadedAssets.ContainsKey(type){
    //         onCompleted?.Invoke(_loadedAssets[type] as T);
    //         return;
    //     }

    //     handle.Completed += (obj) =>
    //     {
    //         if(obj.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             onCompleted?.Invoke(obj.Result);
    //         }
    //         else
    //         {
    //             Debug.LogError($"Failed to load asset at address: {address}");
    //             onCompleted?.Invoke(null);
    //             Addressables.Release(handle);
    //         }
    //     };
    // }

    /// <summary>
    /// Addressables에서 라벨로 에셋들을 로드하는 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="label"></param>
    /// <param name="onCompleted"></param>
    public static void LoadAssetByLabel<T>(string label, System.Action<List<T>> onCompleted) where T : UnityEngine.Object
    {
        Type type = typeof(T);

        if (_dataCache.ContainsKey(type))
        {
            onCompleted?.Invoke(_dataCache[type] as List<T>);
            return;
        }
        else
        {
            Addressables.LoadAssetsAsync<T>(label, null).Completed += (handle) =>
            {
                if(handle.Status == AsyncOperationStatus.Succeeded)
                {
                    List<T> results = new List<T>(handle.Result);
                    _dataCache[type] = results;
                    onCompleted?.Invoke(results);
                }
                else
                {
                    Debug.LogError($"Failed to load assets with label: {label}");
                    onCompleted?.Invoke(null);
                    Addressables.Release(handle);
                }
            };
        }
    }

    public static void ClearCache<T>()
    {
        Type type = typeof(T);
        if (_dataCache.ContainsKey(type))
        {
            _dataCache.Remove(type);
        }
    }
}   