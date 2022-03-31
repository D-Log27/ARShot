using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public class SFX_TargetMarker : SFX_ControlledObject
    {
        public float LifeTime;

        public SFX_FxObject MarkFx;

        public MarkMode MarkTargetMode;

        public int MaxTargets;

        public Material MarkMaterial;
        public float MarkLifeTime;

        private readonly Dictionary<int, GameObject> _markedObjects = new Dictionary<int, GameObject>();
        private readonly List<Vector3> _markedPositions = new List<Vector3>();

        public List<GameObject> MarkedGameObjects
        {
            get { return _markedObjects.Values.ToList(); }
        }

        public List<Vector3> MarkedPositions
        {
            get { return new List<Vector3>(_markedPositions); }
        }


        public override void Run()
        {
            base.Run();

            // TODO : 여기를 고치면 타겟 위치가 변경된다.
            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit))
                return;
 


            if (MarkFx != null)
            {
                SFX_FxObjectInstancer.InstantiateFx(MarkFx, hit.point, hit.normal);
            }
                

            if (MarkTargetMode == MarkMode.GameObject)
            {
                if (_markedObjects.Count >= MaxTargets)
                    return;

                var gameObjectId = hit.transform.gameObject.GetInstanceID();
                if (_markedObjects.ContainsKey(gameObjectId))
                    return;

                _markedObjects[gameObjectId] = hit.transform.gameObject;
                var materialAdded = hit.transform.gameObject.AddComponent<SFX_MaterialAdder>();
                materialAdded.Material = MarkMaterial;
                materialAdded.LifeTime = MarkLifeTime;

                SFX_InvokeUtil.RunLater(this, () =>
                {
                    materialAdded.Stop();
                    Destroy(materialAdded);
                    _markedObjects.Remove(gameObjectId);
                }, LifeTime);
            }
            else
            {
                if (_markedPositions.Count >= MaxTargets)
                    return;
                _markedPositions.Add(hit.point);
                SFX_InvokeUtil.RunLater(this, () => { _markedPositions.Remove(hit.point); }, LifeTime);
            }
        }

        public enum MarkMode
        {
            GameObject = 0,
            Position = 1
        }
    }
}