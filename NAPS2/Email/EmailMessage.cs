﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAPS2.Email
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            Recipients = new List<EmailRecipient>();
            AttachmentFilePaths = new List<string>();
        }

        public string Subject { get; set; }

        public string BodyText { get; set; }

        public List<EmailRecipient> Recipients { get; set; }

        public List<string> AttachmentFilePaths { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email should be sent automatically without prompting the user to make changes first.
        /// </summary>
        public bool AutoSend { get; set; }

        /// <summary>>
        /// Gets or sets a value indicating whether, if AutoSend is true, the mail should be sent without prompting the user for credentials when necessary.
        /// This may result in an authorization error.
        /// </summary>
        public bool SilentSend { get; set; }
    }
}
