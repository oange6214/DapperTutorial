using System.ComponentModel.DataAnnotations;

namespace DapperCrudTutorial.Models;

public class SuperHero
{
	[Key]
	public int Id { get; set; }

	public string Name { get; set; } = null!;

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public string Place { get; set; } = null!;

}
