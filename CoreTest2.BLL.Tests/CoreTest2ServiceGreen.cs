using CoreTest2.BLL.Implementations;
using CoreTest2.Common;
using CoreTest2.Common.Models;
using CoreTest2.DAL;
using System;
using System.Linq;
using Xunit;

namespace CoreTest2.BLL.Tests
{
    public class CoreTest2ServiceGreen: IDisposable
    {
        readonly InMemoryDbContext context;
        readonly CoreTest2Service _service = new CoreTest2Service();

        public CoreTest2ServiceGreen()
        {
            context = new InMemoryDbContext();
            //populate db
            for(int i=1; i<31; i++)
            {
                var employee = new Employee
                {
                    Id = i,
                    Name = $"Employee{i}",
                    WorkplaceNo = 31-i
                };
                context.Employees.Add(employee);
            }
            context.SaveChanges();
        }

        public void Dispose()
        {
            var all = context.Employees.ToArray();
            context.Employees.RemoveRange(all);
            context.SaveChanges();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        [InlineData(5)]
        [InlineData(25)]
        public void GetEmployeeByID_Test(int id)
        {
            var employee = _service.GetEmployeeByID(id);
            Assert.Equal($"Employee{id}", employee.Name);
        }

        [Fact]
        public void GetEmployeesAsSortedArray_Test()
        {
            var employees = _service.GetEmployeesAsSortedArray(0, SortingCondition.Default);
            Assert.Equal(20, employees.Count());
        }

        [Fact]
        public void GetEmployeesAsSortedArray_Pagination_Test()
        {
            var employees = _service.GetEmployeesAsSortedArray(1, SortingCondition.Default);
            Assert.Equal(10, employees.Count());
        }

        [Fact]
        public void GetEmployeesAsSortedArray_Sorting_Test()
        {
            var employees = _service.GetEmployeesAsSortedArray(0,  new SortingCondition {Field = SortingField.WorkplaceNo, Direction = SortingDirection.Asc });
            Assert.Equal(30, employees[0].Id);
        }

        [Fact]
        public void AddEmployee_Test()
        {
            var employee = new Employee
            {
                Id = 31,
                Name = "Иванов",
                WorkplaceNo = 31
            };

            _service.AddEmployee(employee);

            var check = _service.GetEmployeeByID(31);
            Assert.NotNull(check);
            Assert.Equal("Иванов", check.Name);
        }

        [Fact]
        public void ChangeEmployee_Test()
        {
            var employee = new Employee
            {
                Name = "Иванов",
                WorkplaceNo = 42
            };

            _service.ChangeEmployee(1, employee);

            var check = _service.GetEmployeeByID(1);
            Assert.NotNull(check);
            Assert.Equal("Иванов", check.Name);
        }

        [Fact]
        public void RemoveEmployee_Test()
        {
            _service.RemoveEmployee(1);

            var check = _service.GetEmployeesAsSortedArray(0, SortingCondition.Default);
            Assert.DoesNotContain(check, x =>x.Id==1);
        }
    }
}
