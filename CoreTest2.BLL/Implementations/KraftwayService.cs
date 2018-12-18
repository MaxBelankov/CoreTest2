using CoreTest2.BLL.Interfaces;
using CoreTest2.Common;
using CoreTest2.Common.Models;
using CoreTest2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreTest2.BLL.Implementations
{
    public class CoreTest2Service : ICoreTest2Service
    {
        const int PageLength = 20;
        readonly InMemoryDbContext _context = new InMemoryDbContext();

        public void AddEmployee(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }

        public void ChangeEmployee(int Id, Employee changedEmployee)
        {
            var current = _context.Employees.Find(Id);
            if (current == null) throw new ArgumentException(nameof(Id));
            current.EmploymentDate = changedEmployee.EmploymentDate;
            current.Name = changedEmployee.Name;
            current.Position = changedEmployee.Position;
            current.Salary = changedEmployee.Salary;
            current.WorkplaceNo = changedEmployee.WorkplaceNo;
            _context.Employees.Update(current);
            _context.SaveChanges();
        }

        public Employee GetEmployeeByID(int Id)
        {
            var result = _context.Employees.Find(Id);
            if (result == null) throw new ArgumentException(nameof(Id));
            return result;
        }

        public Employee[] GetEmployeesAsSortedArray(int page, SortingCondition condition)
        {
            IOrderedQueryable<Employee> resultSet = null;
            if (condition.Direction == SortingDirection.Asc)
            {
                switch (condition.Field)
                {
                    case SortingField.Id:
                        resultSet = _context.Employees.OrderBy(x => x.Id);
                        break;
                    case SortingField.Name:
                        resultSet = _context.Employees.OrderBy(x => x.Name);
                        break;
                    case SortingField.Position:
                        resultSet = _context.Employees.OrderBy(x => x.Position);
                        break;
                    case SortingField.Salary:
                        resultSet = _context.Employees.OrderBy(x => x.Salary);
                        break;
                    case SortingField.EmploymentDate:
                        resultSet = _context.Employees.OrderBy(x => x.EmploymentDate);
                        break;
                    case SortingField.WorkplaceNo:
                        resultSet = _context.Employees.OrderBy(x => x.WorkplaceNo);
                        break;
                }
            }
            else
            {
                switch (condition.Field)
                {
                    case SortingField.Id:
                        resultSet = _context.Employees.OrderByDescending(x => x.Id);
                        break;
                    case SortingField.Name:
                        resultSet = _context.Employees.OrderByDescending(x => x.Name);
                        break;
                    case SortingField.Position:
                        resultSet = _context.Employees.OrderByDescending(x => x.Position);
                        break;
                    case SortingField.Salary:
                        resultSet = _context.Employees.OrderByDescending(x => x.Salary);
                        break;
                    case SortingField.EmploymentDate:
                        resultSet = _context.Employees.OrderByDescending(x => x.EmploymentDate);
                        break;
                    case SortingField.WorkplaceNo:
                        resultSet = _context.Employees.OrderByDescending(x => x.WorkplaceNo);
                        break;
                }
            }

            var result = resultSet.Skip(PageLength * page).Take(PageLength).ToArray();
            if (!result.Any()) throw new ArgumentException();
            return result;
        }

        public void RemoveEmployee(int Id)
        {
            var toDelete = _context.Employees.Find(Id);
            if (toDelete == null) throw new ArgumentException(nameof(Id));
            _context.Employees.Remove(toDelete);
            _context.SaveChanges();
        }
    }
}
