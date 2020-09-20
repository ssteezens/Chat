namespace Shared.Models.Models
{
    /// <summary>
    ///     Model for a user to chat room relation.
    /// </summary>
    public class UserRoomModel
    {
        /// <summary>
        ///     The user of the relation.
        /// </summary>
        public UserModel User { get; set; }

        /// <summary>
        ///     The chat room of the relation.
        /// </summary>
        public ChatRoomModel ChatRoom { get; set; }

        /// <summary>
        ///     The type of operation that is performed.
        /// </summary>
        public MessageOperationTypes OperationType { get; set; }
    }
}
