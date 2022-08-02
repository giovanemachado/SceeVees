using System;
using UnityEngine;

public class FlowManager : Singleton<FlowManager>
{
    BaseGameState state;

    [HideInInspector] public MenuState MenuState = new MenuState();
    [HideInInspector] public PlayState PlayState = new PlayState();
    [HideInInspector] public EndgameState EndgameState = new EndgameState();

    public BaseGameState currentState;

    public static event Action<BaseGameState> OnGameStateChange;

    void Start()
    {
        InitStateAndInvokeEvent(MenuState);
    }

    void Update()
    {
        state.UpdateState(this);
    }

    public void SwitchState(BaseGameState newState)
    {
        state.ExitState(this);

        InitStateAndInvokeEvent(newState);
    }

    void InitStateAndInvokeEvent(BaseGameState initState)
    {
        state = initState;
        OnGameStateChange?.Invoke(state);
        currentState = this.state;
        state.EnterState(this);
    }
}
