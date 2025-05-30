using System;
using System.Collections.Generic;

namespace TaskManagerApp
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Completed
    }

    public class Task
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TaskStatus Status { get; set; }

        public Task(string id, string name, string description, DateTime date)
        {
            TaskId = id;
            TaskName = name;
            Description = description;
            Date = date;
            Status = TaskStatus.ToDo;
        }

        public override string ToString()
        {
            return $"{TaskId} - {TaskName} | {Status} | {Date.ToShortDateString()}";
        }
    }

    class Program
    {
        static LinkedList<Task> ToDoList = new LinkedList<Task>();
        static Stack<Task> InProgressList = new Stack<Task>();
        static Queue<Task> CompletedList = new Queue<Task>();

        static void Main(string[] args)
        {
            
            InsertTask(new Task("T001", "Design UI", "Create wireframes", new DateTime(2025, 6, 10)));
            InsertTask(new Task("T002", "Setup DB", "Initialize database", new DateTime(2025, 6, 5)));
            InsertTask(new Task("T003", "Write Docs", "Documentation work", new DateTime(2025, 6, 15)));

            Console.WriteLine("Initial To-Do List:");
            DisplayList(ToDoList);

            MoveToInProgress("T002");

            Console.WriteLine("\nTo-Do List after moving T002 to In Progress:");
            DisplayList(ToDoList);

            Console.WriteLine("\nIn-Progress List:");
            DisplayStack(InProgressList);

          
            MoveToCompleted();

            Console.WriteLine("\nIn-Progress List after completion:");
            DisplayStack(InProgressList);

            Console.WriteLine("\nCompleted List:");
            DisplayQueue(CompletedList);
        }

       
        static void InsertTask(Task task)
        {
            if (ToDoList.Count == 0)
            {
                ToDoList.AddFirst(task);
                return;
            }

            var current = ToDoList.First;
            while (current != null && current.Value.Date <= task.Date)
            {
                current = current.Next;
            }

            if (current == null)
                ToDoList.AddLast(task);
            else
                ToDoList.AddBefore(current, task);
        }

       
        static void MoveToInProgress(string taskId)
        {
            var node = ToDoList.First;
            while (node != null)
            {
                if (node.Value.TaskId == taskId)
                {
                    var task = node.Value;
                    task.Status = TaskStatus.InProgress;
                    InProgressList.Push(task);
                    ToDoList.Remove(node);
                    break;
                }
                node = node.Next;
            }
        }

   
        static void MoveToCompleted()
        {
            if (InProgressList.Count > 0)
            {
                var task = InProgressList.Pop();
                task.Status = TaskStatus.Completed;
                CompletedList.Enqueue(task);
            }
        }

   
        static void DisplayList(LinkedList<Task> list)
        {
            foreach (var task in list)
                Console.WriteLine(task);
        }

        static void DisplayStack(Stack<Task> stack)
        {
            foreach (var task in stack)
                Console.WriteLine(task);
        }

        static void DisplayQueue(Queue<Task> queue)
        {
            foreach (var task in queue)
                Console.WriteLine(task);
        }
    }
}

