using System;
using System.Globalization;
using System.Threading;
using Core.Services;
using Xunit;

namespace EmployeeApp.UnitTests
{
    public class DateConverterTests
    {
        [Fact]
        public void Test()
        {
            // Arrange
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            const string dateValue = "01/06/2002";
            var dateWithLeadingZeros = new DateTime(2002, 6, 1).ToString("dd/MM/yyyy");
            var dateWithoutDayLeadingZero = new DateTime(2002, 6, 1).ToString("d/MM/yyyy");
            var dateWithoutMonthLeadingZero = new DateTime(2002, 6, 1).ToString("dd/M/yyyy");
            var dateWithoutDayMonthLeadingZero = new DateTime(2002, 6, 1).ToString("d/M/yyyy");
            
            // Act
            var dateWithLeadingZeroResult = DateConverter.ConvertDateToAcceptedFormat<DumpClass>(dateWithLeadingZeros);
            var dateWithoutDateLeadingZeroResult = DateConverter.ConvertDateToAcceptedFormat<DumpClass>(dateWithoutDayLeadingZero);
            var dateWithoutMonthLeadingZeroResult = DateConverter.ConvertDateToAcceptedFormat<DumpClass>(dateWithoutMonthLeadingZero);
            var dateWithoutDayMonthLeadingZeroResult = DateConverter.ConvertDateToAcceptedFormat<DumpClass>(dateWithoutDayMonthLeadingZero);
            
            // Assert
            Assert.Equal(dateValue, dateWithLeadingZeroResult);
            Assert.Equal(dateValue, dateWithoutDateLeadingZeroResult);
            Assert.Equal(dateValue, dateWithoutMonthLeadingZeroResult);
            Assert.Equal(dateValue, dateWithoutDayMonthLeadingZeroResult);
        }
    }
}