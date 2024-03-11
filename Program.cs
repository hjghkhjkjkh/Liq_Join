class Program {
    static void Main(String[] args) {
        List<Department> departments = Department.GetDepartments();
        List<Employee> employees = Employee.GetEmployees();

        //Max, MIn, Average
        Console.WriteLine("\n-----Max, min and average of salary-----");
        var MaxSalary = employees.Max(x => x.Salary);
        var MinSalary = employees.Min(x => x.Salary);
        var AverageSalary = employees.Average(x => x.Salary);
        Console.WriteLine("MaxSalary: " + MaxSalary + "\nMinSalary: " + MinSalary + "\nAverageSalary: " + AverageSalary + "\n");

        Console.WriteLine("-----Group join, left join and right join-----");
        // Left Join
        var leftJoinQuery =
            from department in departments
            join employee in employees
            on department.ID equals employee.DepartmentID into empGroup
            from emp in empGroup.DefaultIfEmpty()
            select new {
                DepartmentName = department.Name,
                EmployeeName = emp == null ? "No employee" : emp.Name
            };

        Console.WriteLine("Left Join:");
        foreach (var item in leftJoinQuery) {
            Console.WriteLine($"{item.DepartmentName} - {item.EmployeeName}");
        }

        // Right Join
        var rightJoinQuery =
            from employee in employees
            join department in departments
            on employee.DepartmentID equals department.ID into deptGroup
            from dept in deptGroup.DefaultIfEmpty()
            select new {
                EmployeeName = employee.Name,
                DepartmentName = dept == null ? "No department" : dept.Name
            };

        Console.WriteLine("\nRight Join:");
        foreach (var item in rightJoinQuery) {
            Console.WriteLine($"{item.EmployeeName} - {item.DepartmentName}");
        }

        // Group Join
        var groupJoinQuery =
            from department in departments
            join employee in employees
            on department.ID equals employee.DepartmentID into empGroup
            select new {
                DepartmentName = department.Name,
                Employees = empGroup.Select(emp => emp.Name)
            };

        Console.WriteLine("\nGroup Join:");
        foreach (var item in groupJoinQuery) {
            Console.WriteLine($"{item.DepartmentName}: {string.Join(", ", item.Employees)}");
        }

        //Max, Min age
        Console.WriteLine("\n-----Max and min of age-----");
        int MaxAge = employees.Max(emp => CalculateAge(emp.Birthday));
        Console.WriteLine($"Max Age: {MaxAge}");
        int MinAge = employees.Min(emp => CalculateAge(emp.Birthday));
        Console.WriteLine($"Min Age: {MinAge}");
    }

    static int CalculateAge(DateTime birthDate) {
        DateTime now = DateTime.Today;
        int age = now.Year - birthDate.Year;
        if (birthDate > now.AddYears(-age)) age--;
        return age;
    }
}
/*var departmentEmployees = from department in departments
                                  join employee in employees on department.ID equals employee.DepartmentID into employeeGroup
                                  select new {
                                      Department = department,
                                      Employees = employeeGroup.ToList()
                                  };

        foreach (var depEmp in departmentEmployees) {
            Console.WriteLine($"Department: {depEmp.Department.Name}");
            foreach (var employee in depEmp.Employees) {
                Console.WriteLine($"   Employee: {employee.Name}");
            }
            Console.WriteLine();
        }*/