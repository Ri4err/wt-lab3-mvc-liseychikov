using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using wt_lab3_mvc_liseychikov.Views;
using TaskModel = wt_lab3_mvc_liseychikov.Models.TaskModel;

namespace wt_lab3_mvc_liseychikov.Controllers
{
    public class TaskController
    {
        private readonly List<TaskModel> tasks;
        private readonly TaskView view;

        public TaskController()
        {
            tasks = new List<TaskModel>
            {
                new TaskModel
                {
                    Id = 1,
                    Name = "Разработка приложения",
                    Category = "Разработка",
                    Price = 5000,
                    IsAvaliable = true
                },

                new TaskModel
                {
                    Id = 2,
                    Name = "Оптимизация ПО",
                    Category = "Оптимизация",
                    Price = 3500,
                    IsAvaliable = false
                },

                new TaskModel
                {
                    Id = 3,
                    Name = "Проверка кода на наличие ошибок",
                    Category = "Проверка",
                    Price = 2000,
                    IsAvaliable = true
                }
            };
            view = new TaskView();
        }

        public void Index()
        {
            view.ShowTasks(tasks);
        }

        public void Show(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                view.ShowMessage("Задача не найдена");
                return;
            }
            view.ShowTask(task);
        }

        public void Add(TaskModel task)
        {
            tasks.Add(task);
            view.ShowMessage("Задача успешно добавлена");
        }

        public void Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                view.ShowMessage("Задача не найдена");
                return;
            }
            tasks.Remove(task);
            view.ShowMessage("Задача успешно удалена");
        }

        public void Run()
        {
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("=== МЕНЮ ===");
                Console.WriteLine("1. Показать задачи");
                Console.WriteLine("2. Найти задачу");
                Console.WriteLine("3. Добавить задачу");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Изменить задачу");
                Console.WriteLine("0. Выход");

                Console.WriteLine("Выберите действие: ");
                var command=Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Index();
                        break;

                    case "2":
                        ShowTaskByUserInput();
                        break;

                    case "3":
                        AddTaskByUserInput();
                        break;

                    case "4":
                        DeleteTaskByUserInput();
                        break;

                    case "5":
                        ChangeTaskByUserInput();
                        break;

                    case "0":
                        return;

                    default:
                        view.ShowMessage("Неизвестная команда");
                        break;
                }
            }
        }
        private void ShowTaskByUserInput()
        {
            Console.Write("Введите ID задачи: ");
            if (!int.TryParse(
            Console.ReadLine(),
            out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }
            Show(id);
        }

        private void AddTaskByUserInput()
        {
            Console.Write("Введите ID задачи: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }

            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Введите категорию: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Введите цену: ");
            if (!int.TryParse(Console.ReadLine(), out int price))
            {
                view.ShowMessage("Некорректная цена.");
                return;
            }

            Console.Write("Доступен ли (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isAvailable))
            {
                view.ShowMessage("Некорректное значение доступности.");
                return;
            }

            TaskModel newTask = new TaskModel
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price,
                IsAvaliable = isAvailable
            };

            Add(newTask);
        }

        private void DeleteTaskByUserInput()
        {
            Console.Write("Введите ID задачи для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }

            Delete(id); 
        }

        private void ChangeTaskByUserInput()
        {
            Console.Write("Введите ID задачи для изменения: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }

            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                view.ShowMessage("Задача не найдена.");
                return;
            }

            Console.Write($"Введите новое название (текущее: {task.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
            {
                task.Name = name;
            }

            Console.Write($"Введите новую категорию (текущая: {task.Category}): ");
            string category = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(category))
            {
                task.Category = category;
            }

            Console.Write($"Введите новую цену (текущая: {task.Price}): ");
            string priceInput = Console.ReadLine();
            if (int.TryParse(priceInput, out int price))
            {
                task.Price = price;
            }

            Console.Write($"Доступен ли (true/false) (текущее: {task.IsAvaliable}): ");
            string availableInput = Console.ReadLine();
            if (bool.TryParse(availableInput, out bool isAvailable))
            {
                task.IsAvaliable = isAvailable;
            }

            view.ShowMessage("Задача успешно обновлена!");
        }
    }
}