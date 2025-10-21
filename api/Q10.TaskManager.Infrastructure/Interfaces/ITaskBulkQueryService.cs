﻿using Q10.TaskManager.Infrastructure.DTOs;

namespace Q10.TaskManager.Infrastructure.Interfaces
{
    public interface ITaskBulkQueryService
    {
        Task<List<TaskBulkResponse>> GetBulkTaskResultsAsync(string commandId);
    }
}