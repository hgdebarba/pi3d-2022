using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ctsalidis.Scripts {
    public class Utilities {
        public static bool CheckLayer(LayerMask layerMask, LayerMask other) => (layerMask == (layerMask | (1 << other)));
        public static bool CheckLayer(List<LayerMask> LayersToHit, LayerMask other) {
            return LayersToHit.Select(layerMask => CheckLayer(layerMask, other)).FirstOrDefault();
        }

        public static bool CheckLayer(List<LayerMask> LayersToHit, GameObject other) {
            return CheckLayer(LayersToHit, other.layer) ? other : false;
        }

        public static bool CheckLayer(LayerMask layer, GameObject other) {
            return CheckLayer(layer, other.layer) ? other : false;
        }
    }
}