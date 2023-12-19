using UnityEngine;
using UnityEngine.U2D;

namespace Qw1nt.SpriteAtlasTool
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererAtlasProvider : SpriteAtlasProviderBase<SpriteRenderer>
    {
        protected override void Set(string key, SpriteAtlas atlas)
        {
            Component.sprite = atlas.GetSprite(key);
        }
    }
}