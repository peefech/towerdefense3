using System.Linq;
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

            Map = MapGenerator.Generate(block, ground);
            
            gameStateController.AddEntity(EntityFactory.CreateTarget(new EntityCreateOptions
            {
                Damage = 0,
                Health = 1,
                Position = new Vector2(18.5f, 8.5f),
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
        [SerializeField] private Tilemap ground;
    }
}