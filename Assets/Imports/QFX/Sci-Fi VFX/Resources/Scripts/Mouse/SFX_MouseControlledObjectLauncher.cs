using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public class SFX_MouseControlledObjectLauncher : MonoBehaviour
    {
        public SFX_ControlledObject[] ControlledObjects;
        public int MouseButtonCode;
        public bool CallStop = true;
        public bool isShotGun { get; set; }

        public void SkillAction()
        {
            if (isShotGun)
            {
                foreach (var controlledObject in ControlledObjects)
                {
                    controlledObject.Setup();
                    controlledObject.Run();
                    //controlledObject.Stop();
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButtonCode) && isShotGun)
            {
                foreach (var controlledObject in ControlledObjects)
                {
                    controlledObject.Setup();
                    controlledObject.Run();
                    controlledObject.Stop();
                }
            }
        }
    }
}