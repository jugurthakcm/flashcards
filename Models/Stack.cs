namespace Models
{
    public class Stack
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Flashcard> Flashcards { get; set; }
    }
}
