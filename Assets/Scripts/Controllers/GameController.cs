using Core.MapDto;
using Core.MapDto.Tiles;
using DefaultNamespace;
using EntityFactoryDto;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public Map Map;

        void Awake()
        {
            ResourceLoader.Initialize();
            
            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();

            Map = MapGenerator.Generate();
            
            gameStateController.AddEntity(EntityFactory.CreateTarget(new EntityCreateOptions
            {
                Damage = 0,
                Health = 1,
                Position = new Vector2(16, 6),
                EntityType = EntityType.Target
            }));
        }

        void FixedUpdate()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }

        private GameObject player;
        private GameStateController gameStateController;

        [SerializeField] private Tilemap block;
    }
}