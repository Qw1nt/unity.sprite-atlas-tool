using UnityEngine;
using UnityEngine.U2D;

namespace Qw1nt.SpriteAtlasTool
{
    public abstract class SpriteAtlasProviderBase<T> : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private SpriteAtlas _atlas;

        protected T Component { get; private set; }

        private void Awake()
        {
            Component = GetComponent<T>();
            Set(_key, _atlas);
        }

        protected abstract void Set(string key, SpriteAtlas atlas);
    }
}