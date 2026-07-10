namespace UkiDukiRPG.Core.Domain.Time;

public interface IScheduler
{
    void Schedule(Action action, TimeInterval interval);

    void Schedule(Action action, int interval, TimeUnit delayUnit);
}

public class TimeSystem : IScheduler
{
    private readonly PriorityQueue<Action, int> m_Queue = new();

    private int m_CurrentTick = 0;
    
    public void AdvanceTick()
    {
        ++m_CurrentTick;

        while (m_Queue.TryPeek(out var action, out var executionTick))
        {
            if (executionTick > m_CurrentTick)
                break;

            m_Queue.Dequeue();

            action();
        }
    }

    public void Schedule(Action action, TimeInterval interval) => m_Queue.Enqueue(action, m_CurrentTick + interval.Ticks);

    // @formatter:off
    public void Schedule(Action action, int interval, TimeUnit delayUnit) => m_Queue.Enqueue(action, 
                                                                                             m_CurrentTick + TimeInterval.From(interval, delayUnit).Ticks);
    // @formatter:on
}
