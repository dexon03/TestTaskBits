using TestTaskBits.Models;

namespace TestTaskBits.Extensions;

public static class Mapper
{
    public static User Map(this User user, User updatedUser)
    {
        user.Married = updatedUser.Married;
        user.Name = updatedUser.Name;
        user.DateOfBirth = updatedUser.DateOfBirth;
        user.Salary = updatedUser.Salary;
        user.Phone = updatedUser.Phone;
        return user;
    }
}