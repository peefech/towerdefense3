using Components;
using UnityEngine;

namespace EntityFactoryDto
{
    public static class EntityFactory
    {
        public static GameObject CreateMonster(EntityCreateOptions options)
        {
            var gameObject = CreateUnit(options);
            gameObject.AddComponent<MonsterComponent>();  

            return gameObject;
        }

        public static GameObject CreateTarget(EntityCreateOptions options)
        {
            var gameObject = CreateUnit(options);
            gameObject.AddComponent<TargetComponent>();  

            return gameObject;
        }
        
        private static GameObject CreateUnit(EntityCreateOptions options)
        {
            var gameObject = Object.Instantiate(GetUnitPrefab(options.EntityType));
            gameObject.transform.position = options.Position;
            
            gameObject.AddComponent<TransformComponent>();

            return gameObject;
        }

        private static GameObject GetUnitPrefab(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.Monster:
                    return ResourceLoader.GetMonsterPrefab();
                case EntityType.Target:
                    return ResourceLoader.GetTargetPrefab();
            }

            return null;
        }
    }
}