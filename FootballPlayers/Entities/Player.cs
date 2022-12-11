namespace FootballPlayers.Entities;

public class Player : EntityBase
{
    public string Name { get; set; } = "";

    public int Age { get; set; }

    public float Height { get; set; }

    public int TeamId { get; set; }

    public Team? Team { get; set; }
}