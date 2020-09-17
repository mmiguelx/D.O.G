using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "Animations/Cutscenes")]
public class Cutscene : ScriptableObject
{
    new public string name = "New Cutscene";
    public string[] texts;
    public float[] textDelays;
    public int[] arep;
    public AudioClip[] audioClips;
    public int nextScene;
}
