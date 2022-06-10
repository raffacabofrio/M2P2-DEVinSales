using DevInSales.Services;
using NUnit.Framework;

namespace DevInSalesTest
{
    public class CalculatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(-3, 14, 11)]
        public void DeveSomarCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Soma(x, y);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(5, 7, -2)]
        [TestCase(3, 1, 2)]
        [TestCase(-3, 14, -17)]
        public void DeveSubtrairCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Subtrair(x, y);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(5, 7, 35)]
        [TestCase(3, 1, 3)]
        [TestCase(-3, 14, -42)]
        public void DeveMultiplicarCorretamente(int x, int y, int expected)
        {
            var result = CalculatorService.Multiplicar(x, y);
            Assert.AreEqual(expected, result);
        }


    }
}