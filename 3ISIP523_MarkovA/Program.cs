using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityManagement
{
    // Абстрактный базовый класс (абстракция) + общие поля для людей
    public abstract class Person
    {
        // Инкапсуляция: приватные поля и публичные свойства для их доступа
        private static int _nextId = 1;
        private readonly int _id;
        private string _name;
        private int _age;
        private string _contact;

        protected Person(string name, int age, string contact)
        {
            _id = _nextId++;
            Name = name;
            Age = age;
            Contact = contact;
        }

        public int Id => _id;
        public string Name { get => _name; protected set => _name = value ?? "Unknown"; }
        public int Age { get => _age; protected set => _age = Math.Max(0, value); }
        public string Contact { get => _contact; protected set => _contact = value ?? ""; }

        // Полиморфизм: разные классы могут переопределять представление
        public abstract string GetInfo();
        public override string ToString() => $"{GetType().Name} #{Id}: {Name}";
    }

    public class Student : Person
    {
        private readonly List<Course> _courses = new();

        public Student(string name, int age, string contact)
            : base(name, age, contact) { }

        // Чтение списка курсов (инкапсуляция списка)
        public IReadOnlyList<Course> Courses => _courses.AsReadOnly();

        internal void Enroll(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            if (!_courses.Contains(course))
            {
                _courses.Add(course);
            }
        }

        internal void Unenroll(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            _courses.Remove(course);
        }

        public override string GetInfo()
        {
            var courseNames = _courses.Count == 0 ? "Нет курсов" : string.Join(", ", _courses.Select(c => c.Title));
            return $"Студент #{Id}: {Name}, Возраст: {Age}, Контакт: {Contact}, Курсы: {courseNames}";
        }
    }

    public class Teacher : Person
    {
        private readonly List<Course> _teachingCourses = new();

        public Teacher(string name, int age, string contact)
            : base(name, age, contact) { }

        public IReadOnlyList<Course> TeachingCourses => _teachingCourses.AsReadOnly();

        internal void AssignCourse(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            if (!_teachingCourses.Contains(course))
            {
                _teachingCourses.Add(course);
            }
        }

        internal void RemoveCourse(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course));
            _teachingCourses.Remove(course);
        }

        public override string GetInfo()
        {
            var courseNames = _teachingCourses.Count == 0 ? "Нет курсов" : string.Join(", ", _teachingCourses.Select(c => c.Title));
            return $"Преподаватель #{Id}: {Name}, Возраст: {Age}, Контакт: {Contact}, Ведёт: {courseNames}";
        }
    }

    public class Course
    {
        private static int _nextId = 1;
        private readonly int _id;
        private string _title;
        private string _description;
        private Teacher _teacher;
        private readonly List<Student> _students = new();

        public Course(string title, string description = "")
        {
            _id = _nextId++;
            Title = title;
            Description = description;
        }

        public int Id => _id;
        public string Title { get => _title; set => _title = value ?? "Без названия"; }
        public string Description { get => _description; set => _description = value ?? ""; }
        public Teacher Teacher
        {
            get => _teacher;
            internal set
            {
                // при изменении учителя — правильно обновляем списки учителя
                if (_teacher == value) return;
                _teacher?.RemoveCourse(this);
                _teacher = value;
                _teacher?.AssignCourse(this);
            }
        }

        public IReadOnlyList<Student> Students => _students.AsReadOnly();

        internal void AddStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            if (!_students.Contains(student))
            {
                _students.Add(student);
            }
        }

        internal void RemoveStudent(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            _students.Remove(student);
        }

        public string GetInfo()
        {
            var teacherName = Teacher == null ? "Не назначен" : $"{Teacher.Name} (#{Teacher.Id})";
            var studentsInfo = Students.Count == 0 ? "Нет студентов" : string.Join(", ", Students.Select(s => $"{s.Name} (#{s.Id})"));
            return $"Курс #{Id}: {Title}\nОписание: {Description}\nПреподаватель: {teacherName}\nСтуденты: {studentsInfo}";
        }

        public override string ToString() => $"Курс #{Id}: {Title}";
    }

    // Менеджер системы — управление сущностями
    public class UniversityManager
    {
        private readonly List<Student> _students = new();
        private readonly List<Teacher> _teachers = new();
        private readonly List<Course> _courses = new();

        // Студенты
        public Student AddStudent(string name, int age, string contact)
        {
            var s = new Student(name, age, contact);
            _students.Add(s);
            return s;
        }
        public Student GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);
        public IReadOnlyList<Student> GetAllStudents() => _students.AsReadOnly();

        // Преподаватели
        public Teacher AddTeacher(string name, int age, string contact)
        {
            var t = new Teacher(name, age, contact);
            _teachers.Add(t);
            return t;
        }
        public Teacher GetTeacherById(int id) => _teachers.FirstOrDefault(t => t.Id == id);
        public IReadOnlyList<Teacher> GetAllTeachers() => _teachers.AsReadOnly();

        // Курсы
        public Course AddCourse(string title, string desc = "")
        {
            var c = new Course(title, desc);
            _courses.Add(c);
            return c;
        }
        public Course GetCourseById(int id) => _courses.FirstOrDefault(c => c.Id == id);
        public IReadOnlyList<Course> GetAllCourses() => _courses.AsReadOnly();

        // Запись студента на курс
        public bool EnrollStudentToCourse(int studentId, int courseId)
        {
            var s = GetStudentById(studentId);
            var c = GetCourseById(courseId);
            if (s == null || c == null) return false;
            // обновляем обе стороны связи
            c.AddStudent(s);
            s.Enroll(c);
            return true;
        }

        // Отписать
        public bool UnenrollStudentFromCourse(int studentId, int courseId)
        {
            var s = GetStudentById(studentId);
            var c = GetCourseById(courseId);
            if (s == null || c == null) return false;
            c.RemoveStudent(s);
            s.Unenroll(c);
            return true;
        }

        // Назначить преподавателя на курс
        public bool AssignTeacherToCourse(int teacherId, int courseId)
        {
            var t = GetTeacherById(teacherId);
            var c = GetCourseById(courseId);
            if (t == null || c == null) return false;
            c.Teacher = t;
            return true;
        }

        // Удобные методы просмотра
        public IEnumerable<Course> GetCoursesForStudent(int studentId)
        {
            var s = GetStudentById(studentId);
            return s?.Courses ?? Enumerable.Empty<Course>();
        }

        public IEnumerable<Student> GetStudentsForCourse(int courseId)
        {
            var c = GetCourseById(courseId);
            return c?.Students ?? Enumerable.Empty<Student>();
        }
    }

    internal class Program
    {
        private static UniversityManager _um = new();

        private static void Main()
        {
            SeedSampleData();

            while (true)
            {
                ShowMainMenu();
                var choice = ReadInt("Выберите пункт: ");
                Console.WriteLine();
                switch (choice)
                {
                    case 0: return;
                    case 1: AddStudentMenu(); break;
                    case 2: ListStudents(); break;
                    case 3: ViewStudentDetails(); break;
                    case 4: AddTeacherMenu(); break;
                    case 5: ListTeachers(); break;
                    case 6: ViewTeacherDetails(); break;
                    case 7: AddCourseMenu(); break;
                    case 8: ListCourses(); break;
                    case 9: ViewCourseDetails(); break;
                    case 10: EnrollStudentToCourseMenu(); break;
                    case 11: ListStudentCoursesMenu(); break;
                    case 12: ListStudentsInCourseMenu(); break;
                    case 13: AssignTeacherToCourseMenu(); break;
                    case 14: UnenrollStudentFromCourseMenu(); break;
                    default:
                        Console.WriteLine("Неверный пункт.");
                        break;
                }
                Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
                Console.ReadLine();
            }
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Система управления университетом ===");
            Console.WriteLine("0. Выход");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Список всех студентов");
            Console.WriteLine("3. Просмотреть информацию о студенте");
            Console.WriteLine("4. Добавить преподавателя");
            Console.WriteLine("5. Список всех преподавателей");
            Console.WriteLine("6. Просмотреть информацию о преподавателе");
            Console.WriteLine("7. Создать курс");
            Console.WriteLine("8. Список всех курсов");
            Console.WriteLine("9. Просмотреть информацию о курсе");
            Console.WriteLine("10. Записать студента на курс");
            Console.WriteLine("11. Показать курсы студента");
            Console.WriteLine("12. Показать студентов на курсе");
            Console.WriteLine("13. Назначить преподавателя на курс");
            Console.WriteLine("14. Отписать студента от курса");
            Console.WriteLine();
        }

        // Меню и операции
        private static void AddStudentMenu()
        {
            Console.WriteLine("=== Добавление студента ===");
            var name = ReadString("Имя: ");
            var age = ReadInt("Возраст: ");
            var contact = ReadString("Контакт (email/телефон): ");
            var s = _um.AddStudent(name, age, contact);
            Console.WriteLine($"Добавлен студент: {s.GetInfo()}");
        }

        private static void ListStudents()
        {
            Console.WriteLine("=== Все студенты ===");
            var students = _um.GetAllStudents();
            if (!students.Any()) { Console.WriteLine("Список пуст."); return; }
            foreach (var s in students) Console.WriteLine(s.GetInfo());
        }

        private static void ViewStudentDetails()
        {
            Console.WriteLine("=== Информация о студенте ===");
            var id = ReadInt("ID студента: ");
            var s = _um.GetStudentById(id);
            if (s == null) Console.WriteLine("Студент не найден.");
            else Console.WriteLine(s.GetInfo());
        }

        private static void AddTeacherMenu()
        {
            Console.WriteLine("=== Добавление преподавателя ===");
            var name = ReadString("Имя: ");
            var age = ReadInt("Возраст: ");
            var contact = ReadString("Контакт: ");
            var t = _um.AddTeacher(name, age, contact);
            Console.WriteLine($"Добавлен преподаватель: {t.GetInfo()}");
        }

        private static void ListTeachers()
        {
            Console.WriteLine("=== Все преподаватели ===");
            var teachers = _um.GetAllTeachers();
            if (!teachers.Any()) { Console.WriteLine("Список пуст."); return; }
            foreach (var t in teachers) Console.WriteLine(t.GetInfo());
        }

        private static void ViewTeacherDetails()
        {
            Console.WriteLine("=== Информация о преподавателе ===");
            var id = ReadInt("ID преподавателя: ");
            var t = _um.GetTeacherById(id);
            if (t == null) Console.WriteLine("Преподаватель не найден.");
            else Console.WriteLine(t.GetInfo());
        }

        private static void AddCourseMenu()
        {
            Console.WriteLine("=== Создание курса ===");
            var title = ReadString("Название курса: ");
            var desc = ReadString("Краткое описание: ");
            var c = _um.AddCourse(title, desc);
            Console.WriteLine($"Создан {c}");
        }

        private static void ListCourses()
        {
            Console.WriteLine("=== Все курсы ===");
            var courses = _um.GetAllCourses();
            if (!courses.Any()) { Console.WriteLine("Список пуст."); return; }
            foreach (var c in courses) Console.WriteLine(c.GetInfo());
        }

        private static void ViewCourseDetails()
        {
            Console.WriteLine("=== Информация о курсе ===");
            var id = ReadInt("ID курса: ");
            var c = _um.GetCourseById(id);
            if (c == null) Console.WriteLine("Курс не найден.");
            else Console.WriteLine(c.GetInfo());
        }

        private static void EnrollStudentToCourseMenu()
        {
            Console.WriteLine("=== Записать студента на курс ===");
            var sid = ReadInt("ID студента: ");
            var cid = ReadInt("ID курса: ");
            var ok = _um.EnrollStudentToCourse(sid, cid);
            Console.WriteLine(ok ? "Студент успешно записан." : "Ошибка: студент или курс не найдены.");
        }

        private static void UnenrollStudentFromCourseMenu()
        {
            Console.WriteLine("=== Отписать студента от курса ===");
            var sid = ReadInt("ID студента: ");
            var cid = ReadInt("ID курса: ");
            var ok = _um.UnenrollStudentFromCourse(sid, cid);
            Console.WriteLine(ok ? "Студент отписан." : "Ошибка: студент или курс не найдены.");
        }

        private static void ListStudentCoursesMenu()
        {
            Console.WriteLine("=== Курсы студента ===");
            var sid = ReadInt("ID студента: ");
            var courses = _um.GetCoursesForStudent(sid).ToList();
            if (!courses.Any()) Console.WriteLine("Курсов нет или студент не найден.");
            else foreach (var c in courses) Console.WriteLine(c.GetInfo());
        }

        private static void ListStudentsInCourseMenu()
        {
            Console.WriteLine("=== Студенты на курсе ===");
            var cid = ReadInt("ID курса: ");
            var students = _um.GetStudentsForCourse(cid).ToList();
            if (!students.Any()) Console.WriteLine("Студентов нет или курс не найден.");
            else foreach (var s in students) Console.WriteLine(s.GetInfo());
        }

        private static void AssignTeacherToCourseMenu()
        {
            Console.WriteLine("=== Назначить преподавателя на курс ===");
            var tid = ReadInt("ID преподавателя: ");
            var cid = ReadInt("ID курса: ");
            var ok = _um.AssignTeacherToCourse(tid, cid);
            Console.WriteLine(ok ? "Преподаватель назначен." : "Ошибка: преподаватель или курс не найдены.");
        }

        // Вспомогательные методы ввода
        private static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        private static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                var s = Console.ReadLine();
                if (int.TryParse(s, out var v)) return v;
                Console.Write("Неверный ввод. Введите число: ");
            }
        }

        // Небольшая выборка для старта
        private static void SeedSampleData()
        {
            var s1 = _um.AddStudent("Иван Иванов", 20, "ivan@example.com");
            var s2 = _um.AddStudent("Мария Петрова", 22, "maria@example.com");

            var t1 = _um.AddTeacher("Алексей Смирнов", 40, "asmirnov@uni.edu");
            var t2 = _um.AddTeacher("Ольга Кузнецова", 35, "okuzn@uni.edu");

            var c1 = _um.AddCourse("Программирование на C#", "Введение в C#, основы ООП");
            var c2 = _um.AddCourse("Алгоритмы", "Базовые алгоритмы и структуры данных");

            _um.AssignTeacherToCourse(t1.Id, c1.Id);
            _um.AssignTeacherToCourse(t2.Id, c2.Id);

            _um.EnrollStudentToCourse(s1.Id, c1.Id);
            _um.EnrollStudentToCourse(s2.Id, c1.Id);
            _um.EnrollStudentToCourse(s2.Id, c2.Id);
        }
    }
}
