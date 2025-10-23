namespace Q10.TaskManager.Infrastructure.DTOs
{
    public class TaskBulkCommand
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public List<TaskBulkRequest> Tasks { get; set; } = new List<TaskBulkRequest>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Se realiza por la conexión de PostgreSQL, ya que era necesario.
    }

    public class TaskBulkResult
    {
        public string CommandId { get; set; }
        public List<TaskBulkResponse> Results { get; set; } = new List<TaskBulkResponse>();
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow; // Se realiza por la conexión de PostgreSQL, ya que era necesario.
    }
}