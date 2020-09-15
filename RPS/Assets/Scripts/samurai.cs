
using UnityEngine;

public class Pupper : MonoBehaviour {

    Movin pupper;
    string str = "pupper";

    void Start () {

        pupper = new Movin(transform, "json/pupper");
        pupper.Play();
    }
/*
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            str = (str == "pupper") ? "pupper2" : "pupper";
            pupper.Blend("json/" + str, 10f);
        }
    }*/
}
