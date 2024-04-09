using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeApp.Models;
using EmployeeApp.Services;
using Microsoft.Toolkit.Mvvm.Input;

namespace EmployeeApp.ViewModels
{
    //manages the collection of employee data and inherits from ObservableObject
    public partial class EmployeesViewModel : ObservableObject
    {
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public readonly IEmployeeService _employeeService;
        public EmployeesViewModel(IEmployeeService employeeService) 
        { 

            _employeeService = employeeService;
        }

        //retrieves the list of employees from the data
        [ICommand]
        public async void GetEmployeeList()
        {
            var employees = await _employeeService.GetEmployeesList();
            if(employees?.Count > 0) 
            {
                //clears the existing employee
                Employees.Clear();
                foreach (var employee in employees)
                {
                    
                    Employees.Add(employee);
                }
            }
        }

        //navigates to the AddEmployee page 
        [ICommand]
        public async void AddUpdateEmployee()
        {
            await AppShell.Current.GoToAsync(nameof(AddEmployee));
        }

        //displays an action with options to edit or delete
        [ICommand]
        public async void DisplayAction(Employee employee)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");

            if (response == "Edit")
            {
                var deleteresponse = await _employeeService.DeleteEmployee(employee);
                var navparam = new Dictionary<string, object>();
                navparam.Add("AddEmployee", employee);
                await AppShell.Current.GoToAsync(nameof(AddEmployee), navparam);
            }
            if (response == "Delete")
            {
                var deleteresponse = await _employeeService.DeleteEmployee(employee);
                if (deleteresponse > 0)
                {
                    GetEmployeeList();
                }
            }
        }

        
    }
}

