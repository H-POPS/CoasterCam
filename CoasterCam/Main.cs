using UnityEngine;

namespace CoasterCam
{
    public class Main : IMod
    {
        private static GameObject go;

        public void onEnabled()
        {
            SetupKeyBinding();

            go = new GameObject(Identifier);

            go.AddComponent<CoasterCam>();
        }

        public void onDisabled()
        {
            Object.Destroy(go);
        }

        private void SetupKeyBinding()
        {
            KeyGroup group = new KeyGroup(Identifier);
            KeyMapping key = new KeyMapping(Identifier + "/enter", KeyCode.R, KeyCode.None);

            key.keyGroupIdentifier = Identifier;

            group.keyGroupName = "CoasterCam";

            key.keyName = "Enter attraction";
            key.keyDescription = "Use this key on a attraction to enter it";

            InputManager.Instance.registerKeyGroup(group);
            InputManager.Instance.registerKeyMapping(key);
        }

        public string Name { get { return "CoasterCam"; } }
        public string Description { get { return "Camera for riding coasters"; } }
        public string Identifier { get { return "H-POPS@CoasterCam"; } }
    }
}
