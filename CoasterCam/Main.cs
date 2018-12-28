using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CoasterCam
{
    public class Main : IMod
    {
        private static GameObject go;

        public Main()
        {
            SetupKeyBinding();
        }

        public void onEnabled()
        {

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


        public string Name => name;
        public string Description => description;
        public string Identifier => identifier;

        private static string name, description, identifier;


        static Main()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var meta =
                assembly.GetCustomAttributes(typeof(AssemblyMetadataAttribute), false)
                .Cast<AssemblyMetadataAttribute>()
                .Single(a => a.Key == "Identifier");
            identifier = meta.Value;

            T GetAssemblyAttribute<T>() where T : System.Attribute => (T) assembly.GetCustomAttribute(typeof(T));

            name = GetAssemblyAttribute<AssemblyTitleAttribute>().Title;
            description = GetAssemblyAttribute<AssemblyDescriptionAttribute>().Description;
        }
    }
}
