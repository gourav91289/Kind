//using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OmniPot.Data;
using Microsoft.AspNetCore.Mvc;
using OmniPot.Common;
using OmniPot.Data.Models;
using System.Collections.Concurrent;

namespace OmniPot.Services
{
    //public class AppConnection : PersistentConnection
    //{
    //    private readonly KindDbContext dbContext;
    //    private readonly IUserContext userContext;
    //    //concurrent dict that stores a list of userid's and their connections
    //    private readonly ConcurrentDictionary<string, List<string>> users = new ConcurrentDictionary<string, List<string>>();
    //    //concurrent dict that stores a list of connectionid's and their users.
    //    private readonly ConcurrentDictionary<string, List<string>> clients = new ConcurrentDictionary<string, List<string>>(); 
    //    //TODO: inmemory backing for sending messages without going to the db.
    //    public AppConnection(KindDbContext context, IUserContext userContext)
    //    {
    //        this.dbContext = context;
    //        this.userContext = userContext; 
    //    }

    //    protected override bool AuthorizeRequest(HttpRequest request)
    //    {
    //        return request.HttpContext.User != null && request.HttpContext.User.Identity != null && request.HttpContext.User.Identity.IsAuthenticated;
    //    }

    //    protected override Task OnConnected(HttpRequest request, string connectionId)
    //    {
    //        ApplySignalGroups(request, connectionId);

    //        if (!dbContext.Connections.Any(c => c.AppConnectionId == Guid.Parse(connectionId)))
    //        {
    //            dbContext.Connections.Add(new Data.Models.AppConnection
    //            {
    //                AppConnectionId = Guid.Parse(connectionId),
    //                ConnectionId = Guid.Parse(connectionId),
    //                State = TrackableEntityState.IsActive,
    //                Username = request.HttpContext.User.Identity.Name,
    //                UserId = userContext.UserId,
    //                //this connection may not have those locations necessarily.
    //                Location = "location",
    //                Tenant = "tenant"
    //            });
    //            dbContext.SaveChanges();
    //        }

    //        clients[connectionId].Add(userContext.UserId.ToString());
    //        users[userContext.UserId.ToString()].Add(connectionId);

    //        return base.OnConnected(request, connectionId);
    //    }

    //    private void ApplySignalGroups(HttpRequest request, string connectionId)
    //    {
    //        //maintain the backing state lists

    //    }

    //    protected override Task OnDisconnected(HttpRequest request, string connectionId, bool stopCalled)
    //    {
    //        //cleanup the connection
    //        var con = dbContext.Connections.Single(c => c.AppConnectionId == Guid.Parse(connectionId)); 
    //        if (null != con)
    //        {
    //            con.State = TrackableEntityState.IsDeleted;
    //            dbContext.SaveChanges();
    //        }
    //        return base.OnDisconnected(request, connectionId, stopCalled);
    //    }

    //    protected override Task OnReceived(HttpRequest request, string connectionId, string data)
    //    {
    //        var message = JsonConvert.DeserializeObject<AppMessage>(data);
    //        message.State = TrackableEntityState.IsActive;
    //        dbContext.Messages.Add(message);
    //        dbContext.SaveChanges();
    //        //TODO: Determine how to route the request. 
    //        switch(message.MessageType)
    //        {
    //            case MessageType.BroadcastAll:
    //                Connection.Broadcast(message.Content);
    //                break;
    //            case MessageType.ToConnection:
    //                Connection.Send(message.ConnectionId.ToString(), message.Content);
    //                break;
    //            case MessageType.ToUserId:
    //            case MessageType.ToUsername:
                    
    //                //Send the message to any connections that mach this user/userid
    //                break;
    //            case MessageType.BroadcastTenant:
    //                //Broadcast the message to the tenant group. 
    //                break;
    //            case MessageType.BroadcastLocation:
    //                //broadcast the message to the location group. 
    //                break; 
    //            case MessageType.None:
    //                throw new InvalidOperationException("Oooops. You tested a throw.");
                    
    //        }

    //        return base.OnReceived(request, connectionId, data);
    //    }
    //}
}
