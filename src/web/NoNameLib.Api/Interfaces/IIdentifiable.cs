namespace NoNameLib.Api.Interfaces;

public interface IIdentifiable
{
    object Id { get; set; }
}

public interface ICreateModel : IIdentifiable { }
public interface IEditModel : IIdentifiable, IEditable { }
public interface IDeleteModel : IIdentifiable { }