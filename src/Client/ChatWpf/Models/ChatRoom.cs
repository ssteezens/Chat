﻿using System.Collections.Generic;

namespace ChatWpf.Models
{
    public class ChatRoom
	{
		/// <summary>
		///		Chat room public key.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Chat's display name.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		///     All the chat entry's for this chat pane.
		/// </summary>
		public IEnumerable<ChatMessage> ChatMessages { get; set; }

		/// <summary>
		///     All user's in this chat.
		/// </summary>
		public IEnumerable<User> Users { get; set; }
	}
}
