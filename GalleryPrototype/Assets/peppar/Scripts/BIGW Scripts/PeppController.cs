using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace peppar
{
    public class PeppController : BehaviourController
    {
        [Serializable]
        public class Pepp
        {
            public string PeppID = "New Pepp";

            public PeppComponent PeppComponent;

            public GameObject PeppObject;

            public GameObject BuildingObject;
        }

        [SerializeField]
        private List<Pepp> _pepps = new List<Pepp>();

        public void SetPeppsActivation(bool active, params string[] peppIds)
        {
            foreach (var pepp in _pepps)
            {
                if (peppIds.Contains(pepp.PeppID))
                {
                    //pepp.PeppObject.SetActive(active); Falsch
                }
            }
        }

        public void SetPeppBuildingHighlighting(int highlightValue, params string[] peppIds)
        {
            foreach (var pepp in _pepps)
            {
                if (peppIds.Contains(pepp.PeppID))
                {
                    foreach(var buildingChildMeshRenderer in pepp.BuildingObject.GetComponentsInChildren<MeshRenderer>())
                    {
                        buildingChildMeshRenderer.material.SetInt("_EnablePan", highlightValue);
                    }
                }
            }
        }

        protected override void Awake()
        {

        }

        protected override void Start()
        {

        }

        protected override void Update()
        {

        }
    }
}