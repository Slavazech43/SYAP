using Domain.Students;
using Domain.Courses.ValueObjects;
using Domain.Students.ValueObjects;

namespace Domain.Students;

public readonly record struct CourseProgressItem(
    LessonId LessonId,
    ProgressPercent Percent,
    bool IsCompleted
);

public readonly record struct CourseAttemptItem(
    AttemptId AttemptId,
    TestId TestId,
    AttemptStatus Status,
    ScorePercent? Score
);
