using System;
using System.Collections.Generic;

class Program
{
    static List<Task> tasks = new List<Task>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Task Management System");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Edit Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    EditTask();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Returning to the main menu...");
                    PauseAndReturnToMenu();
                    break;
            }
        }
    }

    static void AddTask()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Add New Task");
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) throw new Exception("Title cannot be empty.");

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            DateTime startDate, endDate;

            Console.Write("Enter start date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out startDate))
                throw new Exception("Invalid start date format.");

            Console.Write("Enter end date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out endDate) || endDate < startDate)
                throw new Exception("End date must be the same or after the start date.");

            tasks.Add(new Task
            {
                Title = title,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                IsCompleted = false
            });

            Console.WriteLine("Task added successfully!");
            PauseAndReturnToMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            PauseAndReturnToMenu();
        }
    }

    static void ViewTasks()
    {
        Console.Clear();
        Console.WriteLine("Task List:");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                Console.WriteLine($"{i + 1}. [ {(task.IsCompleted ? "X" : " ")} ] {task.Title}");
                Console.WriteLine($"   Description: {task.Description}");
                Console.WriteLine($"   Start Date: {task.StartDate:yyyy-MM-dd}");
                Console.WriteLine($"   End Date: {task.EndDate:yyyy-MM-dd}");
                Console.WriteLine();
            }
        }
        PauseAndReturnToMenu();
    }

    static void EditTask()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Edit Task");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to edit.");
                PauseAndReturnToMenu();
                return;
            }

            ViewTasks();
            Console.Write("Enter the number of the task you want to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tasks.Count)
                throw new Exception("Invalid task number.");

            var task = tasks[index - 1];

            Console.Write($"Enter new title (leave blank to keep '{task.Title}'): ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
                task.Title = newTitle;

            Console.Write($"Enter new description (leave blank to keep '{task.Description}'): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
                task.Description = newDescription;

            Console.Write($"Enter new start date (leave blank to keep '{task.StartDate:yyyy-MM-dd}'): ");
            string startDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(startDateInput) && DateTime.TryParse(startDateInput, out DateTime newStartDate))
                task.StartDate = newStartDate;

            Console.Write($"Enter new end date (leave blank to keep '{task.EndDate:yyyy-MM-dd}'): ");
            string endDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(endDateInput) && DateTime.TryParse(endDateInput, out DateTime newEndDate) && newEndDate >= task.StartDate)
                task.EndDate = newEndDate;

            Console.Write("Mark as completed? (y/n): ");
            string completed = Console.ReadLine();
            if (completed.ToLower() == "y")
                task.IsCompleted = true;

            Console.WriteLine("Task updated successfully!");
            PauseAndReturnToMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            PauseAndReturnToMenu();
        }
    }

    static void DeleteTask()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Delete Task");
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to delete.");
                PauseAndReturnToMenu();
                return;
            }

            ViewTasks();
            Console.Write("Enter the number of the task you want to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tasks.Count)
                throw new Exception("Invalid task number.");

            tasks.RemoveAt(index - 1);
            Console.WriteLine("Task deleted successfully!");
            PauseAndReturnToMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            PauseAndReturnToMenu();
        }
    }

    static void PauseAndReturnToMenu()
    {
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();
    }
}

class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; }
}
