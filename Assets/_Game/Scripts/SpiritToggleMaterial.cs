using DeadTired.Interactables;
using UnityEngine;

namespace DeadTired
{
    public class SpiritToggleMaterial : MonoBehaviour
    {
        public Material toSwitchTo;
        private Material previousMaterial;
        private Renderer renderer;
        private SpiritPoweredLever spiritPoweredLever;


        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            spiritPoweredLever = transform.root.GetComponentInChildren<SpiritPoweredLever>();
        }


        private void OnEnable() => spiritPoweredLever.OnSpiritAdded += ChangeToSwitchMaterial;
        private void OnDisable() => spiritPoweredLever.OnSpiritAdded -= ChangeToSwitchMaterial;


        public void ChangeToSwitchMaterial()
        {
            previousMaterial = renderer.material;
            renderer.material = toSwitchTo;
        }
        
        
        public void ChangeToDefaultMaterial()
        {
            renderer.material = previousMaterial;
        }
    }
}