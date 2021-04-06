using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidVolumeFX {

    public class FloatingObjectSimple : MonoBehaviour {

        public LiquidVolume liquidVolume;

        void Update() {
            if(liquidVolume.level >=0.2f)
            {
                liquidVolume.MoveToLiquidSurface(transform, BuoyancyEffect.PositionOnly);
            }
           
        }

    }
}
