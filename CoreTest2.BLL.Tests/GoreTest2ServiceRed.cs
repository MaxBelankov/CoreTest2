using CoreTest2.BLL.Implementations;
using CoreTest2.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoreTest2.BLL.Tests
{
    public class CoreTest2ServiceRed
    {
        readonly CoreTest2Service _service = new CoreTest2Service();

        [Fact]
        public void Get_WrongEmployeeId()
        {
            Assert.Throws<ArgumentException>(() => _service.GetEmployeeByID(100));
        }

        [Fact]
        public void Get_WrongPageNumber()
        {
            Assert.Throws<ArgumentException>(() => _service.GetEmployeesAsSortedArray(100, SortingCondition.Default));
        }

        [Fact]
        public void Put_WrongEmployeeId()
        {
            Assert.Throws<ArgumentException>(() => _service.ChangeEmployee(100, new Common.Models.Employee()));
        }

        [Fact]
        public void Delete_WrongEmployeeId()
        {
            Assert.Throws<ArgumentException>(() => _service.RemoveEmployee(100));
        }

    }
}
