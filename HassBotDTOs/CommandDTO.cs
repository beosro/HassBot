///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : CommandDTO.cs
//  DESCRIPTION     : A DTO class that holds command related information
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace HassBotDTOs {
    public class CommandDTO {
        public string CommandName { get; set; }
        public string CommandData { get; set; }
        public string CommandAuthor { get; set; }
        public DateTime CommandCreatedDate { get; set; }
        public int CommandCount { get; set; }
    }

    public class CommandDTOComparer : IEqualityComparer<CommandDTO> {
        public bool Equals(CommandDTO x, CommandDTO y) {
            if (x.CommandName == y.CommandName)
                return true;
            else
                return false;
        }

        public int GetHashCode(CommandDTO obj) {
            return base.GetHashCode();
        }
    }
}