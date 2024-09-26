using Domain.Intefaces;
using MongoDB.Bson;

namespace Domain.Entities;

public abstract class Document : IDocument
{
    public ObjectId Id { get; set; }

    public DateTime CreatedAt => Id.CreationTime;
}