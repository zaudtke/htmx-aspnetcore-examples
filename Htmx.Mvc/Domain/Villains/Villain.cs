namespace Htmx.Mvc.Domain.Villains;

public class Villain
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Movie { get; set; } = string.Empty;
	public string Status { get; set; } = "Alive";

}
