using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace peppar
{
    public class PepparController : GameController
    {
        //private Transform _worldCenter;

        //public Transform WorldCenter
        //{
        //    get
        //    {
        //        return _worldCenter;
        //    }
        //}

        protected override void OnStart()
        {
            //_worldCenter = GameObject.FindGameObjectWithTag(Tag.WorldCenter).transform;
            //UnityEngine.Assertions.Assert.IsNotNull(_worldCenter, "Singleton: WorldCenter transform is null");
        }

        protected override void OnUpdate()
        {

        }
    }
}