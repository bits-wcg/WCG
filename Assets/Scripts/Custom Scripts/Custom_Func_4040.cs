using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public static class Custom_Func_4040 
{
    public static List<GameObject> GetChildren(Transform parentTransform)
    {
        return (from Transform child in parentTransform select child.gameObject).ToList();
    }

    public static async Task SaveLocally(string fileName, string data)
    {
        Debug.Log($"Writing to Local File {Application.persistentDataPath}");
        await File.WriteAllTextAsync($"/Users/yokesh/Desktop/{fileName}2.json", data);
    }


    
}