
namespace ResourceInformation
{
    namespace Effect
    {
        static class Path
        {
            public const string COMMON_EFFECT = "Prefab/CommonEffect/";
        }

        public enum CommonEffec
        {
            Hit,

            MAX
        }
    }

    namespace Object
    {
        static class Path
        {
            public const string COMMON_OBJECT = "Prefab/CommonObject/";
        }

        public enum CommonObject
        {
            Bin,
            AttackBox,

            MAX
        }
    }

    namespace Character
    {
        static class Path
        {
            public const string CONTROLLABLE_CHARACTER = "Prefab/Character/Controllable";
        }

        public enum ControllableCharacter
        {
            UnityChan,
            DarkUnityChan,

            MAX
        }
    }

    namespace Skill
    {
        // https://www.c-sharpcorner.com/UploadFile/74ce7b/static-class-in-C-Sharp/
        class Path
        {
            // https://answers.unity.com/questions/306751/get-script-path.html
            // https://docs.unity3d.com/ScriptReference/Application-dataPath.html
            public static string Skill = UnityEngine.Application.dataPath + "/Script/Component/Skill";
        }
    }
}
