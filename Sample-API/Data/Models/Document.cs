using SampleApi.Data.Contracts;
using MongoDB.Bson;
using System;

namespace SampleApi.Data.Models
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
