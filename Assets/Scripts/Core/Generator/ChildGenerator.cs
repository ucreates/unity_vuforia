using UnityEngine;
using System.Collections;
namespace Core.Generator {
public class ChildGenerator {
    public static GameObject Generate(string name, Vector3 position, Quaternion rotation, GameObject parent, string tag) {
        GameObject child = new GameObject();
        child.transform.parent = parent.transform;
        child.transform.localPosition = Vector3.zero;
        child.tag = tag;
        child.name = name;
        return child;
    }
}
}