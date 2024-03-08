using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost", menuName = "ScriptableObjects/Ghost_SO")]

public class Ghost_SO : ScriptableObject
{
    public List<Vector3> positions;
    public List<Quaternion> rotations;

    public bool canRecord;
    public bool canRace;
}
