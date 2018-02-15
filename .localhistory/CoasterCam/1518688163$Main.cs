using UnityEngine;

namespace CoasterCam
{
    public class Main : IMod
    {
        private static GameObject go;

        public void onEnabled()
        {

            KeyGroup group = new KeyGroup(Identifier);
            KeyMapping key = new KeyMapping(Identifier + "/enter", KeyCode.R, KeyCode.None);
            key.keyGroupIdentifier = Identifier;

            InputManager.Instance.registerKeyGroup(group);
            InputManager.Instance.registerKeyMapping(key);

            go = new GameObject();

            go.AddComponent<CoasterCam>();
        }

        public void onDisabled()
        {
            Object.Destroy(go);
        }

        public string Name { get { return "CoasterCam"; } }
        public string Description { get { return "Camera for riding coasters"; } }
        public string Identifier { get { return "H-POPS@CoasterCam"; } }
    }
}
