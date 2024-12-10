using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EmpMgmtApp;
using HelperModule;


namespace IntegrationEmployeeWebApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            string baseUrl = "https://employeemgmtsangeetha.azurewebsites.net/";

            HttpClient client = new HttpClient();
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. View Employees");
                Console.WriteLine("2. Get Employee by ID");
                Console.WriteLine("3. Add an Employee");
                Console.WriteLine("4. Update Employee Details");
                Console.WriteLine("5. Delete Employee");

                int choice = int.Parse(Console.ReadLine() ?? "0");

                switch (choice)
                {
                    case 1:
                        await ViewEmployees(client, baseUrl);
                        break;
                    case 2:
                        await GetEmployeeById(client, baseUrl);
                        break;
                    case 3:
                        await AddEmployee(client, baseUrl);
                        break;
                    case 4:
                        await UpdateEmployee(client, baseUrl);
                        break;
                    case 5:
                        await DeleteEmployee(client, baseUrl);
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                Console.ReadKey();
            }
        }

        static async Task ViewEmployees(HttpClient client, string baseUrl)
        {
            var Employees = await client.GetFromJsonAsync<List<Employee>>(baseUrl + "/ViewEmployee");
            Console.WriteLine("Employees:");
            foreach (var Employee in Employees)
            {
                Console.WriteLine($"{Employee.EmpId}: {Employee.Name}, Salary: {Employee.Salary}, Department: {Employee.DeptName}");
            }
        }

        static async Task GetEmployeeById(HttpClient client, string baseUrl)
        {
            Console.Write("Enter Employee ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var response = await client.GetAsync($"{baseUrl}ViewEmployeeByID/{id}");
            if (response.IsSuccessStatusCode)
            {
                var Employee = await response.Content.ReadFromJsonAsync<Employee>();
                Console.WriteLine($"Employee: {Employee.Name}, Salary: {Employee.Salary}, Department: {Employee.DeptName}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static async Task AddEmployee(HttpClient client, string baseUrl)
        {
            int EmpId = HelperModules.GetIntegerInput("Enter Employee ID: ");
            string Name = HelperModules.GetStringInput("Enter Employee Name: ");
            decimal Salary = HelperModules.GetDecimalInput("Enter Salary: ");
            string Department = HelperModules.GetStringInput("Enter Department: ");

            var newEmployee = new Employee
            {

                EmpId = EmpId,
                Name = Name,
                Salary = Salary,
                DeptName = Department
            };

            var response = await client.PostAsJsonAsync($"{baseUrl}AddEmployee", newEmployee);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Employee added successfully!");
            }
            else
            {
                Console.WriteLine("Error adding Employee.");
            }
        }

        static async Task UpdateEmployee(HttpClient client, string baseUrl)
        {
            Console.Write("Enter Employee ID to update: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            string _name = HelperModules.GetStringInput("Enter Employee Name: ");
            decimal? _salary = HelperModules.GetDecimalInput("Enter Salary: ");
            string _department = HelperModules.GetStringInput("Enter Department: ");

            var updatedEmployee = new Employee
            {
                Name = _name,
                Salary = _salary,
                DeptName = _department
            };

            var response = await client.PutAsJsonAsync($"{baseUrl}UpdateEmployee/{id}", updatedEmployee);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Employee updated successfully!");
            }
            else
            {
                Console.WriteLine("Error updating Employee.");
            }
        }

        static async Task DeleteEmployee(HttpClient client, string baseUrl)
        {
            Console.Write("Enter Employee ID to delete: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var response = await client.DeleteAsync($"{baseUrl}DeleteEmployee/{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Employee deleted successfully!");
            }
            else
            {
                Console.WriteLine("Error deleting Employee.");
            }
        }
    }
}
