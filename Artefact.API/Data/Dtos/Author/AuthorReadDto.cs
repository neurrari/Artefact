namespace Artefact.API.Data.Dtos.Author
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public DateTime DateBirth { get; set; }
        public DateTime? DateDeath { get; set; }
        public string FullName => $"{Lastname} {Firstname} {Middlename}".Trim();
    }
}
