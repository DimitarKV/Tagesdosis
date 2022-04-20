namespace Tagesdosis.Domain.Types;

public class ApiResponse
{
    public List<string>? Errors { get; init; }

    public ApiResponse(IEnumerable<string>? errors = null)
    {
        Errors = errors?.ToList();
    }

    public bool IsValid => Errors == null || !Errors.Any();
}

public class ApiResponse<TModel> : ApiResponse
{
    public TModel Result { get; init; }
    
    public ApiResponse(TModel model, IEnumerable<string>? errors = null) 
        : base(errors)
    {
        Result = model;
    }
}