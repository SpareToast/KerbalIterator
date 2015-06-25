using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KerbalIterator
{
    public class KerbalGui : MonoBehaviour
    {
        private Rect _windowPosition = new Rect();
                public ConfigNode SettingsNode { get; private set; }
        public void OnDraw()
        {
            _windowPosition = GUILayout.Window(10, _windowPosition, OnWindow, "All Kerbals");
        }

        public void OnWindow(int windowID)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(250f));
            GUILayout.Label("test");
            GUILayout.EndHorizontal();
            if (KerbalIterator.TrackedKerbals().Count > 0)
            {
                foreach (KeyValuePair<string, KerbalStore> k in KerbalIterator.TrackedKerbals())
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(k.Value.LoadNumber.ToString());
                    GUILayout.Label(k.Value.KerbalName);
                    GUILayout.Label(k.Value.VesselName);
                    GUILayout.EndHorizontal();
                }
            }
            GUI.DragWindow();
        }
    }
}
