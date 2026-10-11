using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt_lab3_mvc_liseychikov.Models;
using TaskModel = wt_lab3_mvc_liseychikov.Models.TaskModel;

namespace wt_lab3_mvc_liseychikov.Views
{
    public class TaskView
    {
        public void ShowTasks(List<TaskModel> tasks)
        {
            Console.WriteLine("=== СПИСОК ЗАДАЧ ===");
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id} " +
                    $"{task.Name} | " + $"{task.Category} | " + $"{task.Price} | " + $"Доступен: {task.IsAvaliable}");
            }
        }

        public void ShowTask(TaskModel task)
        {
            Console.WriteLine("=== ЗАДАНИЕ ===");
            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"Название: {task.Name}");
            Console.WriteLine($"Категория: {task.Category}");
            Console.WriteLine($"Цена: {task.Price}");
            Console.WriteLine($"Доступен: {task.IsAvaliable}");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}