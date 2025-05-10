namespace WithersAPI.DTO
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public ICollection<CharacterBasicDto> Characters { get; set; } = new List<CharacterBasicDto>();
    }
}
