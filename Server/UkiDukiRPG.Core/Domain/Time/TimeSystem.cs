namespace UkiDukiRPG.Core.Domain.Time;

public interface ITimeSystem
{
    public int          CurrentTick { get; }
    public TimeInterval CurrentTime { get; }

    public void AdvanceTick();

    void Schedule(Action action, TimeInterval interval);

    void Schedule(Action action, int interval, TimeUnit delayUnit);
}

public class TimeSystem : ITimeSystem
{
    private readonly PriorityQueue<Action, int> m_Queue = new();

    public int CurrentTick { get; private set; } = 0;

    public TimeInterval CurrentTime => TimeInterval.FromTicks(CurrentTick);

    public void AdvanceTick()
    {
        ++CurrentTick;

        while (m_Queue.TryPeek(out var action, out var executionTick))
        {
            if (executionTick > CurrentTick)
                break;

            m_Queue.Dequeue();

            action();
        }
    }

    public void Schedule(Action action, TimeInterval interval) => m_Queue.Enqueue(action, CurrentTick + interval.Tick);

    // @formatter:off
    public void Schedule(Action action, int interval, TimeUnit delayUnit) => m_Queue.Enqueue(action, 
                                                                                             CurrentTick + TimeInterval.From(interval, delayUnit).Tick);
    // @formatter:on
}
