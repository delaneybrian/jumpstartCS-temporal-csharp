using Temporalio.Workflows;

namespace JumpstartCS.TemporalTickets.Workflows
{
    [Workflow]
    public class PurchaseTicketsWorkflow
    {
        [WorkflowInit]
        public PurchaseTicketsWorkflow()
        {
            
        }

        [WorkflowRun]
        public Task Run()
        {
            return Task.CompletedTask;
        }
    }
}
