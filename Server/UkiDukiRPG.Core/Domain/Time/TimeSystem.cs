namespace UkiDukiRPG.Core.Domain.Time;

public interface IScheduler
{
    int CurrentTick { get; }

    void Schedule(Action action, TimeInterval interval);

    void Schedule(Action action, int interval, TimeUnit delayUnit);
}

public class TimeSystem : IScheduler
{
    private readonly PriorityQueue<Action, int> m_Queue = new();

    public int CurrentTick { get; private set; } = 0;

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

    public void Schedule(Action action, TimeInterval interval) => m_Queue.Enqueue(action, CurrentTick + interval.Ticks);

    // @formatter:off
    public void Schedule(Action action, int interval, TimeUnit delayUnit) => m_Queue.Enqueue(action, 
                                                                                             CurrentTick + TimeInterval.From(interval, delayUnit).Ticks);
    // @formatter:on
}
