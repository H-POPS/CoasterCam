using System;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using Parkitect.UI;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace CoasterCam
{
    public class Main : AbstractMod
  {
        private static GameObject go;

        public override string getName() => "CoasterCam";
        public override string getDescription() => "Camera for riding coasters";
        public override string getVersionNumber() => "1.0.1";
        public override string getIdentifier() => "H-POPS@CoasterCam";
        public override bool isMultiplayerModeCompatible() => true;
        public override bool isRequiredByAllPlayersInMultiplayerMode() => false;


        public Main()
        {
            SetupKeyBinding();
        }

        public override void onEnabled()
        {
            go = new GameObject(getIdentifier());
            go.AddComponent<CoasterCam>();

            try
            {
                Debug.Log("starting");
                var assembly = typeof(AttractionInfoWindow).Assembly;
                Debug.Log("assembly" + assembly.FullName);
                var eventsType = assembly.GetType("InternalEventManager");
                Debug.Log("eventsType" + eventsType.Name);
                var instance = eventsType.GetProperty("Instance").GetValue(null);
                Debug.Log("instance" + instance.ToString());
                var eventInfo = eventsType.GetEvent("OnWindowOpened");
                Debug.Log("eventInfo" + eventInfo.ToString());

                // Create the delegate on the test class because that's where the
                // method is. This corresponds with `new EventHandler(test.WriteTrace)`.

                var onMethod = GetType().GetMethod("OnWindowOpened");
                Debug.Log("onMethod" + onMethod.ToString());
                var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, onMethod);
                // Assign the eventhandler. This corresponds with `control.Load += ...`.
                Debug.Log("handler" + handler.ToString());
                eventInfo.AddEventHandler(instance, handler);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void OnWindowOpened(UIWindowFrame windowFrame)
        {
            go.GetComponent<CoasterCam>().OnWindowOpened(windowFrame);
        }
        
        public override void onDisabled()
        {
            Object.Destroy(go);
        }

        private void SetupKeyBinding()
        {
            KeyGroup group = new KeyGroup(getIdentifier());
            KeyMapping key = new KeyMapping(getIdentifier() + "/enter", KeyCode.R, KeyCode.None);

            key.keyGroupIdentifier = getIdentifier();

            group.keyGroupName = "CoasterCam";

            key.keyName = "Enter attraction";
            key.keyDescription = "Use this key on a attraction to enter it";

            InputManager.Instance.registerKeyGroup(group);
            InputManager.Instance.registerKeyMapping(key);
        }
    }
}
