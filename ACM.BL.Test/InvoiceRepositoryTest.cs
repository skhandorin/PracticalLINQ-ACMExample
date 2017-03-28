using ACM.BL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BL.Test
{
    [TestClass]
    public class InvoiceRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CalculateTotalAmountInvoicedTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var actual = repository.CalculateTotalAmountInvoiced(invoiceList);

            Assert.AreEqual(1333.14M, actual);
        }

        [TestMethod]
        public void CalculateTotalUnitSoldTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var actual = repository.CalculateTotalUnitSold(invoiceList);

            Assert.AreEqual(136, actual);
        }


        [TestMethod]
        public void GetInvoiceTotalByIsPaidTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var query = repository.GetInvoiceTotalByIsPaid(invoiceList);

            // not really a test
        }

        [TestMethod]
        public void GetInvoiceTotalByIsPaidAndMonthTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var query = repository.GetInvoiceTotalByIsPaidAndMonth(invoiceList);

            // not really a test
        }

        [TestMethod()]
        public void CalculateMeanTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var actual = repository.CalculateMean(invoiceList);

            Assert.AreEqual(6.875M, actual);
        }

        [TestMethod()]
        public void CalculateMedianTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var actual = repository.CalculateMedian(invoiceList);

            Assert.AreEqual(10M, actual);
        }

        [TestMethod()]
        public void CalculateModeTest()
        {
            InvoiceRepository repository = new InvoiceRepository();
            var invoiceList = repository.Retrieve();

            var actual = repository.CalculateMode(invoiceList);

            Assert.AreEqual(10M, actual);
        }
    }
}
