﻿namespace Api.Models
{
    public class ChatEntry
    {
        /// <summary>
        ///     The chat entry's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The user that sent the chat entry.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     The text of the chat entry
        /// </summary>
        public string Message { get; set; }
    }
}