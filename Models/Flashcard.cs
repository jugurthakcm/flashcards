namespace Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }

        // Foreign Key
        public int StackId { get; set; }

        //Navigation Property to access a Stack from FlashCards
        public Stack Stack { get; set; }
    }
}
