using UnityEngine;

[ExecuteInEditMode]
public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;
	
	void Update ()
    {
        Mat.SetVector("_LightPosition",  SpotLight.transform.position);
        Mat.SetVector("_LightDir", -SpotLight.transform.forward);
        Mat.SetFloat ("_LightAngle", SpotLight.spotAngle);
    }
}