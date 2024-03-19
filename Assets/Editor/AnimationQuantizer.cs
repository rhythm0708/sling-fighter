using UnityEditor;
using UnityEngine;
 
// This importer will allow animations to import with a
// stepped style. 
// Source: https://forum.unity.com/threads/quantized-animations-from-blender-fbx-not-importing-correctly.1405234/
public class AnimationQuantizer : AssetPostprocessor
{
    /// <param name="root"></param>
    /// <param name="clip"></param>
    private void OnPostprocessAnimation(GameObject root, AnimationClip clip)
    {
        if(AnimationQuantizerSettings.Enabled)
        {
            var curveBindings = AnimationUtility.GetCurveBindings(clip);
            foreach(var curveBinding in curveBindings)
            {
                var curve = AnimationUtility.GetEditorCurve(clip, curveBinding);
                for(int i = 0; i < curve.keys.Length; i++)
                {
                    curve.keys[i].inWeight = 0;
                    curve.keys[i].outWeight = 0;
                    curve.keys[i].inTangent = 0;
                    curve.keys[i].outTangent = 0;
                    curve.keys[i].weightedMode = WeightedMode.None;
                    AnimationUtility.SetKeyLeftTangentMode(curve, i, AnimationUtility.TangentMode.Constant);
                    AnimationUtility.SetKeyRightTangentMode(curve, i, AnimationUtility.TangentMode.Constant);
                }
                AnimationUtility.SetEditorCurve(clip, curveBinding, curve);
            }
        }
    }
}

public static class AnimationQuantizerSettings
{
    public static bool Enabled = true;
}