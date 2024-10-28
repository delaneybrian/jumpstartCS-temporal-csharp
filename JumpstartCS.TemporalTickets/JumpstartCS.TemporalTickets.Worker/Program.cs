using JumpstartCS.TemporalTickets.Activities;
using JumpstartCS.TemporalTickets.Workflows;
using Temporalio.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddHostedTemporalWorker("localhost:7233", "default", "ticket-purchase-task-queue")
    .AddScopedActivities<TicketPurchaseActivities>()
    .AddScopedActivities<PaymentActivities>()
    .AddWorkflow<PurchaseTicketsWorkflow>();

var host = builder.Build();
host.Run();
