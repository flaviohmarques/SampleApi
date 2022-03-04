using SampleApi.Data.Models;
using System;
using System.Collections.Generic;

namespace SampleApi.Data.Entities
{
    [BsonCollection("input")]
    public class Input : Document
    {
        public string User { get; set; }
        public string Agreement { get; set; }
        public string Module { get; set; }
        public string Operation { get; set; }
        public DateTime DateInclusion { get; set; }
        public DateTime DateAnswer { get; set; }
        public Dictionary<string, object> EntryData { get; set; }
        public Dictionary<string, object> AnswerData { get; set; }

        public Input(string user, DateTime dateInclusion, string agreement, string module, string operation, Dictionary<string, object> entryData)
        {
            User = user;
            Agreement = agreement;
            DateInclusion = dateInclusion;
            Module = module;
            Operation = operation;
            EntryData = entryData;
        }
    }
}