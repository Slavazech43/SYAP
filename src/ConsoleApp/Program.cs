using Domain.Common;
using Domain.Courses;
using Domain.Courses.ValueObjects;
using Domain.Students;
using Domain.Students.ValueObjects;

static void PrintCourse(Course course)
{
    Console.WriteLine("=== COURSE ===");
    Console.WriteLine("Id: " + course.Id);
    Console.WriteLine("Title: " + course.Title);
    Console.WriteLine("Description: " + course.Description);
    Console.WriteLine("Price: " + course.Price);
    Console.WriteLine("Status: " + course.Status);
    Console.WriteLine("Modules: " + course.Modules.Count);

    for (int i = 0; i < course.Modules.Count; i++)
    {
        var m = course.Modules[i];
        Console.WriteLine($"  [{i + 1}] Module: {m.Title} ({m.Id}) Lessons: {m.Lessons.Count}");

        for (int j = 0; j < m.Lessons.Count; j++)
        {
            var l = m.Lessons[j];
            var hasTest = l.Test != null;
            Console.WriteLine($"      - Lesson: {l.Title} ({l.Id}), Test: {(hasTest ? "YES" : "NO")}");

            if (hasTest)
            {
                Console.WriteLine($"        TestId: {l.Test!.Id}, Questions: {l.Test.Questions.Count}");
            }
        }
    }

    Console.WriteLine();
}

static void PrintStudent(Student student)
{
    Console.WriteLine("=== STUDENT ===");
    Console.WriteLine("Id: " + student.Id);
    Console.WriteLine("Name: " + student.Name);
    Console.WriteLine("Email: " + student.Email);

    Console.WriteLine("Enrollments: " + student.Enrollments.Count);
    for (int i = 0; i < student.Enrollments.Count; i++)
    {
        var e = student.Enrollments[i];
        Console.WriteLine($"  [{i + 1}] CourseId: {e.CourseId}, EnrolledAt(UTC): {e.EnrolledAtUtc}");
    }

    Console.WriteLine();
}

static void PrintStudentDetails(Student student, CourseId courseId)
{
    Console.WriteLine("=== STUDENT DETAILS ===");
    Console.WriteLine("Course: " + courseId);

    var progress = student.GetProgressForCourse(courseId);
    Console.WriteLine("  Progress:");
    if (progress.Count == 0)
    {
        Console.WriteLine("    (no progress yet)");
    }
    else
    {
        for (int i = 0; i < progress.Count; i++)
        {
            var p = progress[i];
            Console.WriteLine($"    LessonId: {p.LessonId}, Progress: {p.Percent}, Completed: {p.IsCompleted}");
        }
    }

    var attempts = student.GetAttemptsForCourse(courseId);
    Console.WriteLine("  Test attempts:");
    if (attempts.Count == 0)
    {
        Console.WriteLine("    (no attempts)");
    }
    else
    {
        for (int i = 0; i < attempts.Count; i++)
        {
            var a = attempts[i];
            Console.WriteLine($"    AttemptId: {a.AttemptId}, TestId: {a.TestId}, Status: {a.Status}, Score: {a.Score}");
        }
    }

    Console.WriteLine();
}

try
{
    // 1) Курс
    var course = Course.Create(
        CourseId.Create(Guid.NewGuid()),
        CourseTitle.Create("C# ООП: Доменные модели"),
        CourseDescription.Create("Курс про агрегаты, сущности и объекты значения."),
        Money.Create(1990m, "RUB")
    );

    // 2) Структура курса
    var module1 = Module.Create(
        ModuleId.Create(Guid.NewGuid()),
        ModuleTitle.Create("Модуль 1 — Основы")
    );

    var lesson1 = Lesson.Create(
        LessonId.Create(Guid.NewGuid()),
        LessonTitle.Create("Урок 1 — Value Object и Entity")
    );

    var test1 = Test.Create(TestId.Create(Guid.NewGuid()));
    test1.AddQuestion(Question.Create(
        QuestionId.Create(Guid.NewGuid()),
        QuestionText.Create("Что такое Value Object?"),
        AnswerText.Create("Объект, сравниваемый по значению")
    ));

    lesson1.AttachTest(test1);
    module1.AddLesson(lesson1);
    course.AddModule(module1);

    // 3) Студент + зачисление + прогресс + попытка
    var student = Student.Create(
        StudentId.Create(Guid.NewGuid()),
        PersonName.Create("Slava"),
        Email.Create("slava@example.com")
    );

    student.Enroll(course.Id);
    student.UpdateLessonProgress(course.Id, lesson1.Id, ProgressPercent.Create(50));
    var attemptId = student.StartTestAttempt(course.Id, test1.Id);
    student.FinishTestAttempt(course.Id, attemptId, ScorePercent.Create(100));

    // 4) Вывод
    PrintCourse(course);

    PrintStudent(student);
    PrintStudentDetails(student, course.Id);

    // 5) Публикация курса
    course.Publish();
    Console.WriteLine("OK: курс опубликован.");
    PrintCourse(course);
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.GetType().Name);
    Console.WriteLine(ex.Message);
}
