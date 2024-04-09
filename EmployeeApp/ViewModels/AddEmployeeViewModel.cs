using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EmployeeApp.Models;
using EmployeeApp.Services;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.ViewModels
{
    [QueryProperty(nameof(AddEmployee),"AddEmployee")]
    //using abstract class and inheritance
    public partial class AddEmployeeViewModel : ObservableObject
    {
        [ObservableProperty]
        private Employee employeeDetails = new Employee();

        private readonly IEmployeeService _employeeService;

        //manages addition of employee details
        public AddEmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }



        [ICommand]
        public async void AddEmployee()
        {
            //the data you need to save
            var response = await  _employeeService.AddEmployee(employeeDetails);
            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Recoed Added", "Employee Details Successfully Submitted", "ok");
            }
            else
            {
                //Displays alert messages to user
                await Shell.Current.DisplayAlert("Not Added", "Something Wrong with Employee Details", "ok");
            }
        }


    }
}
