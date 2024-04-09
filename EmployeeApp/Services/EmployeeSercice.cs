using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeApp.Models;
using SQLite;

namespace EmployeeApp.Services
{
    //Manages interactions with an SQLite
    public class EmployeeSercice : IEmployeeService
    {   
        // link to SQLite database
        public SQLiteAsyncConnection _dbConnection;

        //SQLite database connection and ensures table exists
        private async Task SetupDatabase()
        {
            if (_dbConnection == null)
            {
                string _dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Employee.db3");
                _dbConnection = new SQLiteAsyncConnection(_dbpath);
                await _dbConnection.CreateTableAsync<Employee>();

            }
            
        }


        //use exception and add new employee to database
        public async Task<int> AddEmployee(Employee employee)
        {
            try
            {
                await SetupDatabase();
                return await _dbConnection.InsertAsync(employee);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error adding employee: {ex.Message}");
                throw; // Rethrow the exception to propagate it to the caller
            }
        }

        //Deletes existing employee from database
        public async Task<int> DeleteEmployee(Employee employee)
        {
            await SetupDatabase();
            return await _dbConnection.DeleteAsync(employee);
        }

        //Updates an existing employee in database
        public async Task<int> UpdateEmployee(Employee employee)
        {
            await SetupDatabase();
            return await _dbConnection.UpdateAsync(employee);
        }

        //retrieves employee from database
        public async Task<List<Employee>> GetEmployeesList()
        {
            await SetupDatabase();
            var employeeslist= await _dbConnection.Table<Employee>().ToListAsync();
            return employeeslist;
        }

  
    }
}
