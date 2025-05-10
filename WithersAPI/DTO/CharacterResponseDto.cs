namespace WithersAPI.DTO
{
    public class CharacterResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int Level { get; set; }
        public int UserId { get; set; }
    }
}
