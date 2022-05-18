using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TimeAndTimePeriod;

namespace TimeAndTimePeriod_UnitTests
{
    [TestClass]
    public class TimeUnitTests
    {
        public static IEnumerable<object[]> DataSetInvalid3ParamsArgOutOfRangeEx => new List<object[]> {
            new object[] {100},
            new object[] {0, 0, 61},
            new object[] {-125, 0},
            new object[] {27, 45},
            new object[] {6, -49, 69},
            new object[] {6, 89, 69},
            new object[] {12, 0, -1},
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetInvalid3ParamsArgOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_Invalid_3_params_ArgumentOutOfRangeException(int a, int b = 0, int c = 0)
        {
            var t = new Time(a, b, c);
        }

        public static IEnumerable<object[]> DataSetInvalidStringArgumentException => new List<object[]> {
            new object[] {""},
            new object[] {"12|12|12"},
            new object[] {"bike"},
            new object[] {"b:i:k:3"},
            new object[] {"b:i:k"},
            new object[] {"100:200"},
            new object[] {"10:20,30"},
            new object[] {"9:8:7:6"},
            new object[] {"6:7:8."},
            new object[] {"11:222"},
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetInvalidStringArgumentException))]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Invalid_String_ArgumentException(string a)
        {
            var t = new Time(a);
        }
        public static IEnumerable<object[]> DataSetInvalidStringParamsArgOutOfRangeEx => new List<object[]> {
            new object[] {"24:59:59"},
            new object[] {"12:12:122"},
            new object[] {"20:-20:20"},
            new object[] {"0:0:60"},
            new object[] {"12:-12:-49"},
            new object[] {"-14:0:10"},
            new object[] {"22:100000:12"},
            new object[] {"1:1:111"},
            new object[] {"4:-4:-4"},
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetInvalidStringParamsArgOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_Invalid_String_Params_ArgumentOutOfRangeEx(string a)
        {
            var t = new Time(a);
        }

        public static IEnumerable<object[]> DataSetValidString => new List<object[]> {
            new object[] {"0:0:0", 0, 0, 0 },
            new object[] {"12:0:0", 12, 0, 0 },
            new object[] {"23:59:59", 23, 59, 59 },
            new object[] {"1:1:1", 1, 1, 1 },
            new object[] {"0:4:15", 0, 4, 15 },
            new object[] {"10:15:20", 10, 15, 20 },
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValidString))]
        public void Constructor_Valid_String(string a,
            int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            var t = new Time(a);
            Assert.AreEqual(expectedHour, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }

        public static IEnumerable<object[]> DataSetValid3Params => new List<object[]> {
            new object[] { 0, 0, 0, 0, 0, 0 },
            new object[] { 12, 0, 0, 12, 0, 0 },
            new object[] { 23, 59, 59, 23, 59, 59 },
            new object[] { 1, 1, 1, 1, 1, 1 },
            new object[] { 0, 4, 15, 0, 4, 15 },
            new object[] { 10, 15, 20, 10, 15, 20 },
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValid3Params))]
        public void Constructor_Valid_3Params(int hour, int minutes, int seconds, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            var t = new Time(hour, minutes, seconds);
            Assert.AreEqual(expectedHour, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }

        public static IEnumerable<object[]> DataSetValid2Params => new List<object[]> {
            new object[] { 0, 0, 0, 0, 0 },
            new object[] { 12, 0, 12, 0, 0 },
            new object[] { 23, 59, 23, 59, 0 },
            new object[] { 1, 1, 1, 1, 0 },
            new object[] { 0, 4, 0, 4, 0 },
            new object[] { 10, 15, 10, 15, 0 },
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValid2Params))]
        public void Constructor_Valid_2Params(int hour, int minutes, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            var t = new Time(hour, minutes);
            Assert.AreEqual(expectedHour, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }

        public static IEnumerable<object[]> DataSetValid1Param => new List<object[]> {
            new object[] {0, 0, 0, 0},
            new object[] {23, 23, 0, 0},
            new object[] {1, 1, 0, 0},
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValid1Param))]
        public void Constructor_Valid_1Param(int hour, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            var t = new Time(hour);
            Assert.AreEqual(expectedHour, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }
        
        public static IEnumerable<object[]> DataSetToString => new List<object[]> {
            new object[] { 0, 0, 0 , "00:00:00" },
            new object[] { 12, 0, 0 , "12:00:00" },
            new object[] { 23, 59, 59, "23:59:59" },
            new object[] { 1, 1, 1, "01:01:01" },
            new object[] { 0, 4, 15, "00:04:15" },
            new object[] { 10, 15, 20, "10:15:20" },
        };
        [DataTestMethod, TestCategory("String representation")]
        [DynamicData(nameof(DataSetToString))]
        public void ToString_Valid_Format(int hour, int minutes, int seconds, string expected)
        {
            var t = new Time(hour, minutes, seconds);
            Assert.AreEqual(expected, t.ToString());
        }

        public static IEnumerable<object[]> DataSetComparisonGreater => new List<object[]> {
            new object[] {"12:0:0", "13:0:0", false},
            new object[] {"1:2:3", "1:3:3", false},
            new object[] {"1:2:3", "1:2:4", false},
            new object[] {"11:22:33", "11:22:32", true},
            new object[] {"11:22:33", "11:21:33", true },
            new object[] {"11:22:33", "10:22:33", true }
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonGreater))]
        public void TimePeriod_Comparisons_Greater(string a, string b, bool expected)
        {
            var t1 = new Time(a);
            var t2 = new Time(b);
            Assert.AreEqual(expected, t1 > t2);
        }

        public static IEnumerable<object[]> DataSetComparisonGreaterEqual => new List<object[]> {
            new object[] {"12:0:13", "13:0:13", false},
            new object[] {"10:50:40", "10:56:40", false},
            new object[] {"0:0:4", "0:0:5", false},
            new object[] {"1:5:4", "1:5:4", true},
            new object[] {"11:22:33", "11:22:32", true},
            new object[] {"11:22:33", "11:21:33", true },
            new object[] {"11:22:33", "10:22:33", true },
            new object[] {"11:22:34", "11:22:33", true}
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonGreaterEqual))]
        public void TimePeriod_Comparisons_Greater_Equal(string a, string b, bool expected)
        {
            var t1 = new Time(a);
            var t2 = new Time(b);
            Assert.AreEqual(expected, t1 >= t2);
        }

        public static IEnumerable<object[]> DataSetComparisonLower => new List<object[]> {
            new object[] {"12:0:0", "13:0:0", true},
            new object[] {"1:2:3", "1:3:3", true},
            new object[] {"1:2:3", "1:2:4", true},
            new object[] {"11:22:33", "11:22:32", false},
            new object[] {"11:22:33", "11:21:33", false },
            new object[] {"11:22:33", "10:22:33", false }
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonLower))]
        public void TimePeriod_Comparisons_Lower(string a, string b, bool expected)
        {
            var t1 = new Time(a);
            Console.WriteLine(t1);
            var t2 = new Time(b);
            Console.WriteLine(t2);
            Assert.AreEqual(expected, t1 < t2);
        }

        public static IEnumerable<object[]> DataSetComparisonLowerEqual => new List<object[]> {
            new object[] {"12:0:13", "13:0:13", true},
            new object[] {"10:50:40", "10:56:40", true},
            new object[] {"0:0:4", "0:0:5", true},
            new object[] {"1:5:4", "1:5:4", true},
            new object[] {"11:22:33", "11:22:32", false},
            new object[] {"11:22:33", "11:21:33", false },
            new object[] {"11:22:33", "10:22:33", false },
            new object[] {"11:22:34", "11:22:33", false }
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonLowerEqual))]
        public void TimePeriod_Comparisons_Lower_Equal(string a, string b, bool expected)
        {
            var t1 = new Time(a);
            var t2 = new Time(b);
            Assert.AreEqual(expected, t1 <= t2);
        }

        public static IEnumerable<object[]> DataSetComparisonEqual => new List<object[]> {
            new object[] {"0:0:0", "0:0:0", true},
            new object[] {"1:13:55", "1:6:4", false},
            new object[] {"01:05:45", "1:5:45", true},
            new object[] {"1:5:45", "01:05:45", true},
            new object[] {"11:22:33", "11:42:25", false },
            new object[] {"12:22:33", "10:31:21", false },
            new object[] {"11:22:33", "11:22:32", false },
            new object[] {"11:22:33", "11:22:33", true },
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonEqual))]
        public void TimePeriod_Comparisons_Equal(string a, string b, bool expected)
        {
            var t1 = new Time(a);
            var t2 = new Time(b);
            Assert.AreEqual(expected, t1 == t2);
        }

        public static IEnumerable<object[]> DataSetComparisonNotEqual => new List<object[]> {
            new object[] {"0:0:0", "0:0:0", false},
            new object[] {"1:13:55", "1:6:4", true},
            new object[] {"01:05:45", "1:5:45", false},
            new object[] {"1:5:45", "01:05:45", false},
            new object[] {"11:22:33", "11:42:25", true },
            new object[] {"12:22:33", "10:31:21", true },
            new object[] {"11:22:33", "11:22:32", true },
            new object[] {"11:22:33", "11:22:33", false },
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonNotEqual))]
        public void TimePeriod_Comparisons_Not_Equal(string a, string b, bool expected)
        {
            var t1 = new Time(a);
            var t2 = new Time(b);
            Assert.AreEqual(expected, t1 != t2);
        }

        public static IEnumerable<object[]> DataSetAdd => new List<object[]> {
            new object[] {"12:12:12", 1, "12:12:13"},
            new object[] {"1:0:5", 15 * 3600 + 24 * 60 + 53, "16:24:58"},
            new object[] {"13:5:14", 23 * 3600 + 14 * 60 + 54, "12:20:08"},
            new object[] {"0:0:0", 100 * 3600 + 0 * 60 + 0, "04:00:00"},
        };
        [DataTestMethod, TestCategory("Add and subtract")]
        [DynamicData(nameof(DataSetAdd))]
        public void Add(string a, long b, string expected)
        {
            var t1 = new Time(a);
            var t2 = new TimePeriod(b);
            var result = t1 + t2;
            Assert.AreEqual(expected, result.ToString());
        }

        public static IEnumerable<object[]> DataSetSubtract => new List<object[]> {
            new object[] {"1:1:1", 1, "01:01:00"},
            new object[] {"1:1:1", 5 * 3600 + 24 * 60 + 53, "19:36:08"},
            new object[] {"23:20:17", 72 * 3600 + 46 * 60 + 1, "22:34:16"},
        };
        [DataTestMethod, TestCategory("Add and subtract")]
        [DynamicData(nameof(DataSetSubtract))]
        public void Subtract(string a, long b, string expected)
        {
            var t1 = new Time(a);
            var t2 = new TimePeriod(b);
            var result = t1 - t2;
            Assert.AreEqual(expected, result.ToString());
        }
    }
}
