using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class GradingCalculatorNUnitTests
    {
        private GradingCalculator gradingCalculator;
        [SetUp]
        public void Setup()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Test]
        public void GetGrade_InputScore95Attendance90_ReturnA()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            var actual = gradingCalculator.GetGrade();

            Assert.AreEqual("A", actual);
        }

        [Test]
        public void GetGrade_InputScore85Attendance90_ReturnB()
        {
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            var actual = gradingCalculator.GetGrade();

            Assert.AreEqual("B", actual);
        }

        [Test]
        public void GetGrade_InputScore65Attendance90_ReturnC()
        {
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            var actual = gradingCalculator.GetGrade();

            Assert.AreEqual("C", actual);
        }

        [Test]
        public void GetGrade_InputScore95Attendance65_ReturnB()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            var actual = gradingCalculator.GetGrade();

            Assert.AreEqual("B", actual);
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GetGrade_InputFailureScoreAndAttendance_ReturnF(int score, int attendancePercentage)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendancePercentage;

            var actual = gradingCalculator.GetGrade();

            Assert.That(actual, Is.EqualTo("F"));
        }

        [Test]
        public void GetGrade_InputMultipleScoreAndAttendanceAtOnce_ReturnGrades()
        {
            Assert.Multiple(() =>
            {
                gradingCalculator.Score = 95;
                gradingCalculator.AttendancePercentage = 90;
                var actual = gradingCalculator.GetGrade();
                Assert.AreEqual("A", actual);

                gradingCalculator.Score = 85;
                gradingCalculator.AttendancePercentage = 90;
                actual = gradingCalculator.GetGrade();
                Assert.AreEqual("B", actual);

                gradingCalculator.Score = 65;
                gradingCalculator.AttendancePercentage = 90;
                actual = gradingCalculator.GetGrade();
                Assert.AreEqual("C", actual);

                gradingCalculator.Score = 95;
                gradingCalculator.AttendancePercentage = 65;
                actual = gradingCalculator.GetGrade();
                Assert.AreEqual("B", actual);
            });
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]

        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_InputMultipleScoreAndAttendance_ReturnGrades(int score, int attendancePercentage)
        {
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendancePercentage;

            return gradingCalculator.GetGrade();
        }
    }
}
