using System.Text.Json;
using Application.UseCases.Commands;
using cShop.Infrastructure.Hub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace WebApi.Hub;

public class CommentHub : HubBase
{
    private readonly ILogger<CommentHub> _logger;
    private readonly ISender _mediator;

    public CommentHub(ILogger<CommentHub> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var groupId = httpContext?.Request.Query["group-id"].ToString();
        if (!string.IsNullOrEmpty(groupId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
        await base.OnConnectedAsync();
    }
    public async Task SendMessage(string message)
    {
        _logger.LogInformation(message);
        var groupId = Context.GetHttpContext()?.Request.Query["group-id"].ToString();
        var result = await _mediator.Send(new CreateCommentCommand(Guid.Parse(groupId) , message));
        await Clients.Group(groupId).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(result));
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}