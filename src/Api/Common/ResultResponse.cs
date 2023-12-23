namespace Api.Common;

public class ResultResponse<TData>
{
    public string Message { get; }
    public TData Data { get; }

    private ResultResponse(string message, TData data)
    {
        Message = message;
        Data = data;
    }

    public static ResultResponse<TData> Init(TData data, string message)
    {
        return new ResultResponse<TData>(message, data);
    }
}