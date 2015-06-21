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
        
        public Dictionary<String, KerbalStore> TrackedKerbals(){
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

            Debug.Log("Iterator is starting to populate.");
            this.PopulateLists();
            Debug.Log("Did it populate?");
            Debug.Log(TrackedKerbals().Count());
            foreach (KerbalStore k in TrackedKerbals().Values.ToList())
            {
                Debug.Log("a kerbal");
                Debug.Log(k.KerbalName);
            }
        }

        public void PopulateLists()
        {
            foreach (Vessel v in FlightGlobals.Vessels)
            {
                foreach (ProtoCrewMember k in v.GetVesselCrew())
                {
                    KerbalStore ks = new KerbalStore();
                    ks.KerbalName = k.name;
                    trackedKerbals.Add(k.name,ks);
                    Debug.Log("k.name " + k.name);
                    Debug.Log("ks.name" + ks.KerbalName);
                    Debug.Log("trackedKerbals[k.name] " + trackedKerbals[k.name]);
                }
            }
        }
    }
}
