using UnityEngine.U2D;
using UnityEngine.UI;

namespace Qw1nt.SpriteAtlasTool
{
    public class ImageAtlasProvider : SpriteAtlasProviderBase<Image>
    {
        protected override void Set(string key, SpriteAtlas atlas)
        {
            Component.sprite = atlas.GetSprite(key);
        }
    }
}