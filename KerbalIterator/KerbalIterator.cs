using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace KerbalIterator
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KerbalIterator : MonoBehaviour
    {

        private static Dictionary<String,KerbalStore> trackedKerbals = new Dictionary<String,KerbalStore>();
        
        public static Dictionary<String, KerbalStore> TrackedKerbals(){
            return trackedKerbals;
        }
        public KerbalIterator()
        {
            Debug.Log("Iterator is constructed");
        }

        void Awake()
        {
            Debug.Log("Iterator is awake");
        }

        void Start()
        {
            Debug.Log("Iterator is starting");
            trackedKerbals = new Dictionary<String, KerbalStore>();
            this.PopulateLists();
            Debug.Log(TrackedKerbals().Count());
            KerbalGui kerbalGui = new KerbalGui();
            RenderingManager.AddToPostDrawQueue(0, kerbalGui.OnDraw);
        }

        public void PopulateLists()
        {
            foreach (Vessel v in FlightGlobals.Vessels)
            {
                foreach (ProtoCrewMember k in v.GetVesselCrew())
                {
                    KerbalStore ks = new KerbalStore();
                    ks.KerbalName = k.name;
                    ks.VesselName = v.name;
                    trackedKerbals.Add(k.name,ks);
                }
            }
        }
    }
}
