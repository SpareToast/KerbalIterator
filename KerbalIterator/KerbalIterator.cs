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

        public static Dictionary<String, KerbalStore> TrackedKerbals()
        {
            return trackedKerbals;
        }

        public static void TrackedKerbals(Dictionary<String, KerbalStore> newKerbals)
        {
            trackedKerbals = newKerbals;
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

            Game game = HighLogic.CurrentGame;

            if (HighLogic.LoadedScene.ToString() == "SPACECENTER")
            {
                Debug.Log(HighLogic.LoadedScene.ToString());
                ProtoScenarioModule psm = game.scenarios.Find(s => s.moduleName == typeof(KerbalSave).Name);
                if (psm == null)
                {
                    game.AddProtoScenarioModule(typeof(KerbalSave), GameScenes.SPACECENTER,
                        GameScenes.FLIGHT, GameScenes.EDITOR);
                }
                else
                {
                    if (psm.targetScenes.All(s => s != GameScenes.SPACECENTER))
                    {
                        psm.targetScenes.Add(GameScenes.SPACECENTER);
                    }
                    if (psm.targetScenes.All(s => s != GameScenes.FLIGHT))
                    {
                        psm.targetScenes.Add(GameScenes.FLIGHT);
                    }
                    if (psm.targetScenes.All(s => s != GameScenes.EDITOR))
                    {
                        psm.targetScenes.Add(GameScenes.EDITOR);
                    }
                }
            }

            Debug.Log("Iteration is starting");
            if (trackedKerbals.Count == 0)
            {
                Debug.Log("Iterating");
                trackedKerbals = new Dictionary<String, KerbalStore>();
                this.PopulateLists();
            }
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
                    ks.LoadNumber = 0;
                    trackedKerbals.Add(k.name,ks);

                }
            }
        }
    }
}
