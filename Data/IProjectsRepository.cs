
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BeetrootPortfolio.Models;

namespace BeetrootPortfolio.Data
{
public interface IProjectsRepository
{  
  Task<Project> GetProjectAsync(string id);
  Task<IEnumerable<Project>> GetProjectsAsync(Expression<Func<Project, bool>> predicate);
  Task<Project> CreateProjectAsync(Project item);
  Task<Project> UpdateItemAsync(string id, Project item);
  Task DeleteItemAsync(string id);
  Task<Info> GetInfoAsync(string key);
  }
}