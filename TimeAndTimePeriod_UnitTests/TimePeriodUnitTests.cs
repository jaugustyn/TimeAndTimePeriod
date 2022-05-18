using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeAndTimePeriod;

namespace TimeAndTimePeriod_UnitTests
{
    [TestClass]
    public class TimePeriodUnitTests
    {
        public static IEnumerable<object[]> DataSetNegativeSecondsArgOutOfRangeEx => new List<object[]> {
            new object[] {-12},
            new object[] {-999999},
            new object[] {-1}
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetNegativeSecondsArgOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_Negative_Seconds_ArgumentOutOfRangeException(long a)
        {
            var t = new TimePeriod(a);
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
            var t = new TimePeriod(a);
        }

        public static IEnumerable<object[]> DataSetInvalid3ParamsArgOutOfRangeEx => new List<object[]> {
            new object[] {-1, 12, 0},
            new object[] {1, -1111, -5},
            new object[] {-0, -2, -1},
            new object[] {-24, 12, -5},
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetInvalid3ParamsArgOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_Invalid_3_Params(int a, int b, int c)
        {
            var t = new TimePeriod(a, b, c);
        }

        public static IEnumerable<object[]> DataSetValidString => new List<object[]> {
            new object[] {"0:0:0", 0},
            new object[] {"111:0:0", 111 * 3600},
            new object[] {"15:12:1777", 15*3600 + 12 * 60 + 1777},
            new object[] {"1000:1000:1000", 1000 * 3600 + 1000 * 60 + 1000},
            new object[] {"59:59:59", 59 * 3600 + 59 * 60 + 59},
            new object[] {"0:01:0", 0 * 3600 + 1 * 60 + 0},
            new object[] {"25:14:03", 25 * 3600 + 14 * 60 + 3}
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValidString))]
        public void Constructor_Valid_String(string a, long expectedSeconds)
        {
            var t = new TimePeriod(a);
            Assert.AreEqual(expectedSeconds, t.Time);
        }

        public static IEnumerable<object[]> DataSetValid1ParamTimeUnit => new List<object[]> {
            new object[] {1, 1},
            new object[] {1111111, 1111111},
            new object[] {626, 626},
            new object[] {0, 0}
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValid1ParamTimeUnit))]
        public void Constructor_Valid_1_Param(long a, long expected)
        {
            var t = new TimePeriod(a);
            Assert.AreEqual(expected, t.Time);
        }

        public static IEnumerable<object[]> DataSetValid2Params => new List<object[]> {
            new object[] {1, 1, 1 * 3600 + 1 * 60},
            new object[] {511, 111, 511 * 3600 + 111 * 60},
            new object[] {0, 5, 5 * 60},
            new object[] {0, 0, 0 * 60}
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValid2Params))]
        public void Constructor_Valid_2_Params(int a, int b, int expected)
        {
            var t = new TimePeriod(a, b);

            Assert.AreEqual(expected, t.Time);
        }

        public static IEnumerable<object[]> DataSetValid3Params => new List<object[]> {
            new object[] {1, 1, 1, 1 * 3600 + 1 * 60 + 1},
            new object[] {0, 0, 0, 0 * 3600 + 0 * 60 + 0},
            new object[] {111, 11, 1, 111 * 3600 + 11 * 60 + 1},
            new object[] {0, 0, 10, 0 * 3600 + 0 * 60 + 10}
        };
        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSetValid3Params))]
        public void Constructor_Valid_3_Params(int a, int b, int c, long expected)
        {
            var t = new TimePeriod(a, b, c);

            Assert.AreEqual(expected, t.Time);
        }
        
        public static IEnumerable<object[]> DataSetToString => new List<object[]> {
            new object[] {111, "0:01:51"},
            new object[] {1, "0:00:01"},
            new object[] {0, "0:00:00"},
            new object[] {12 * 3600 + 12 * 60 + 12, "12:12:12"},
            new object[] {5 * 60 + 59, "0:05:59"},
            new object[] {26 * 3600 + 2 * 60 + 12, "26:02:12"},
            new object[] {600 * 3600 + 55 * 60, "600:55:00"}
        };
        [DataTestMethod, TestCategory("String representation")]
        [DynamicData(nameof(DataSetToString))]
        public void ToString_Valid_Format(long a, string expected)
        {
            var t = new TimePeriod(a);
            Assert.AreEqual(expected, t.ToString());
        }

        public static IEnumerable<object[]> DataSetComparisonGreater => new List<object[]> {
            new object[] {12, 11, true},
            new object[] {125, 5, true},
            new object[] {125, 888, false},
            new object[] {2, 15, false},
            new object[] {2, 2, false},
            new object[] {744, 7772, false},
            new object[] {0, 0, false},
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonGreater))]
        public void TimePeriod_Comparisons_Greater(long a, long b, bool expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            Assert.AreEqual(expected, t1 > t2);
        }

        public static IEnumerable<object[]> DataSetComparisonGreaterEqual => new List<object[]> {
            new object[] {12, 11, true},
            new object[] {125, 5, true},
            new object[] {125, 888, false},
            new object[] {2, 15, false},
            new object[] {2, 2, true},
            new object[] {744, 7772, false},
            new object[] {0, 0, true},
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonGreaterEqual))]
        public void TimePeriod_Comparisons_Greater_Equal(long a, long b, bool expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            Assert.AreEqual(expected, t1 >= t2);
        }

        public static IEnumerable<object[]> DataSetComparisonLower => new List<object[]> {
            new object[] {12, 11, false},
            new object[] {125, 5, false},
            new object[] {125, 888, true},
            new object[] {2, 15, true},
            new object[] {2, 2, false},
            new object[] {744, 7772, true},
            new object[] {0, 0, false},
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonLower))]
        public void TimePeriod_Comparisons_Lower(long a, long b, bool expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            Assert.AreEqual(expected, t1 < t2);
        }

        public static IEnumerable<object[]> DataSetComparisonLowerEqual => new List<object[]> {
            new object[] {12, 11, false},
            new object[] {125, 5, false},
            new object[] {125, 888, true},
            new object[] {2, 15, true},
            new object[] {2, 2, true},
            new object[] {744, 7772, true},
            new object[] {0, 0, true},
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonLowerEqual))]
        public void TimePeriod_Comparisons_Lower_Equal(long a, long b, bool expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            Assert.AreEqual(expected, t1 <= t2);
        }

        public static IEnumerable<object[]> DataSetComparisonEqual => new List<object[]> {
            new object[] {12, 11, false},
            new object[] {125, 5, false},
            new object[] {125, 888, false},
            new object[] {2, 15, false},
            new object[] {2, 2, true},
            new object[] {744, 7772, false},
            new object[] {0, 0, true},
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonEqual))]
        public void TimePeriod_Comparisons_Equal(long a, long b, bool expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            Assert.AreEqual(expected, t1 == t2);
        }

        public static IEnumerable<object[]> DataSetComparisonNotEqual => new List<object[]> {
            new object[] {12, 11, true},
            new object[] {125, 5, true},
            new object[] {125, 888, true},
            new object[] {2, 15, true},
            new object[] {2, 2, false},
            new object[] {744, 7772, true},
            new object[] {0, 0, false},
        };
        [DataTestMethod, TestCategory("Comparison")]
        [DynamicData(nameof(DataSetComparisonNotEqual))]
        public void TimePeriod_Comparisons_Not_Equal(long a, long b, bool expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            Assert.AreEqual(expected, t1 != t2);
        }
        
        public static IEnumerable<object[]> DataSetAdd => new List<object[]> {
            new object[] {1, 1, 1 + 1},
            new object[] {0, 0, 0 + 0},
            new object[] {9999, 1111, 9999 + 1111},
            new object[] {0, 1, 0 + 1},
            new object[] {123, 0, 123 + 0},
            new object[] {500, 400, 500 + 400}
        };
        [DataTestMethod, TestCategory("Add, subtract")]
        [DynamicData(nameof(DataSetAdd))]
        public void TimePeriod_Add(long a, long b, long expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            var result = t1 + t2;
            Assert.AreEqual(expected, result.Time);
        }

        public static IEnumerable<object[]> DataSetSubtract => new List<object[]> {
            new object[] {1, 1, 0},
            new object[] {0, 0, 0},
            new object[] {9999, 1111, 9999 - 1111},
            new object[] {0, 1, 0},
            new object[] {123, 0, 123},
            new object[] {500, 400, 500 - 400}
        };
        [DataTestMethod, TestCategory("Add, subtract")]
        [DynamicData(nameof(DataSetSubtract))]
        public void TimePeriod_Subtract(long a, long b, long expected)
        {
            var t1 = new TimePeriod(a);
            var t2 = new TimePeriod(b);
            var result = t1 - t2;
            Assert.AreEqual(expected, result.Time);
        }
    }
}