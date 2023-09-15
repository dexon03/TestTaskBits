using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace TestTaskBits.Models;

public class User
{
    [Ignore]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool Married { get; set; }
    [Phone]
    public string Phone { get; set; }
    public decimal Salary { get; set; }
}