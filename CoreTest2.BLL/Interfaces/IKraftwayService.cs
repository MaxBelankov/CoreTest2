using CoreTest2.Common;
using CoreTest2.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTest2.BLL.Interfaces
{
    public interface ICoreTest2Service
    {
        Employee GetEmployeeByID(int Id);
        Employee[] GetEmployeesAsSortedArray(int page, SortingCondition condition);
        void AddEmployee(Employee newEmployee);
        void ChangeEmployee(int Id, Employee changedEmployee);
        void RemoveEmployee(int Id);
    }
}
