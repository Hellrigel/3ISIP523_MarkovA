using System.Reflection;

List<string> StudentNamesRegistry = new List<string>();
List<Student> StudentRoster = new List<Student>();
List<Teacher> FacultyMembers = new List<Teacher>();
List<Course> CourseCatalog = new List<Course>();

bool programRunning = true;
while (programRunning)
{
    Console.WriteLine("----------ГЛАВНОЕ МЕНЮ----------");
    Console.WriteLine("1 - Зарегистрировать студента");
    Console.WriteLine("2 - Показать список студентов");
    Console.WriteLine("3 - Добавить преподавателя");
    Console.WriteLine("4 - Показать список преподавателей");
    Console.WriteLine("5 - Создать новый курс");
    Console.WriteLine("6 - Показать все курсы");
    Console.WriteLine("7 - Найти курсы по имени студента");
    Console.WriteLine("8 - Отобразить полную базу данных");
    Console.WriteLine("0 - Завершить программу");

    int userChoice = Convert.ToInt32(Console.ReadLine());

    switch (userChoice)

   

class Course
{
    private int CourseId;
    public string CourseTitle;
    private string CourseDescription;
    private string InstructorName;
    public List<string> EnrolledStudents;

    public Course(int id, string title, string description, string instructor, List<string> students)
    {
        CourseId = id;
        CourseTitle = title;
        CourseDescription = description;
        InstructorName = instructor;
        EnrolledStudents = students;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Название: {CourseTitle}\nОписание: {CourseDescription}\nПреподаватель: {InstructorName}");
        Console.WriteLine("Зачисленные студенты:");
        foreach (var participant in EnrolledStudents)
        {
            Console.WriteLine(participant);
        }
    }
}

class Person
{
    private string FullName;
    private DateOnly BirthDate;
    private string Gender;

    public Person(string name, DateOnly dateOfBirth, string gender)
    {
        FullName = name;
        BirthDate = dateOfBirth;
        Gender = gender;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"ФИО: {FullName}\nДата рождения: {BirthDate}\nПол: {Gender}");
    }
}

class Teacher : Person
{
    private int TeacherId;
    private int ComputerExperience;

    public Teacher(int id, string name, DateOnly dateOfBirth, string gender, int experience)
        : base(name, dateOfBirth, gender)
    {
        TeacherId = id;
        ComputerExperience = experience;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("ПРЕПОДАВАТЕЛЬ");
        base.DisplayInfo();
        Console.WriteLine($"ID: {TeacherId}");
        Console.WriteLine($"Опыт работы с компьютером: {ComputerExperience} лет\n");
    }
}

class Student : Person
{
    private int StudentId;
    private int PCExperienceYears;
    private string HealthGroup;

    public Student(int id, string name, DateOnly dateOfBirth, string gender, int computerExperience, string healthGroup)
        : base(name, dateOfBirth, gender)
    {
        StudentId = id;
        PCExperienceYears = computerExperience;
        HealthGroup = healthGroup;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("СТУДЕНТ");
        base.DisplayInfo();
        Console.WriteLine($"ID студента: {StudentId}");
        Console.WriteLine($"Опыт работы с ПК: {PCExperienceYears} лет");
        Console.WriteLine($"Группа здоровья: {HealthGroup}\n");
    }
}