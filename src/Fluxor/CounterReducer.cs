using Fluxor;

namespace imsblazor.Fluxor;



public static class CounterReducer
{
    [ReducerMethod]
    public static CounterState Reduce(CounterState state, IncrementCounterAction action)
    {
        return state with { ClickCount = state.ClickCount + 1 };
    }
}
