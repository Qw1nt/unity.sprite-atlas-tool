using System;
using System.Linq;
using Qw1nt.SpriteAtlasTool;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Qw1nt.SpriteAtlasTools.Editor
{
    [CustomEditor(typeof(SpriteAtlasProviderBase<>), true)]
    public class SpriteAtlasToolsCustomEditor : UnityEditor.Editor
    {
        private string[] _atlasKeys = Array.Empty<string>();

        private SerializedProperty _key;
        private SerializedProperty _atlas;

        private SpriteAtlas _selectedAtlas;
        private int _selectedKeyIndex;

        private void OnEnable()
        {
            _key = serializedObject.FindProperty("_key");
            _atlas = serializedObject.FindProperty("_atlas");

            UpdateAtlasKeys();
            _selectedKeyIndex = Array.FindIndex(_atlasKeys, key => key == _key.stringValue);
        }

        public override void OnInspectorGUI()
        {
            _selectedKeyIndex = EditorGUILayout.Popup("Key", _selectedKeyIndex, _atlasKeys);

            if (_selectedKeyIndex >= 0)
                _key.stringValue = _atlasKeys[_selectedKeyIndex];

            var oldAtlas = _atlas.objectReferenceValue;
            EditorGUILayout.PropertyField(_atlas, new GUIContent("Atlas"));

            if (oldAtlas != _atlas.objectReferenceValue)
                UpdateAtlasKeys();

            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateAtlasKeys()
        {
            if (_atlas.objectReferenceValue == null)
                return;

            var atlas = (SpriteAtlas) _atlas.objectReferenceValue;
            var sprites = new Sprite[atlas.spriteCount];

            atlas.GetSprites(sprites);
            _atlasKeys = new string[atlas.spriteCount];

            for (int i = 0; i < sprites.Length; i++)
                _atlasKeys[i] = sprites[i].name.Split('(').First();

            var unsortedKeys = _atlasKeys.ToList();
            unsortedKeys.Sort();

            _atlasKeys = unsortedKeys.ToArray();

            if (string.IsNullOrEmpty(_key.stringValue) == true)
                TrySetKeyAuto();
        }

        private void TrySetKeyAuto()
        {
            if (((SpriteAtlasProviderBase<Image>) target).TryGetComponent(out Image image) == false)
                return;

            _selectedKeyIndex = _atlasKeys.ToList().FindIndex(x => x == image.sprite.name);
        }
    }
}