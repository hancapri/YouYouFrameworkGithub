using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;
using YouYouFramework;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//using Tetris;

public static class MyGenConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
                typeof(System.Object),
                typeof(UnityEngine.Object),
                typeof(Vector2),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Ray),
                typeof(Bounds),
                typeof(Ray2D),
                typeof(Time),
                typeof(GameObject),
                typeof(Component),
                typeof(Behaviour),
                typeof(Transform),
                typeof(Resources),
                typeof(TextAsset),
                typeof(Keyframe),
                typeof(AnimationCurve),
                typeof(AnimationClip),
                typeof(MonoBehaviour),
                typeof(ParticleSystem),
                typeof(SkinnedMeshRenderer),
                typeof(Renderer),
                typeof(WWW),
                typeof(Light),
                typeof(Mathf),
                typeof(System.Collections.Generic.List<int>),
                typeof(Action<string>),
                typeof(UnityEngine.Debug),
                typeof(MeshRenderer),
                //typeof(PromptStruct),
                typeof(System.TimeSpan)
            };

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(UnityEngine.Events.UnityAction<bool>),
                typeof(UnityEngine.Events.UnityAction<System.Boolean>),
                typeof(UnityEngine.Events.UnityAction<float>),
                typeof(UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>),
                typeof(System.Collections.IEnumerator),
                typeof(DG.Tweening.Core.DOGetter<int>),
                typeof(DG.Tweening.Core.DOSetter<int>),
                typeof(DG.Tweening.Core.DOGetter<float>),
                typeof(DG.Tweening.Core.DOSetter<float>),
                typeof(DG.Tweening.Core.DOGetter<Vector3>),
                typeof(DG.Tweening.Core.DOSetter<Vector3>),
                //typeof(GameFrameworkAction<object>),
                typeof(UnityEvent<Vector2>),
                typeof(UnityAction<Vector2>),
                //typeof(BaseAction<CommonEntity>),
                typeof(BaseAction<MMO_MemoryStream>)
            };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"UnityEngine.WWW", "movie"},
    #if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
    #endif
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},

    #if UNITY_2017_1_OR_NEWER
                new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
                new List<string>(){"UnityEngine.WWW", "MovieTexture"},
                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
                new List<string>(){"UnityEngine.Light", "SetLightDirty"},
                new List<string>(){"UnityEngine.Light", "shadowRadius"},
                new List<string>(){"UnityEngine.Light", "shadowAngle"},
                new List<string>(){"UnityEngine.ParticleSystem", "SetParticles", "UnityEngine.ParticleSystem+Particle[]"},
                new List<string>(){"UnityEngine.ParticleSystem", "SetParticles", "UnityEngine.ParticleSystem+Particle[]", "System.Int32"},
                new List<string>(){"UnityEngine.ParticleSystem", "SetParticles", "UnityEngine.ParticleSystem+Particle[]", "System.Int32", "System.Int32"},
                new List<string>(){"UnityEngine.ParticleSystem", "GetParticles", "UnityEngine.ParticleSystem+Particle[]"},
                new List<string>(){"UnityEngine.ParticleSystem", "GetParticles", "UnityEngine.ParticleSystem+Particle[]", "System.Int32"},
                new List<string>(){"UnityEngine.ParticleSystem", "GetParticles", "UnityEngine.ParticleSystem+Particle[]", "System.Int32", "System.Int32"},
    #endif
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
                new List<string>(){ "TurnBook", "BookPanel"},
                new List<string>(){ "TurnBook", "ClippingPlane"},
                new List<string>(){ "TurnBook", "Shadow"},
                //new List<string>(){ "TurnBook", "ShadowLTR"},
                //new List<string>(){ "TurnBook", "LeftPageShadow"},
                new List<string>(){ "TurnBook", "RightPageShadow"},
                new List<string>(){ "TurnBook", "LeftPageTransform"},
                new List<string>(){ "TurnBook", "RightPageTransform"},
                new List<string>(){ "TurnBook", "EndBottomLeft"},
                new List<string>(){ "TurnBook", "EndBottomMiddle"},
                new List<string>(){ "TurnBook", "EndBottomRight"},
                new List<string>(){ "TurnBook", "StartFlippingPaper"},
                new List<string>(){ "TurnBook", "EndFlippingPaper"},
                new List<string>(){ "TurnBook", "enableShadowEffect"},
                new List<string>(){ "TurnBook", "UpdateBookRTLToPoint", "UnityEngine.Vector3"},
                new List<string>(){ "TurnBook", "UpdateBookLTRToPoint", "UnityEngine.Vector3"},
                new List<string>(){ "TurnBook", "DragRightPageToPoint", "UnityEngine.Vector3"},
                new List<string>(){ "TurnBook", "DragLeftPageToPoint", "UnityEngine.Vector3"},
                new List<string>(){ "TurnBook", "Flip"},
                new List<string>(){ "TurnBook", "papers"},
                new List<string>(){ "AutoFlip", "ControledBook"},
                new List<string>(){ "AutoFlip", "Mode"},
    #if !UNITY_5_6_OR_NEWER

    #endif
            };
}