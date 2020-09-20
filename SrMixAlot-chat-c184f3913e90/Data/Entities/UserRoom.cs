namespace Data.Entities
{
    /// <summary>
    ///     Relational table between users and chat rooms.
    /// </summary>
    public class UserRoom
    {
        /// <summary>
        ///     The id for a user.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        ///     The id for a chat room.
        /// </summary>
        public int ChatRoomId { get; set; }

        /// <summary>
        ///     Determines if user is an admin of the room.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     The user relational entity.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     The chat room relational entity.
        /// </summary>
        public ChatRoom ChatRoom { get; set; }
    }
}
