var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


// ==============================
// Health Check API
// ==============================

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        application = "Employee Management System",
        framework = ".NET 11",
        timestamp = DateTime.UtcNow
    });
});


// ==============================
// Employee Data
// ==============================

var employees = new List<Employee>
{
    new Employee
    {
        Id = 1,
        Name = "John Smith",
        Email = "john.smith@example.com",
        Department = "IT",
        Position = "Software Engineer",
        Status = "Active"
    },

    new Employee
    {
        Id = 2,
        Name = "Mary Johnson",
        Email = "mary.johnson@example.com",
        Department = "HR",
        Position = "HR Manager",
        Status = "Active"
    },

    new Employee
    {
        Id = 3,
        Name = "David Wilson",
        Email = "david.wilson@example.com",
        Department = "Finance",
        Position = "Financial Analyst",
        Status = "Active"
    },

    new Employee
    {
        Id = 4,
        Name = "Sarah Brown",
        Email = "sarah.brown@example.com",
        Department = "Marketing",
        Position = "Marketing Executive",
        Status = "Active"
    },

    new Employee
    {
        Id = 5,
        Name = "Michael Davis",
        Email = "michael.davis@example.com",
        Department = "IT",
        Position = "DevOps Engineer",
        Status = "Active"
    }
};


// ==============================
// Get All Employees
// ==============================

app.MapGet("/api/employees", () =>
{
    return Results.Ok(employees);
});


// ==============================
// Get Employee By ID
// ==============================

app.MapGet("/api/employees/{id:int}", (int id) =>
{
    var employee = employees.FirstOrDefault(x => x.Id == id);

    if (employee == null)
    {
        return Results.NotFound(new
        {
            message = "Employee not found"
        });
    }

    return Results.Ok(employee);
});


// ==============================
// Add Employee
// ==============================

app.MapPost("/api/employees", (Employee employee) =>
{
    employee.Id = employees.Count == 0
        ? 1
        : employees.Max(x => x.Id) + 1;

    employees.Add(employee);

    return Results.Created(
        $"/api/employees/{employee.Id}",
        employee
    );
});


// ==============================
// Delete Employee
// ==============================

app.MapDelete("/api/employees/{id:int}", (int id) =>
{
    var employee = employees.FirstOrDefault(x => x.Id == id);

    if (employee == null)
    {
        return Results.NotFound(new
        {
            message = "Employee not found"
        });
    }

    employees.Remove(employee);

    return Results.Ok(new
    {
        message = "Employee deleted successfully"
    });
});


// ==============================
// Start Application
// ==============================

app.Run();


// ==============================
// Employee Model
// ==============================

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string Department { get; set; } = "";

    public string Position { get; set; } = "";

    public string Status { get; set; } = "";
}
