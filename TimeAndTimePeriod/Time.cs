using System;

namespace TimeAndTimePeriod
{
    public readonly struct Time: IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }
        private static int Verify(int value, int min, int max) => value >= min && value < max ? value : throw new ArgumentOutOfRangeException();

        public Time(int hours = 0, int minutes = 0, int seconds = 0)
        {
            Hours = (byte)Verify(hours, 0, 24);
            Minutes = (byte)Verify(minutes, 0, 60);
            Seconds = (byte)Verify(seconds, 0, 60);
        }

        public Time(string str)
        {
            var data = str.Split(":");
            if (data.Length != 3) throw new ArgumentException("The format 'hh:mm:ss' is required.");

            bool h = int.TryParse(data[0], out var hours);
            bool m = int.TryParse(data[1], out var minutes);
            bool s = int.TryParse(data[2], out var seconds);

            if (!h || !m || !s) throw new ArgumentException("The format 'hh:mm:ss' is required.");

            Hours = (byte)Verify(hours, 0, 24);
            Minutes = (byte)Verify(minutes, 0, 60);
            Seconds = (byte)Verify(seconds, 0, 60);
        }
        public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        public override bool Equals(object obj) => obj is Time time && Equals(time);
        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        public override int GetHashCode() => HashCode.Combine(Hours, Minutes, Seconds);
        public int CompareTo(Time other)
        {
            if (Hours.CompareTo(other.Hours) != 0) return Hours.CompareTo(other.Hours);
            if (Minutes.CompareTo(other.Minutes) != 0) return Minutes.CompareTo(other.Minutes);
            if (Seconds.CompareTo(other.Seconds) != 0) return Seconds.CompareTo(other.Seconds);
            return 0;
        }
        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !t1.Equals(t2);
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;

        public static Time operator +(Time time, TimePeriod timePeriod)
        {
            var timeInSeconds = time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
            var newTimeInSeconds = (timeInSeconds + timePeriod.Time) % (3600 * 24); // % 24h
            var obj = new TimePeriod(newTimeInSeconds);
            return new Time(obj.ToString()); // using TimePeriod.ToString method to get time in good format - h:mm:ss
        }
        public Time Plus(TimePeriod timePeriod) => this + timePeriod;
        public static Time operator -(Time time, TimePeriod timePeriod)
        {
            var timeInSeconds = time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
            var newTimeInSeconds = timeInSeconds - timePeriod.Time;
            if (newTimeInSeconds < 0) newTimeInSeconds = 3600 * 24 - Math.Abs(newTimeInSeconds) % (3600 * 24);

            var obj = new TimePeriod(newTimeInSeconds);
            return new Time(obj.ToString());
        }
        public Time Minus(TimePeriod timePeriod) => this - timePeriod;
    }
}
