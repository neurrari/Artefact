namespace Artefact.API.Data.Dtos.Caretaker
{
    public class CaretakerReadModel
    {
        public int Id { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string? Middlename { get; set; }
        public string FullName => $"{Lastname} {Firstname} {Middlename ?? ""}".Trim();
    }
}
