using LarkFramework.Module;
using Project;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace LarkFramework.Scenes
{
    public class ScenesManager : ServiceModule<ScenesManager>
    {
        public const string LOG_TAG = "ScenesManager";

        public ScenesComponent scenesComponent;

        public static string MainScene = SceneDef.HomeScene;   //主场景

        class SceneTrack
        {
            public string name; //场景名
            public bool isAdditive;  //加载方式
        }

        private Stack<SceneTrack> m_SceneStack;    //场景堆栈
        private SceneTrack m_currentScene;  //当前场景名
        private List<SceneTrack> m_listLoadedScene;    //所有加载过的场景

        public ScenesManager()
        {
            m_SceneStack = new Stack<SceneTrack>();
            m_listLoadedScene = new List<SceneTrack>();
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="uiResRoot">场景资源的根目录，默认为"Scene/"</param>
        public void Init(string sceneResRoot = "Scene/")
        {
            CheckSingleton();
            ScenesRes.SceneResRoot = sceneResRoot;
            scenesComponent = ScenesComponent.FindScenesComponent();
        }

        #region Scene管理
        /// <summary>
        /// 进入Scene
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="isAdditive"></param>
        /// <param name="arg"></param>
        public void LoadScene(string scene, bool isAdditive=false, object arg = null)
        {
            Debuger.Log(LOG_TAG, "LoadScene() scene:{0}, isAdditive:{1}, arg:{2}", scene, isAdditive, arg);

            m_currentScene = new SceneTrack();
            m_currentScene.name = scene;
            m_currentScene.isAdditive = isAdditive;

            m_listLoadedScene.Add(m_currentScene);

            //加载场景
            ScenesRes.LoadScene(scene, isAdditive);
        }

        /// <summary>
        /// 进入Scene
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="isAdditive"></param>
        /// <param name="arg"></param>
        public void LoadSceneAsync(string scene, bool isAdditive = false, Action action = null, object arg = null)
        {
            Debuger.Log(LOG_TAG, "LoadSceneAsync() scene:{0}, isAdditive:{1}, arg:{2}", scene, isAdditive, arg);

            m_currentScene = new SceneTrack();
            m_currentScene.name = scene;
            m_currentScene.isAdditive = isAdditive;

            m_listLoadedScene.Add(m_currentScene);

            //加载场景
            //TODO:加载成功失败的回调没有实现
            scenesComponent.LoadSceneAnysc(scene, isAdditive,action);
        }


        //-------------------------------------------------------

        /// <summary>
        /// 进入主场景
        /// 会清空场景堆栈
        /// </summary>
        public void EnterMainScene()
        {
            m_SceneStack.Clear();
            LoadScene(MainScene,false, null);
        }

        //--------------------------------------------------------

        /// <summary>
        /// 返回上一场景
        /// </summary>
        public void GoBackScene()
        {
            Debuger.Log(LOG_TAG, "GoBackScene()");
            if (m_SceneStack.Count > 0)
            {
                var track = m_SceneStack.Pop();
                LoadScene(track.name,track.isAdditive,null);
            }
            else if (m_SceneStack.Count == 0)
            {
                EnterMainScene();
            }
        }
        #endregion
    }
}
