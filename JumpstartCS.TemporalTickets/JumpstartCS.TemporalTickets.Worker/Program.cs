using JumpstartCS.TemporalTickets.Activities;
using JumpstartCS.TemporalTickets.Definitions.Configuration;
using JumpstartCS.TemporalTickets.Infrastructure;
using JumpstartCS.TemporalTickets.Interfaces;
using JumpstartCS.TemporalTickets.Workflows;
using Temporalio.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .Configure<InMemoryTicketRepositoryOptions>(builder.Configuration.GetSection("InMemoryTicketRepository"));
builder.Services
    .AddSingleton<ITicketRepository, InMemoryTicketRepository>();

builder.Services
    .Configure<StripePaymentGatewayOptions>(builder.Configuration.GetSection("StripePaymentGateway"));
builder.Services
    .AddSingleton<IPaymentGateway, StripePaymentGateway>();

builder.Services
    .Configure<SmsNotificationServiceOptions>(builder.Configuration.GetSection("SmsNotificationServic"));
builder.Services
    .AddSingleton<INotificationService, SmsNotificationService>();

builder.Services
    .AddHostedTemporalWorker("localhost:7233", "default", "ticket-purchase-task-queue")
    .AddScopedActivities<TicketPurchaseActivities>()
    .AddScopedActivities<PaymentActivities>()
    .AddScopedActivities<NotificationActivities>()
    .AddWorkflow<PurchaseTicketsWorkflow>();

var host = builder.Build();
host.Run();
