using Fluxor;

namespace imsblazor.Fluxor;


public record CounterState(int ClickCount);

public class CounterFeature : Feature<CounterState>
{
    public override string GetName() => "Counter";
    protected override CounterState GetInitialState() => new(0);
}
