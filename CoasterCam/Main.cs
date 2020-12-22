using System.Linq;
using System.Reflection;
using UnityEngine;

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
