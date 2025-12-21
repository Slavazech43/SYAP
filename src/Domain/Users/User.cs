using Domain.Users.ValueObjects;

namespace Domain.Users;

public sealed class User
{
    public Guid Id { get; }
    public UserEmail Email { get; private set; }
    public UserPhone Phone { get; private set; }

    private User(Guid id, UserEmail email, UserPhone phone)
    {
        Id = id;
        Email = email;
        Phone = phone;
    }

    public static User Create(Guid id, UserEmail email, UserPhone phone)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("UserId пустой.");

        return new User(id, email, phone);
    }

    public void ChangeEmail(UserEmail email) => Email = email;

    public void ChangePhone(UserPhone phone) => Phone = phone;
}
