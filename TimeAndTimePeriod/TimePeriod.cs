using System;

namespace TimeAndTimePeriod
{
    public readonly struct TimePeriod: IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public long Time { get; }
        public TimePeriod(long seconds)
        {
            Time = seconds >= 0 ? seconds : throw new ArgumentOutOfRangeException();
        }
        public TimePeriod(int hours, int minutes, int seconds = 0)
        {
            if (hours < 0 || minutes < 0 || seconds < 0) throw new ArgumentOutOfRangeException();
            Time = seconds + minutes * 60 + hours * 3600;
        }
        public TimePeriod(string str)
        {
            var data = str.Split(":");
            if (data.Length != 3) throw new ArgumentException("The format 'h:m:s' is required.");

            bool h = int.TryParse(data[0], out var hours);
            bool m = int.TryParse(data[1], out var minutes);
            bool s = int.TryParse(data[2], out var seconds);

            if (!h || !m || !s) throw new ArgumentException("The format 'h:m:s' is required.");

            var result = seconds + minutes * 60 + hours * 3600;
            Time = result >= 0 ? result : throw new ArgumentOutOfRangeException();
        }
        public override string ToString()
        {
            var seconds = Time % 60;
            var minutes = (Time % 3600) / 60;
            var hours = Time / 3600;
            return $"{hours}:{minutes:D2}:{seconds:D2}";
        }

        public override bool Equals(object obj) => obj is TimePeriod timePeriod && Equals(timePeriod);
        public bool Equals(TimePeriod other) => Time == other.Time;
        public override int GetHashCode() => Time.GetHashCode();

        public int CompareTo(TimePeriod other) => Time.CompareTo(other.Time);
        public static bool operator ==(TimePeriod tp1, TimePeriod tp2) => tp1.Equals(tp2);
        public static bool operator !=(TimePeriod tp1, TimePeriod tp2) => !tp1.Equals(tp2);
        public static bool operator <(TimePeriod tp1, TimePeriod tp2) => tp1.CompareTo(tp2) < 0;
        public static bool operator <=(TimePeriod tp1, TimePeriod tp2) => tp1.CompareTo(tp2) <= 0;
        public static bool operator >(TimePeriod tp1, TimePeriod tp2) => tp1.CompareTo(tp2) > 0;
        public static bool operator >=(TimePeriod tp1, TimePeriod tp2) => tp1.CompareTo(tp2) >= 0;

        public static TimePeriod operator +(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(tp1.Time + tp2.Time);
        public TimePeriod Plus(TimePeriod tp2) => this + tp2;
        public static TimePeriod operator -(TimePeriod tp1, TimePeriod tp2)
        {
            var newTimeInSeconds = tp1.Time - tp2.Time;
            return newTimeInSeconds <= 0 ? new TimePeriod() : new TimePeriod(newTimeInSeconds);
        }
        public TimePeriod Minus(TimePeriod tp2) => this - tp2;
    }
}
