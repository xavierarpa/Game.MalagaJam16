using System;
using System.Collections;
using Kingdox.UniFlux;
using UnityEngine.SceneManagement;
using UnityEngine;

public sealed class Bootstrap : MonoFlux
{
    
    private static Bootstrap _;
    private void Awake()
    {
        if (SceneManager.sceneCount!=1)
        {
            Reset();
        }
    }

    private IEnumerator Start()
    {
        //FIRST
        yield return SceneManager.LoadSceneAsync(SceneData.Scene, LoadSceneMode.Additive);

        // ESENTIALS
        yield return Service.AddScene(SceneData.Updates);
        yield return Service.AddScene(SceneData.Click);
        yield return Service.AddScene(SceneData.Binary);
        yield return Service.AddScene(SceneData.EventSystem);
        yield return Service.AddScene(SceneData.System_Audio); 
        yield return Service.AddScene(SceneData.Fader); 

        // EXPERIMENTAL
        yield return Service.AddScene(SceneData.Intro); 
        yield return Service.AddScene(SceneData.Dia_n); 
        yield return Service.AddScene(SceneData.ChooseScene); 
        yield return Service.AddScene(SceneData.Map); 
        yield return Service.AddScene(SceneData.End); 


        // INIT GAME
        Service.SetBinary("_debug_", Service.GetBinary("_debug_", 0) + 1);
        "DayN".DispatchState(3);

        Service.PlayMusic(MusicEnum.Intro);
        "Intro.Display".Dispatch(true);
        "Intro.Start".Dispatch();
        
        // END
        yield return new WaitForSeconds(1);
        yield return Service.RemoveScene(SceneData.Bootstrap); 
    }

    [Flux("Reset")] private void Reset()
    {
        SceneManager.LoadScene(SceneData.Bootstrap);       
    }
}