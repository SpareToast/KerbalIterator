using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace KerbalIterator
{
    public class KerbalSave : ScenarioModule
    {

        public ConfigNode KerbalNode { get; private set; }

        public KerbalSave()
        {
            Instance = this;
        }

        public static KerbalSave Instance { get; private set; }

        public override void OnLoad(ConfigNode gameNode)
        {
            Debug.Log("Loading??? " + KerbalIterator.TrackedKerbals());
            base.OnLoad(gameNode);
            Debug.Log("Loading??? " + KerbalIterator.TrackedKerbals());
            if (gameNode.HasNode("KERBAL_TEST_STORE"))
            {

                Debug.Log("KERBAL TEST STORE FOUND");
                KerbalNode = gameNode.GetNode("KERBAL_TEST_STORE");
                ConfigNode[] kNodes = KerbalNode.GetNodes("ITERATED_KERBAL");
                Dictionary<String, KerbalStore> kDict = new Dictionary<String, KerbalStore>();
                foreach (ConfigNode kNode in kNodes)
                {
                    KerbalStore kerbalStore = ResourceUtilities.LoadNodeProperties<KerbalStore>(kNode);
                    kDict.Add(kerbalStore.KerbalName, kerbalStore);
                }
                KerbalIterator.TrackedKerbals(kDict);
            }
            else
            {
                Debug.Log("KERBAL TEST STORE NOT FOUND");
                
                //trackedKerbals = new Dictionary<String, KerbalStore>();
            }
        }

        public override void OnSave(ConfigNode gameNode)
        {
            Debug.Log("Saving??? " + KerbalIterator.TrackedKerbals());
            base.OnSave(gameNode);
            Debug.Log("Saving??? " + KerbalIterator.TrackedKerbals());

            if (gameNode.HasNode("KERBAL_TEST_STORE"))
            {
                Debug.Log("KERBAL TEST STORE FOUND (SAVE)");
                
                KerbalNode = gameNode.GetNode("KERBAL_TEST_STORE");
            }
            else
            {
                Debug.Log("KERBAL TEST STORE NOT FOUND (SAVE)");
                
                KerbalNode = gameNode.AddNode("KERBAL_TEST_STORE");
            }

            foreach (KeyValuePair<string, KerbalStore> k in KerbalIterator.TrackedKerbals())
            {

                Debug.Log(k.Value.KerbalName);
                ConfigNode kNode = new ConfigNode("ITERATED_KERBAL");
                kNode.AddValue("KerbalName", k.Value.KerbalName);
                kNode.AddValue("VesselName", k.Value.VesselName);
                kNode.AddValue("LoadNumber", k.Value.LoadNumber+1);
                KerbalNode.AddNode(kNode);
            }
        }
    }
}