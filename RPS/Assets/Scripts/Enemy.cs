using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Units/Enemy")]
public class Enemy : ScriptableObject
{
    new public string name = "New Enemy";
    public int level;
    public int damage;
    public int hp;
    public Sprite icon = null;
    public int ABehaviourType;
    public int DBehaviourType;
    public int[] ABehaviour;
    public int[] DBehaviour;
}
